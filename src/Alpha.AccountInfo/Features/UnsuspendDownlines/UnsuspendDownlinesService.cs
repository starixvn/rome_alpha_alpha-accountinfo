namespace Alpha.AccountInfo.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
    using Fanex.Data.Repository;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface UnsuspendDownlinesService
    {
        Task<IEnumerable<UnsuspendSingleModel>> GetUnsuspendInfoModel(int searchAccountId);

        Task<UnsuspendViewListModel> GetUnsuspendViewListModel(
            int custId,
            bool isAffected,
            byte downlineRoleId,
            int pageSize,
            int pageIndex);

        Task<IEnumerable<UnsuspendMultipleModel>> GetUnsuspendMultipleModel(string custIds, byte roleId);

        Task<bool> CheckMultipleCustomesBelonging(int actorId, int subAccountId, string custIds, bool isInternal);
    }

    public class UnsuspendDownlinesServiceImpl : UnsuspendDownlinesService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly UnsuspendDownlinesDataAccess unsuspendDownlinesDataAccess;

        public UnsuspendDownlinesServiceImpl(
            IDynamicRepository dynamicRepository, 
            UnsuspendDownlinesDataAccess unsuspendDownlinesDataAccess)
        {
            this.dynamicRepository = dynamicRepository;
            this.unsuspendDownlinesDataAccess = unsuspendDownlinesDataAccess;
        }

        public async Task<IEnumerable<UnsuspendSingleModel>> GetUnsuspendInfoModel(int searchAccountId)
            => await dynamicRepository
            .FetchAsync<UnsuspendSingleModel>(new SelectSuspendInfoCriteria(searchAccountId))
            .ConfigureAwait(false);

        public async Task<UnsuspendViewListModel> GetUnsuspendViewListModel(
            int custId,
            bool isAffected,
            byte downlineRoleId,
            int pageSize,
            int pageIndex)
            => await unsuspendDownlinesDataAccess.GetDownlinesInfoStatus(
                new SelectSuspendInfoDetailCriteria(
                    custId, 
                    downlineRoleId, 
                    isAffected,
                    pageSize,
                    pageIndex)).ConfigureAwait(false);

        public async Task<IEnumerable<UnsuspendMultipleModel>> GetUnsuspendMultipleModel(string custIds, byte roleId)
            => await unsuspendDownlinesDataAccess
            .GetUnsuspendMultipleModel(new SelectSuspendInfoMultipleCriteria(custIds, roleId))
            .ConfigureAwait(false);

        public async Task<bool> CheckMultipleCustomesBelonging(int actorId, int subAccountId, string custIds, bool isInternal)
        {
            var criteria = new CheckMultipleCustomesBelongingCriteria(actorId, subAccountId, custIds);

            await dynamicRepository.ExecuteAsync(criteria).ConfigureAwait(false);

            return criteria.RoleId > 0 && (isInternal || criteria.IsActive);
        }
    }
}
