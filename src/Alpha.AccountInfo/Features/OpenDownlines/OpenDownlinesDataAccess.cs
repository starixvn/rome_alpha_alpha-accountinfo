namespace Alpha.AccountInfo.Features.OpenDownlines
{
    using Fanex.Data;
    using Models;
    using Params;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface OpenDownlinesDataAccess
    {
        Task<OpenViewListModel> GetDownlinesInfoStatus(SelectCloseInfoDetailCriteria criteria);

        Task<IEnumerable<OpenMultipleModel>> GetOpenMultipleModel(SelectCloseInfoMultipleCriteria criteria);
    }

    public class OpenDownlinesDataAccessImpl : OpenDownlinesDataAccess
    {
        public async Task<OpenViewListModel> GetDownlinesInfoStatus(SelectCloseInfoDetailCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                IEnumerable<OpenDownlineDto> downlines;
                using (var multiResultReader = await objectDb.QueryMultipleAsync(criteria.GetSpParams()).ConfigureAwait(false))
                {
                    downlines = await multiResultReader.ReadAsync<OpenDownlineDto>().ConfigureAwait(false);
                }

                var outputParams = objectDb.GetOuputParams();
                return new OpenViewListModel(downlines.GroupBy(item => item.RoleId).Select(group =>
                {
                    return new OpenDownlineByLevelModel(group.Key, group);
                }), (int)outputParams["totalrows"]);
            }
        }

        public async Task<IEnumerable<OpenMultipleModel>> GetOpenMultipleModel(SelectCloseInfoMultipleCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = await objectDb.QueryMultipleAsync(criteria.GetSpParams()).ConfigureAwait(false))
                {
                    var searchAccounts = (await multiResultReader.ReadAsync<OpenSearchAccountModel>().ConfigureAwait(false));
                    var downlines = (await multiResultReader.ReadAsync<OpenDownlineModel>().ConfigureAwait(false));

                    return searchAccounts.Select(searchAccount =>
                            new OpenMultipleModel(
                                 searchAccount.SearchAccountId,
                                 searchAccount.Username,
                                 searchAccount.IsClosed,
                                 downlines
                                 .Where(downline => downline.SearchAccountId == searchAccount.SearchAccountId)
                                 .OrderByDescending(downline => downline.DownlineRoleId).ToList()));
                }
            }
        }
    }
}