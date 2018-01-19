namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Constants;
    using Fanex.Data.Repository;
    using Models.ProductStatus;
    using RemotingRef;

    public interface ProductStatusService
    {
        Task<IEnumerable<GroupProductStatus>> GetProductStatusAsync(int productId, int custId, byte roleId);
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
            var rawData = await repository.FetchAsync<ProductStatusModel>(criteria);

            var rawDataCount = rawData != null ? rawData.Count() : 0;

            if (rawDataCount == 0)
            {
                return Enumerable.Empty<GroupProductStatus>();
            }

            return rawDataCount == 1 ?
                GroupByProduct(productId, rawData) :
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
    }
}
