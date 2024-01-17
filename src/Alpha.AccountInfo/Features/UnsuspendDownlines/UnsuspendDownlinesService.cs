namespace Alpha.AccountInfo.Features.UnsuspendDownlines
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
    using Fanex.Data.Repository;

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

        public UnsuspendDownlinesServiceImpl(IDynamicRepository dynamicRepository)
            => this.dynamicRepository = dynamicRepository;

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
        {
            var criteria = new SelectSuspendInfoDetailCriteria(
                custId,
                downlineRoleId,
                isAffected,
                pageSize,
                pageIndex);
            var downlines = await dynamicRepository.FetchAsync<UnsuspendDownlineDto>(criteria);

            return new UnsuspendViewListModel(
                downlines.GroupBy(item => item.RoleId).Select(group => new UnsuspendDownlineByLevelModel(group.Key, group)),
                criteria.TotalRows);
        }

        public async Task<IEnumerable<UnsuspendMultipleModel>> GetUnsuspendMultipleModel(string custIds, byte roleId)
        {
            var (searchAccounts, downlines) = await dynamicRepository
                .FetchMultipleAsync<UnsuspendSearchAccountModel, UnsuspendDownlineModel>(
                    new SelectSuspendInfoMultipleCriteria(custIds, roleId));

            return searchAccounts.Select(searchAccount =>
                new UnsuspendMultipleModel(
                    searchAccount.SearchAccountId,
                    searchAccount.Username,
                    searchAccount.Suspended,
                    downlines
                        .Where(downline => downline.SearchAccountId == searchAccount.SearchAccountId)
                        .OrderByDescending(downline => downline.DownlineRoleId).ToList()));
        }
    }
}
