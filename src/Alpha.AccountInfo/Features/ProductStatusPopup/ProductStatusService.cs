namespace Alpha.AccountInfo.Services.ProductStatus
{
    using Alpha.AccountInfo.Features.ProductStatusPopup;
    using Alpha.AccountInfo.Features.ProductStatusPopup.AppResources;
    using Alpha.AccountInfo.Features.ProductStatusPopup.Models;
    using Alpha.AccountInfo.Features.ProductStatusPopup.Params.SPs;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.Products.Core.Shared;
    using Fanex.Data.Repository;
    using Fanex.Logging;
    using Models.ProductStatus;
    using Params.ProductStatus;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public interface ProductStatusService
    {
        Task<IEnumerable<ProductStatusModel>> GetProductStatusAsync(
            int productId,
            int custId,
            UserLevel custRole,
            int uplineId,
            UserLevel uplineLevel);

        Task<IEnumerable<Result>> UpdateProductStatusAsync(ProductStatusCommandParam commandParam);
    }

    public class ProductStatusServiceImpl : ProductStatusService
    {
        private const int DefaultErrorCode = 101;
        private const int DoNothingErrorCode = 90017;
        private const string UMDatetimeFormat = "dd/MM/yyyy HH:mm";
        private const int ParentChildViolated = 92;
        private readonly IDynamicRepository repository;
        private readonly CacheRefService cacheRefService;
        private readonly ProductStatusCriteriaFactory criteriaFactory;
        private readonly ProductStatusAuthorizationService authorizationService;
        private readonly ILogger logger;

        public ProductStatusServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService,
            ProductStatusCriteriaFactory criteriaFactory,
            ProductStatusAuthorizationService authorizationService,
            ILogger logger)
        {
            repository = dynamicRepository;
            this.cacheRefService = cacheRefService;
            this.criteriaFactory = criteriaFactory;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }

        public async Task<IEnumerable<ProductStatusModel>> GetProductStatusAsync(
            int productId,
            int custId,
            UserLevel custRole,
            int uplineId,
            UserLevel uplineLevel)
        {
            var authorizationParam = new ProductStatusAuthorizationParam(
                productId,
                custId,
                custRole,
                uplineId,
                uplineLevel);

            if (!authorizationService.CanView(authorizationParam))
            {
                return Enumerable.Empty<ProductStatusModel>();
            }

            return await GetProductStatusAsync(productId, custId, custRole).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Result>> UpdateProductStatusAsync(ProductStatusCommandParam commandParam)
        {
            var authorizationParam = new ProductStatusAuthorizationParam(
                   commandParam.ProductId,
                   commandParam.CustId,
                   commandParam.CustRole,
                   commandParam.UplineId,
                   commandParam.UplineLevel);

            if (!authorizationService.CanUpdate(authorizationParam))
            {
                return new[] { Result.CreateForbiddenResult() };
            }

            return await UpdateStatusAsync(commandParam).ConfigureAwait(false);
        }

        private async Task<IEnumerable<ProductStatusModel>> GetProductStatusAsync(
            int productId,
            int custId,
            UserLevel custRole)
        {
            ICriteria criteria = criteriaFactory.CreateSelectCriteria(productId, custId, custRole.Value);
            var rawData = await repository.FetchAsync<ProductStatusModel>(criteria).ConfigureAwait(false);

            return SetProductStatusName(productId, rawData);
        }

        private IEnumerable<ProductStatusModel> SetProductStatusName(
            int productId,
            IEnumerable<ProductStatusModel> rawData)
        {
            var hasMultipleRow = rawData.Count() > 1;

            foreach (var item in rawData)
            {
                item.Name = GetProductStatusName(productId, item.SportType, item.BetType, hasMultipleRow);

                yield return item;
            }
        }

        private string GetProductStatusName(int productId, int sportType, int betType, bool hasMultipleRow)
        {
            var sportShowName = new List<int>
            {
                SportTypeConstants.AllBet,
                SportTypeConstants.BBIN,
                SportTypeConstants.SexyGaming,
                SportTypeConstants.SAGaming,
                SportTypeConstants.GTCasino,
                SportTypeConstants.Lottery,
                SportTypeConstants.TableGaming,
                SportTypeConstants.ION,
                SportTypeConstants.RNGWar,
                SportTypeConstants.WM,
                SportTypeConstants.GDG,
                SportTypeConstants.BigGame,
                SportTypeConstants.SportsLottery,
                SportTypeConstants.PPLiveCasino,
                SportTypeConstants.MacauGames,
                SportTypeConstants.Jackpot,
                SportTypeConstants.RNGSlot,
                SportTypeConstants.VirtualGames,
                SportTypeConstants.FGG,
                SportTypeConstants.NumberGame,
                SportTypeConstants.RNGKeno
            };

            if (sportShowName.Contains(sportType))
            {
                return cacheRefService.GetSportTypeName(sportType);
            }

            if (hasMultipleRow)
            {
                return cacheRefService.GetBetTypeName(betType);
            }

            return cacheRefService.GetProductName(productId);
        }

        private async Task<IEnumerable<Result>> UpdateStatusAsync(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCriteria(param);
            await repository.ExecuteAsync(criteria).ConfigureAwait(false);

            if (!criteria.Success && criteria.ErrCode != DoNothingErrorCode)
            {
                return new[] { Result.CreateErrorResult(criteria.ErrCode, ParseDCSXmlErrorMessage(criteria.MsgType, criteria.ErrMsg, param.Language)) };
            }

            IList<Result> results = new List<Result>();

            if (criteria.Success)
            {
                results.Add(Result.CreateSuccessResult(GetResource("StatusUpdatedSuccessfully", param.Language)));
            }

            var umMessages = await GetProductStatusUMAsync(param);

            if (!string.IsNullOrWhiteSpace(umMessages))
            {
                results.Add(Result.CreateErrorResult(DoNothingErrorCode, umMessages));
            }

            return results;
        }

        private async Task<string> GetProductStatusUMAsync(ProductStatusCommandParam param)
        {
            var umMessages = new List<string>();
            var productStatusUM = await repository.FetchAsync<ProductStatusUM>(new CheckProductStatusUMCriteria(param.ProductId, param.CustId));

            foreach (var item in productStatusUM)
            {
                if (item.ProductId == 0 || item.SportType == 0)
                {
                    return $"{GetResource("ProductStatusUM", param.Language)}<br>{BuildProductStatusUMMessage(item, param.Language)}";
                }

                if (param.ProductStatusParams.Any(p => p.SportType == item.SportType))
                {
                    umMessages.Add(BuildProductStatusUMMessage(item, param.Language));
                }
            }

            return umMessages.Count == 0
                ? string.Empty
                : $"{GetResource("ProductStatusUM", param.Language)}<br>{string.Join("<br>", umMessages)}";
        }

        private string BuildProductStatusUMMessage(ProductStatusUM message, string language)
            => string.Format(
                GetResource("ProductStatusUMPattern", language),
                GetProductStatusName(message, language),
                message.SettingType == 0 ? GetResource("CustomerSettings", language) : GetResource("_ProductStatus", language),
                ToUMDatetime(message.BeginTime),
                ToUMDatetime(message.EndTime));

        private string GetProductStatusName(ProductStatusUM message, string language)
        {
            if (message.ProductId == 0)
            {
                return GetResource("AllProducts", language);
            }

            if (message.SportType == 0)
            {
                cacheRefService.GetProductName(message.ProductId, language);
            }

            return GetProductStatusName(message.ProductId, message.SportType, 0, false);
        }

        private string ParseDCSXmlErrorMessage(int msgType, string errMsg, string language)
        {
            if (IsValidXml(errMsg))
            {
                if (msgType == ParentChildViolated)
                {
                    return ParseParentChildViolatedMessage(errMsg, language);
                }

                var xmlRoot = XElement.Parse(errMsg);
                var errCode = xmlRoot.Attributes("MsgID").FirstOrDefault().Value;
                var errMsgFromResource = GetResource($"Msg{errCode}", language);

                var errors = new List<string>
                {
                    errMsgFromResource ?? xmlRoot.Attributes("Msg").First().Value
                };

                return string.Join("<br>", errors);
            }

            return errMsg;
        }

        private bool IsValidXml(string xml)
        {
            if (!string.IsNullOrEmpty(xml) && xml.TrimStart().StartsWith("<", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    XDocument.Parse(xml);
                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error($"Invalid DCS Xml: {xml}", ex);
                }
            }

            return false;
        }

        private static string GetResource(string name, string language)
            => ProductStatus.ResourceManager.GetString(name, new CultureInfo(language));

        private static string ToUMDatetime(DateTime dateTime)
            => dateTime.ToString(UMDatetimeFormat);

        private string ParseParentChildViolatedMessage(string message, string language)
        {
            var xmlRoot = XElement.Parse(message);
            var errCode = xmlRoot.Attributes("MsgID").FirstOrDefault().Value;
            string parentSportType = string.Empty;
            string parentBetType = string.Empty;
            var errMsgTemplate = GetResource("Msg" + errCode, language);
            List<string> childSportTypesNames = new List<string>();
            StringBuilder errorMessages = new StringBuilder();
            foreach (var xmlElement in xmlRoot.Elements())
            {
                var currentParentSportType = xmlElement.Attribute("ParentSportType").Value;
                var currentParentBetType = xmlElement.Attribute("ParentBetType").Value;
                var currentSportType = xmlElement.Attribute("SportType").Value;
                if (currentParentSportType != parentSportType || currentParentBetType != parentBetType)
                {
                    if (!string.IsNullOrWhiteSpace(parentSportType))
                    {
                        errorMessages.Append(string.Format(errMsgTemplate, string.Join(", ", childSportTypesNames), cacheRefService.GetSportTypeName(Convert.ToInt32(parentSportType))));
                    }

                    parentSportType = currentParentSportType;
                    parentBetType = currentParentBetType;
                    childSportTypesNames = new List<string>();
                }

                childSportTypesNames.Add(cacheRefService.GetSportTypeName(Convert.ToInt32(currentSportType), language));
            }

            errorMessages.Append(string.Format(errMsgTemplate, string.Join(", ", childSportTypesNames), cacheRefService.GetSportTypeName(Convert.ToInt32(parentSportType))));
            return errorMessages.ToString();
        }
    }
}