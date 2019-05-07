namespace Alpha.AccountInfo.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Fanex.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface UnsuspendDownlinesDataAccess
    {
        Task<UnsuspendViewListModel> GetDownlinesInfoStatus(SelectSuspendInfoDetailCriteria criteria);

        Task<IEnumerable<UnsuspendMultipleModel>> GetUnsuspendMultipleModel(SelectSuspendInfoMultipleCriteria criteria);
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

                    var dataFake = new List<UnsuspendDownlineDto>();
                    for (byte i = 1; i < 3; i++)
                    {
                        for (int j = criteria.PageSize * criteria.PageIndex; j < criteria.PageSize * (criteria.PageIndex + 1); j++)
                        {
                            dataFake.Add(new UnsuspendDownlineDto($"PageIndex:{criteria.PageIndex}-Affected:{criteria.IsAffected}-{i}-{j}", i));
                        }
                    }

                    return new UnsuspendViewListModel(dataFake.GroupBy(item => item.RoleId).Select(group =>
                    {
                        return new UnsuspendDownlineByLevelModel(group.Key, group);
                    }), criteria.TotalRows);
                }
            }
        }

        public async Task<IEnumerable<UnsuspendMultipleModel>> GetUnsuspendMultipleModel(SelectSuspendInfoMultipleCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = await objectDb.QueryMultipleAsync(criteria.GetSpParams()).ConfigureAwait(false))
                {
                    var searchAccounts = (await multiResultReader.ReadAsync<UnsuspendMultipleSearchAccount>().ConfigureAwait(false));
                    var downlines = (await multiResultReader.ReadAsync<UnsuspendMultipleDownline>().ConfigureAwait(false));

                    return searchAccounts.Select(searchAccount =>
                            new UnsuspendMultipleModel(
                                 searchAccount.CustId,
                                 criteria.RoleId,
                                 searchAccount.Username,
                                 searchAccount.Suspended,
                                 downlines.Where(downline => downline.CustId == searchAccount.CustId)));
                }
            }
        }
    }
}
