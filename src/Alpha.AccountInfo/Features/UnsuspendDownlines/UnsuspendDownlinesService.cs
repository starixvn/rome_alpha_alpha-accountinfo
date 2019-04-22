namespace Alpha.AccountInfo.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Fanex.Data.Repository;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface UnsuspendDownlinesService
    {
        Task<IEnumerable<UnsuspendMultipleModel>> GetUnsuspendInfoModel(int searchAccountId);

        Task<UnsuspendViewListModel> GetUnsuspendViewListModel();

        Task<IEnumerable<UnsuspendMultipleByUserModel>> GetUnsuspendMultipleModel(string custIds, byte roleId);
    }

    public class UnsuspendDownlinesServiceImpl : UnsuspendDownlinesService
    {
        private readonly IDynamicRepository dynamicRepository;

        public UnsuspendDownlinesServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public Task<IEnumerable<UnsuspendMultipleModel>> GetUnsuspendInfoModel(int searchAccountId)
            => dynamicRepository.FetchAsync<UnsuspendMultipleModel>(new SelectSuspendInfoCriteria(searchAccountId));

        public async Task<UnsuspendViewListModel> GetUnsuspendViewListModel()
        {
            var unsuspendDownlines = new List<UnsuspendDownlineDto>();

           for(int i = 0; i < 99; i++)
            {
                unsuspendDownlines.Add( new UnsuspendDownlineDto($"Username-{i}", i, i < 30 ? (byte)1 : i < 60 ? (byte)2 : (byte)3));
            }

            return new UnsuspendViewListModel(unsuspendDownlines.GroupBy(item => item.RoleId).Select(group => {
                return new UnsuspendDownlineByLevelModel(group.Key, group);
            }), 5);
        }

        public Task<IEnumerable<UnsuspendMultipleByUserModel>> GetUnsuspendMultipleModel(string custIds, byte roleId)
            => dynamicRepository.FetchAsync<UnsuspendMultipleByUserModel>(new SelectSuspendInfoMultipleCriteria(custIds, roleId));
        //{
        //    var model = new List<UnsuspendMultipleByUserModel>();

        //    for (int j = 5; j > 0; j--)
        //    {
        //        var unsuspendByLevel = new List<UnsuspendInfoModel>();
        //        for (int i = 3; i > 0; i--)
        //        {
        //            unsuspendByLevel.Add(new UnsuspendInfoModel((byte)i, i * 15 + j, i * 30 + j));
        //        }

        //        model.Add(new UnsuspendMultipleByUserModel(j * 2, $"Username-{j}", j % 2 == 0, unsuspendByLevel));
        //    }

        //    return model;
        //}
    }
}
