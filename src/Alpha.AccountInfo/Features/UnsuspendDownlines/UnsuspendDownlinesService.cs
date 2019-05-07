namespace Alpha.AccountInfo.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
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

        public Task<IEnumerable<UnsuspendSingleModel>> GetUnsuspendInfoModel(int searchAccountId)
            => dynamicRepository.FetchAsync<UnsuspendSingleModel>(new SelectSuspendInfoCriteria(searchAccountId));

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

        public Task<IEnumerable<UnsuspendMultipleModel>> GetUnsuspendMultipleModel(string custIds, byte roleId)
            => unsuspendDownlinesDataAccess.GetUnsuspendMultipleModel(new SelectSuspendInfoMultipleCriteria(custIds, roleId));
    }
}
