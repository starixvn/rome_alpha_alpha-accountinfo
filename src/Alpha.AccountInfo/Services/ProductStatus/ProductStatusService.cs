namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Constants;
    using Fanex.Data.Repository;
    using Helpers;
    using Models;
    using Models.ProductStatus;
    using Params.ProductStatus;
    using RemotingRef;

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
        private readonly IDynamicRepository repository;
        private readonly CacheRefService cacheRefService;
        private readonly ProductStatusCriteriaFactory criteriaFactory;
        private readonly ProductStatusAuthorizationService authorizationService;
        private readonly RnGCasinoService rnGCasinoService;

        public ProductStatusServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService,
            ProductStatusCriteriaFactory criteriaFactory,
            ProductStatusAuthorizationService authorizationService,
            RnGCasinoService rnGCasinoService)
        {
            repository = dynamicRepository;
            this.cacheRefService = cacheRefService;
            this.criteriaFactory = criteriaFactory;
            this.authorizationService = authorizationService;
            this.rnGCasinoService = rnGCasinoService;
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

            return await GetProductStatusAsync(productId, custId, custRole);
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

            switch(commandParam.ProductId)
            {
                case ProductIdConstants.Keno:
                    return await UpdateKenoStatusAsync(commandParam);
                case ProductIdConstants.RNGCasino:
                    return await UpdateCasinoStatus(commandParam);
                default:
                    return await UpdateStatusAsync(commandParam);
            }
        }

        private async Task<IEnumerable<ProductStatusModel>> GetProductStatusAsync(
            int productId,
            int custId,
            UserLevel custRole)
        {
            ICriteria criteria = criteriaFactory.CreateSelectCriteria(productId, custId, custRole.Value);
            var rawData = await repository.FetchAsync<ProductStatusModel>(criteria);

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
            if (betType == BetTypeConstants.AllbetStartRange)
            {
                return AppResources.ProductStatus.BetType_2401;
            }

            return cacheRefService.GetBetTypeName(betType);
        }

        private async Task<Result> UpdateKenoStatusAsync(ProductStatusCommandParam param)
        {
            IList<Result> results = new List<Result>();
            var criteria = criteriaFactory.CreateUpdateKenoStatusCriteria(param);

            foreach (var item in criteria)
            {
                await repository.ExecuteAsync(item);

                results.Add(item.Success
                    ? Result.CreateSuccessResult(AppResources.ProductStatus.StatusUpdatedSuccessfully)
                    : Result.CreateErrorResult(item.ErrCode, item.ErrMsg));
            }

            var errorResult = results.FirstOrDefault(r => !r.IsSuccess());

            return errorResult ?? results[0];
        }

        private async Task<Result> UpdateStatusAsync(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCriteria(param);

            await repository.ExecuteAsync(criteria);

            return criteria.Success ?
                Result.CreateSuccessResult(AppResources.ProductStatus.StatusUpdatedSuccessfully) :
                Result.CreateErrorResult(criteria.ErrCode, ResultHelper.ParseDCSProductMessage(criteria.ErrMsg));
        }

        private async Task<Result> UpdateCasinoStatus(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCasinoStatusCriteria(param);

            await repository.ExecuteAsync(criteria);

            if (criteria.Success)
            {
                rnGCasinoService.Update3rdRnGCasino(criteria.CustId,
                    !criteria.Disable,
                    criteria.IP,
                    criteria.ActorId,
                    param.ActorName,
                    criteria.SystemId);

                return Result.CreateSuccessResult(AppResources.ProductStatus.StatusUpdatedSuccessfully);
            }

             return Result.CreateErrorResult(criteria.ErrCode, criteria.ErrMsg);
        }
    }
}
