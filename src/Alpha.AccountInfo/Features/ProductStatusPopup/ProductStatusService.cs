namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Alpha.AccountInfo.Features.ProductStatusPopup.AppResources;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
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
        private const int AllbetStartRange = 2401;

        private readonly IDynamicRepository repository;
        private readonly CacheRefService cacheRefService;
        private readonly ProductStatusCriteriaFactory criteriaFactory;
        private readonly ProductStatusAuthorizationService authorizationService;
        private readonly RnGCasinoService rnGCasinoService;
        private readonly ILogger logger;

        public ProductStatusServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService,
            ProductStatusCriteriaFactory criteriaFactory,
            ProductStatusAuthorizationService authorizationService,
            RnGCasinoService rnGCasinoService,
            ILogger logger)
        {
            repository = dynamicRepository;
            this.cacheRefService = cacheRefService;
            this.criteriaFactory = criteriaFactory;
            this.authorizationService = authorizationService;
            this.rnGCasinoService = rnGCasinoService;
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
            var setNameByBetTypeId = rawData.Count() > 1;

            foreach (var item in rawData)
            {
                item.Name = setNameByBetTypeId
                    ? GetBetTypeName(item.BetType)
                    : cacheRefService.GetProductName(productId);

                yield return item;
            }
        }

        private string GetBetTypeName(int betType)
        {
            if (betType == AllbetStartRange)
            {
                return ProductStatus.BetType_2401;
            }

            return cacheRefService.GetBetTypeName(betType);
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
                    errorMessages.Add($"{item.ErrMsg}: {cacheRefService.GetBetTypeName(item.BetType)}");
                }
            }

            return errorMessages.Count == 0
                ? Result.CreateSuccessResult(ProductStatus.StatusUpdatedSuccessfully)
                : Result.CreateErrorResult(101, string.Join("<br>", errorMessages));
        }

        private async Task<Result> UpdateStatusAsync(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCriteria(param);

            await repository.ExecuteAsync(criteria).ConfigureAwait(false);

            return criteria.Success ?
                Result.CreateSuccessResult(ProductStatus.StatusUpdatedSuccessfully) :
                Result.CreateErrorResult(criteria.ErrCode, ParseDCSXmlErrorMessage(criteria.ErrMsg, param.ProductId));
        }

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

                return Result.CreateSuccessResult(ProductStatus.StatusUpdatedSuccessfully);
            }

            return Result.CreateErrorResult(criteria.ErrCode, criteria.ErrMsg);
        }

        private string ParseDCSXmlErrorMessage(string errMsg, int productId)
        {
            if (IsValidXml(errMsg))
            {
                var xmlRoot = XElement.Parse(errMsg);
                var errCode = xmlRoot.Attributes("MsgID").FirstOrDefault().Value;
                var errMsgFromResource = ProductStatus.ResourceManager.GetString($"Msg{errCode}");

                var errors = new List<string>
                {
                    errMsgFromResource ?? xmlRoot.Attributes("Msg").First().Value
                };

                if (productId == ProductConstants.AllBet)
                {
                    errors.AddRange(xmlRoot.Elements()
                        .Select(row => GetBetTypeName(int.Parse(row.Attributes("BetType").First().Value))));
                }

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
    }
}