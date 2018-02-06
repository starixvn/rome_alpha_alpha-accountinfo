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

        public ProductStatusServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService,
            ProductStatusCriteriaFactory criteriaFactory,
            ProductStatusAuthorizationService authorizationService)
        {
            repository = dynamicRepository;
            this.cacheRefService = cacheRefService;
            this.criteriaFactory = criteriaFactory;
            this.authorizationService = authorizationService;
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

            return commandParam.ProductId == ProductIdConstants.Keno ?
                await UpdateKenoStatusAsync(commandParam) :
                await UpdateStatusAsync(commandParam);
        }

        private async Task<IEnumerable<ProductStatusModel>> GetProductStatusAsync(
            int productId,
            int custId,
            UserLevel custRole)
        {
            ICriteria criteria = criteriaFactory.CreateSelectCriteria(productId, custId, custRole.Value);
            var rawData = await repository.FetchAsync<ProductStatusModel>(criteria);
            var rawDataCount = rawData.Count();

            if (rawDataCount == 1)
            {
                return GetProductStatusProductName(rawData);
            }

            return GetProductStatusBetTypeName(rawData);
        }

        private IEnumerable<ProductStatusModel> GetProductStatusProductName(IEnumerable<ProductStatusModel> entities)
        {
            foreach (var item in entities)
            {
                item.BetTypeName = cacheRefService.GetProductName(item.ProductId);

                yield return item;
            }
        }

        private IEnumerable<ProductStatusModel> GetProductStatusBetTypeName(IEnumerable<ProductStatusModel> entities)
        {
            foreach (var item in entities)
            {
                item.BetTypeName = GetBetTypeName(item.BetType);

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
            IList<Result> errors = new List<Result>();
            var criteria = criteriaFactory.CreateUpdateKenoStatusCriteria(param);

            foreach (var item in criteria)
            {
                await repository.ExecuteAsync(item);

                if (item.ErrCode != 0)
                {
                    errors.Add(Result.CreateErrorResult(item.ErrCode, item.ErrMsg));
                }
            }

            return errors.Count == 0 ?
                Result.CreateSuccessResult(AppResources.ProductStatus.StatusUpdatedSuccessfully) :
                ResultHelper.ParseKenoErrorResults(errors);
        }

        private async Task<Result> UpdateStatusAsync(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCriteria(param);

            await repository.ExecuteAsync(criteria);

            return criteria.ErrCode == 0 ?
                Result.CreateSuccessResult(AppResources.ProductStatus.StatusUpdatedSuccessfully) :
                Result.CreateErrorResult(criteria.ErrCode, ResultHelper.ParseDCSProductMessage(criteria.ErrMsg));
        }

        ////private void UpdateCasinoStatus(UpdateCasinoStatusCriteria criteria)
        ////{
        ////    const int CasinoFromSystem = 2;
        ////    var customer = new Customer();

        ////    try
        ////    {
        ////        customer.UpdateCustEnableCasino(
        ////            criteria.CustId,
        ////            !criteria.DisabledCS,
        ////            !criteria.DisabledCS,
        ////            criteria.IP,
        ////            criteria.UserId,
        ////            param.LoginUserName,
        ////            CasinoFromSystem);
        ////    }
        ////    catch (ErrorException casinoEx)
        ////    {
        ////        //ExceptionHandler.Handle(new Exception(casinoEx.Message, casinoEx), TraceEventType.Warning);
        ////        throw;
        ////    }
        ////    catch (Exception)
        ////    {
        ////        throw;
        ////    }
        ////}
    }
}
