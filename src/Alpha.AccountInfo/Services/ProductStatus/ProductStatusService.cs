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
        Task<IEnumerable<GroupProductStatus>> GetProductStatusAsync(int productId, int custId, byte roleId);

        Task<Result> UpdateKenoStatusAsync(UpdateProductStatusParam param);

        Task<Result> UpdateProductStatusAsync(UpdateProductStatusParam param);
    }

    public class ProductStatusServiceImpl : ProductStatusService
    {
        private readonly IDynamicRepository repository;
        private readonly CacheRefService cacheRefService;
        private readonly ProductStatusCriteriaFactory criteriaFactory;

        public ProductStatusServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService,
            ProductStatusCriteriaFactory criteriaFactory)
        {
            repository = dynamicRepository;
            this.cacheRefService = cacheRefService;
            this.criteriaFactory = criteriaFactory;
        }

        public async Task<IEnumerable<GroupProductStatus>> GetProductStatusAsync(int productId, int custId, byte roleId)
        {
            ICriteria criteria = criteriaFactory.CreateSelectCriteria(productId, custId, roleId);
            var rawData = await repository
                .FetchAsync<ProductStatusModel>(criteria)
                .ConfigureAwait(false);

            var rawDataCount = rawData != null ? rawData.Count() : 0;

            if (rawDataCount == 0)
            {
                return Enumerable.Empty<GroupProductStatus>();
            }

            return rawDataCount == 1 ?
                GroupByProduct(productId, rawData) :
                GroupBySportType(rawData);
        }

        public async Task<Result> UpdateKenoStatusAsync(UpdateProductStatusParam param)
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

        public async Task<Result> UpdateProductStatusAsync(UpdateProductStatusParam param)
        {
            var criteria = criteriaFactory.CreateUpdateCriteria(param);

            await repository.ExecuteAsync(criteria);

            return criteria.ErrCode == 0 ?
                Result.CreateSuccessResult(AppResources.ProductStatus.StatusUpdatedSuccessfully) :
                Result.CreateErrorResult(criteria.ErrCode, criteria.ParseErrMsg());
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
