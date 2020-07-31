namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Alpha.AccountInfo.Features.ProductStatusPopup;
    using Alpha.AccountInfo.Features.ProductStatusPopup.AppResources;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Constants;
    using Alpha.AccountInfo.Features.Shared.Extensions;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Params;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.Products.Core.Shared;
    using Fanex.Data.Repository;
    using Fanex.Logging;
    using Models.ProductStatus;
    using Params.ProductStatus;

    public interface ProductStatusService
    {
        Task<IEnumerable<ProductStatusModel>> GetProductStatusAsync(
            int productId,
            int custId,
            UserLevel custRole,
            int uplineId,
            UserLevel uplineLevel);

        Task<Result> UpdateProductStatusAsync(ProductStatusCommandParam commandParam);
    }

    public class ProductStatusServiceImpl : ProductStatusService
    {
        private const int DefaultErrorCode = 101;

        private readonly IDynamicRepository repository;
        private readonly CacheRefService cacheRefService;
        private readonly ProductStatusCriteriaFactory criteriaFactory;
        private readonly ProductStatusAuthorizationService authorizationService;
        private readonly RnGCasinoService rnGCasinoService;
        private readonly ILogger logger;
        private readonly UMService umService;

        public ProductStatusServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService,
            ProductStatusCriteriaFactory criteriaFactory,
            ProductStatusAuthorizationService authorizationService,
            RnGCasinoService rnGCasinoService,
            ILogger logger,
            UMService umService)
        {
            repository = dynamicRepository;
            this.cacheRefService = cacheRefService;
            this.criteriaFactory = criteriaFactory;
            this.authorizationService = authorizationService;
            this.rnGCasinoService = rnGCasinoService;
            this.logger = logger;
            this.umService = umService;
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

        public async Task<Result> UpdateProductStatusAsync(ProductStatusCommandParam commandParam)
        {
            var authorizationParam = new ProductStatusAuthorizationParam(
                   commandParam.ProductId,
                   commandParam.CustId,
                   commandParam.CustRole,
                   commandParam.UplineId,
                   commandParam.UplineLevel);

            if (!authorizationService.CanUpdate(authorizationParam))
            {
                return Result.CreateForbiddenResult();
            }

            switch (commandParam.ProductId)
            {
                case ProductConstants.Keno:
                    return await UpdateKenoStatusAsync(commandParam).ConfigureAwait(false);

                case ProductConstants.RNGCasino:
                    return await UpdateCasinoStatus(commandParam).ConfigureAwait(false);

                default:
                    return await UpdateStatusAsync(commandParam).ConfigureAwait(false);
            }
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
                SportTypeConstants.RNGWar
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
            const int UMErrorCode = 90014;
            IList<string> errorMessages = new List<string>();
            var criteria = criteriaFactory.CreateUpdateKenoStatusCriteria(param);

            foreach (var item in criteria)
            {
                await repository.ExecuteAsync(item).ConfigureAwait(false);

                if (!item.Success)
                {
                    if (item.ErrCode != UMErrorCode)
                    {
                        errorMessages.Add($"{item.ErrMsg}: {cacheRefService.GetBetTypeName(item.BetType)}");
                    }
                    else if (errorMessages.Count == 0)
                    {
                        errorMessages.Add(item.ErrMsg);
                    }
                }
            }

            return errorMessages.Count == 0
                ? Result.CreateSuccessResult(GetResource("StatusUpdatedSuccessfully", param.Language))
                : Result.CreateErrorResult(DefaultErrorCode, string.Join("<br>", errorMessages));
        }

        private async Task<Result> UpdateStatusAsync(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCriteria(param);
            await repository.ExecuteAsync(criteria).ConfigureAwait(false);

            if (criteria.Success)
            {
                var umMessages = await GetUMMessagesAsync(param);

                return string.IsNullOrWhiteSpace(umMessages)
                    ? Result.CreateSuccessResult(GetResource("StatusUpdatedSuccessfully", param.Language))
                    : Result.CreateErrorResult(DefaultErrorCode, umMessages);
            }

            return Result.CreateErrorResult(criteria.ErrCode, ParseDCSXmlErrorMessage(criteria.ErrMsg, param.Language));
        }

        private async Task<string> GetUMMessagesAsync(ProductStatusCommandParam param)
        {
            IList<string> umMessages = new List<string>();
            var settingType = umService.GetDCSSettingType(param.ProductId);

            foreach (var item in param.ProductStatusParams)
            {
                var message = await umService.GetUMMessageAsync(
                    new CheckUMParam(param.CustId, param.ProductId, item.SportType, settingType, FunctionTypeConstants.LockEdit, param.Language));

                if (message != null)
                {
                    umMessages.Add(BuildProductStatusUMMessage(message, param.ProductId, item.SportType, param.Language));
                }
            }

            return string.Join("<br>", umMessages);
        }

        private string BuildProductStatusUMMessage(UMMessageItem message, int productId, int sportType, string language)
            => string.Format(
                GetResource("UMProductStatusPattern", language),
                GetProductStatusName(productId, sportType, 0, false),
                message.BeginTime.ToLongDateWithSlash(),
                message.EndTime.ToLongDateWithSlash());

        private async Task<Result> UpdateCasinoStatus(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCasinoStatusCriteria(param);

            await repository.ExecuteAsync(criteria).ConfigureAwait(false);

            if (criteria.Success)
            {
                rnGCasinoService.SyncedToRnGCasino3rdByStatus(
                 new UpdateRnGCasino3rdParam(
                     criteria.CustId,
                     StatusNames.EnableRnGCasino3rd,
                     !criteria.Disable,
                     criteria.ActorId,
                      param.ActorName,
                      criteria.IP
                     ));

                return Result.CreateSuccessResult(GetResource("StatusUpdatedSuccessfully", param.Language));
            }

            return Result.CreateErrorResult(criteria.ErrCode, criteria.ErrMsg);
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
    }
}