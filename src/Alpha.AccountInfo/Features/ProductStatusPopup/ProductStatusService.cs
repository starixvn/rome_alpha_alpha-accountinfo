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
        private const int UMKenoErrorCode = 90014;
        private const int DoNothingErrorCode = 90017;
        private const string UMDatetimeFormat = "dd/MM/yyyy HH:mm";

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

            return commandParam.ProductId == ProductConstants.Keno ? new[] { await UpdateKenoStatusAsync(commandParam).ConfigureAwait(false) } :
                await UpdateStatusAsync(commandParam).ConfigureAwait(false);
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
                SportTypeConstants.VirtualGames,
                SportTypeConstants.FGG
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

        private async Task<Result> UpdateKenoStatusAsync(ProductStatusCommandParam param)
        {
            IList<string> errorMessages = new List<string>();
            var criteria = criteriaFactory.CreateUpdateKenoStatusCriteria(param);

            foreach (var item in criteria)
            {
                await repository.ExecuteAsync(item).ConfigureAwait(false);

                if (!item.Success)
                {
                    if (item.ErrCode != UMKenoErrorCode)
                    {
                        errorMessages.Add($"{item.ErrMsg}: {cacheRefService.GetBetTypeName(item.BetType)}");
                    }
                    else if (errorMessages.Count == 0)
                    {
                        var errorMessage = item.ErrMsg.Contains("{0}")
                            ? string.Format(item.ErrMsg, cacheRefService.GetProductName(ProductConstants.Keno, param.Language))
                            : item.ErrMsg;
                        errorMessages.Add(errorMessage);
                    }
                }
            }

            return errorMessages.Count == 0
                ? Result.CreateSuccessResult(GetResource("StatusUpdatedSuccessfully", param.Language))
                : Result.CreateErrorResult(DefaultErrorCode, string.Join("<br>", errorMessages));
        }

        private async Task<IEnumerable<Result>> UpdateStatusAsync(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCriteria(param);
            await repository.ExecuteAsync(criteria).ConfigureAwait(false);

            if (!criteria.Success && criteria.ErrCode != DoNothingErrorCode)
            {
                return new[] { Result.CreateErrorResult(criteria.ErrCode, ParseDCSXmlErrorMessage(criteria.ErrMsg, param.Language)) };
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

        private string ParseDCSXmlErrorMessage(string errMsg, string language)
        {
            if (IsValidXml(errMsg))
            {
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
    }
}