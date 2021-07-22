namespace Alpha.AccountInfo.Features.OpenDownlines
{
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
        private readonly OpenDownlinesDataAccess openDownlinesDataAccess;

        public OpenDownlinesServiceImpl(
            IDynamicRepository dynamicRepository,
            OpenDownlinesDataAccess openDownlinesDataAccess)
        {
            this.dynamicRepository = dynamicRepository;
            this.openDownlinesDataAccess = openDownlinesDataAccess;
        }

        public async Task<IEnumerable<OpenSingleModel>> GetOpenInfoModel(int searchAccountId)
            => await dynamicRepository
            .FetchAsync<OpenSingleModel>(new SelectCloseInfoCriteria(searchAccountId))
            .ConfigureAwait(false);

        public async Task<IEnumerable<OpenMultipleModel>> GetOpenMultipleModel(string custIds, byte roleId)
        => await openDownlinesDataAccess
            .GetOpenMultipleModel(new SelectCloseInfoMultipleCriteria(custIds, roleId))
            .ConfigureAwait(false);

        public async Task<OpenViewListModel> GetOpenViewListModel(
            int custId,
            bool isAffected,
            byte downlineRoleId,
            int pageSize,
            int pageIndex)
        => await openDownlinesDataAccess.GetDownlinesInfoStatus(
                new SelectCloseInfoDetailCriteria(
                    custId,
                    downlineRoleId,
                    isAffected,
                    pageSize,
                    pageIndex)).ConfigureAwait(false);
    }
}