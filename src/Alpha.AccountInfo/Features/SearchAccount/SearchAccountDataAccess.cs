namespace Alpha.AccountInfo.DataAccess
{
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Fanex.Data;

    public interface SearchAccountDataAccess
    {
        Task<CustomerStatusModel> GetCustomerAllStatus(SelectCustomerAllStatusModelCriteria criteria);

        Task<DownlineListModel> GetDownlineList(SelectDownlineListCriteria criteria);

        Task<Result> UpdateCustomerStatus(UpdateCustomerStatusCommand command);
    }

    public class SearchAccountDataAccessImpl : SearchAccountDataAccess
    {
        public async Task<CustomerStatusModel> GetCustomerAllStatus(SelectCustomerAllStatusModelCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var objectDb = await db.QueryMultipleAsync(criteria.GetSpParams()).ConfigureAwait(false))
                {
                    var customerStatus = (await objectDb.ReadAsync<NonProductStatusModel>().ConfigureAwait(false)).FirstOrDefault();
                    var nonDCSProduct = (await objectDb.ReadAsync<NonDCSProductStatusModel>().ConfigureAwait(false)).FirstOrDefault();
                    var dcsProducts = await objectDb.ReadAsync<DCSProductStatusModel>().ConfigureAwait(false);

                    return new CustomerStatusModel(
                        customerStatus,
                        nonDCSProduct,
                        dcsProducts
                            .GroupBy(product => product.ProductId)
                            .Select(group => group.First()));
                }
            }
        }

        public async Task<DownlineListModel> GetDownlineList(SelectDownlineListCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var objectDb = await db.QueryMultipleAsync(criteria.GetSpParams()).ConfigureAwait(false))
                {
                    var downlines = await objectDb.ReadAsync<DownlineDto>().ConfigureAwait(false);
                    var downlineListModel = (await objectDb.ReadAsync<DownlineListModel>().ConfigureAwait(false)).FirstOrDefault();

                    if (downlineListModel != null)
                    {
                        return new DownlineListModel(downlines, downlineListModel);
                    }

                    return downlineListModel;
                }
            }
        }

        public async Task<Result> UpdateCustomerStatus(UpdateCustomerStatusCommand command)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(command.GetSettingKey()))
            {
                await objectDb.ExecuteNonQueryAsync(command.GetSpParams()).ConfigureAwait(false);

                var outputParams = objectDb.GetOuputParams();

                return new Result(
                    int.Parse(outputParams["errcode"].ToString()),
                    outputParams["errmsg"]?.ToString());
            }
        }
    }
}