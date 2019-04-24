namespace Alpha.AccountInfo.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Fanex.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public interface UnsuspendDownlinesDataAccess
    {
        Task<UnsuspendViewListModel> GetDownlinesInfoStatus(SelectSuspendInfoDetailCriteria criteria);
    }

    public class UnsuspendDownlinesDataAccessImpl : UnsuspendDownlinesDataAccess
    {
        public async Task<UnsuspendViewListModel> GetDownlinesInfoStatus(SelectSuspendInfoDetailCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = await objectDb.QueryMultipleAsync(criteria.GetSpParams()).ConfigureAwait(false))
                {
                    var downlines = await multiResultReader.ReadAsync<UnsuspendDownlineDto>().ConfigureAwait(false);

                    return new UnsuspendViewListModel(downlines.GroupBy(item => item.RoleId).Select(group => {
                        return new UnsuspendDownlineByLevelModel(group.Key, group);
                    }), criteria.TotalRows);
                }
            }
        }
    }
}
