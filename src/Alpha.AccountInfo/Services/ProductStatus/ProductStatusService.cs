namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Constants;
    using Fanex.Data.Repository;
    using Models;
    using Models.ProductStatus;
    using Params.ProductStatus;
    using RemotingRef;

    public interface ProductStatusService
    {
        Task<IEnumerable<GroupProductStatus>> GetProductStatusAsync(
            ProductStatusAuthorizationParam authorizationParam,
            ProductStatusQueryParam queryParam);

        Task<Result> UpdateProductStatusAsync(
            ProductStatusAuthorizationParam authorizationParam,
            ProductStatusCommandParam commandParam);
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

        public async Task<IEnumerable<GroupProductStatus>> GetProductStatusAsync(
            ProductStatusAuthorizationParam authorizationParam,
            ProductStatusQueryParam queryParam)
        {
            if (!authorizationService.CanView(authorizationParam))
            {
                return Enumerable.Empty<GroupProductStatus>();
            }

            return await GetProductStatusAsync(queryParam);
        }

        public async Task<Result> UpdateProductStatusAsync(
            ProductStatusAuthorizationParam authorizationParam,
            ProductStatusCommandParam commandParam)
        {
            if (!authorizationService.CanUpdate(authorizationParam))
            {
                return Result.CreateForbiddenResult();
            }

            return commandParam.ProductId == ProductIdConstants.Keno ?
                await UpdateKenoStatusAsync(commandParam) :
                await UpdateStatusAsync(commandParam);
        }

        private static Result ParseKenoErrorResults(IList<Result> errors)
        {
            IList<Result> flattenErrors = new List<Result>();
            var groupErrorCode = errors.ToLookup(err => err.Code);

            foreach (var group in groupErrorCode)
            {
                flattenErrors.Add(Result.CreateErrorResult(group.Key, group.First().Message));
            }

            return Result.CreateErrorResult(
                flattenErrors[0].Code,
                string.Join("<br/>", flattenErrors.Select(err => err.Message)));
        }

        private async Task<IEnumerable<GroupProductStatus>> GetProductStatusAsync(ProductStatusQueryParam queryParam)
        {
            ICriteria criteria = criteriaFactory.CreateSelectCriteria(queryParam.ProductId, queryParam.CustId, queryParam.CustRoleId);
            var rawData = await repository.FetchAsync<ProductStatusModel>(criteria);
            var rawDataCount = rawData.Count();

            if (rawDataCount == 0)
            {
                return Enumerable.Empty<GroupProductStatus>();
            }

            return rawDataCount == 1 ?
                GroupByProduct(queryParam.ProductId, rawData) :
                GroupBySportType(rawData);
        }

        private IEnumerable<GroupProductStatus> GroupByProduct(int productId, IEnumerable<ProductStatusModel> entities)
        {
            yield return new GroupProductStatus(productId, cacheRefService.GetProductName(productId), entities);
        }

        private IEnumerable<GroupProductStatus> GroupBySportType(IEnumerable<ProductStatusModel> entities)
        {
            var groupEntities = entities.ToLookup(kvp => kvp.SportType, kvp => kvp);

            foreach (var group in groupEntities)
            {
                foreach (var item in group)
                {
                    item.BetTypeName = GetBetTypeName(item.BetType);
                }

                yield return new GroupProductStatus(group.Key, group);
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
                ParseKenoErrorResults(errors);
        }

        private async Task<Result> UpdateStatusAsync(ProductStatusCommandParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCriteria(param);

            await repository.ExecuteAsync(criteria);

            return criteria.ErrCode == 0 ?
                Result.CreateSuccessResult(AppResources.ProductStatus.StatusUpdatedSuccessfully) :
                Result.CreateErrorResult(criteria.ErrCode, criteria.ParseErrMsg());
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
