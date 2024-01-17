namespace Alpha.AccountInfo.Features.OpenDownlines
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fanex.Data.Repository;
    using Models;
    using Params;

    public interface OpenDownlinesService
    {
        Task<IEnumerable<OpenSingleModel>> GetOpenInfoModel(int searchAccountId);

        Task<IEnumerable<OpenMultipleModel>> GetOpenMultipleModel(string custIds, byte roleId);

        Task<OpenViewListModel> GetOpenViewListModel(
            int custId,
            bool isAffected,
            byte downlineRoleId,
            int pageSize,
            int pageIndex);
    }

    public class OpenDownlinesServiceImpl : OpenDownlinesService
    {
        private readonly IDynamicRepository dynamicRepository;

        public OpenDownlinesServiceImpl(IDynamicRepository dynamicRepository)
            => this.dynamicRepository = dynamicRepository;

        public async Task<IEnumerable<OpenSingleModel>> GetOpenInfoModel(int searchAccountId)
            => await dynamicRepository
            .FetchAsync<OpenSingleModel>(new SelectCloseInfoCriteria(searchAccountId))
            .ConfigureAwait(false);

        public async Task<IEnumerable<OpenMultipleModel>> GetOpenMultipleModel(string custIds, byte roleId)
        {
            var (searchAccounts, downlines) = await dynamicRepository.FetchMultipleAsync<OpenSearchAccountModel, OpenDownlineModel>(
                new SelectCloseInfoMultipleCriteria(custIds, roleId));

            return searchAccounts.Select(searchAccount => new OpenMultipleModel(
                searchAccount.SearchAccountId,
                searchAccount.Username,
                searchAccount.IsClosed,
                downlines
                    .Where(downline => downline.SearchAccountId == searchAccount.SearchAccountId)
                    .OrderByDescending(downline => downline.DownlineRoleId).ToList()));
        }

        public async Task<OpenViewListModel> GetOpenViewListModel(
            int custId,
            bool isAffected,
            byte downlineRoleId,
            int pageSize,
            int pageIndex)
        {
            var criteria = new SelectCloseInfoDetailCriteria(
                custId,
                downlineRoleId,
                isAffected,
                pageSize,
                pageIndex);
            var downlines = await dynamicRepository.FetchAsync<OpenDownlineDto>(criteria);

            return new OpenViewListModel(
                downlines.GroupBy(item => item.RoleId).Select(
                    group => new OpenDownlineByLevelModel(group.Key, group)),
                criteria.TotalRows);
        }
    }
}