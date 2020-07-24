namespace Alpha.AccountInfo.DataAccess
{
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Fanex.Data;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public interface SearchAccountDataAccess
    {
        Task<CustomerStatusModel> GetCustomerStatus(SelectCustomerAllStatusModelCriteria criteria);

        Task<DownlineListModel> GetDownlineList(SelectDownlineListByStatusCriteria criteria);

        Task<Result> UpdateCustomerStatus(UpdateCustomerStatusCommand command);

        Task<Result> UpdateAllDayBalanceTransfer(UpdateAllDayBalanceTransferCommand command);
    }

    public class SearchAccountDataAccessImpl : SearchAccountDataAccess
    {
        public async Task<CustomerStatusModel> GetCustomerStatus(SelectCustomerAllStatusModelCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = await objectDb.QueryMultipleAsync(criteria.GetSpParams()).ConfigureAwait(false))
                {
                    var customerStatus = (await multiResultReader.ReadAsync<NonProductStatusModel>().ConfigureAwait(false)).FirstOrDefault();
                    var nonDCSProduct = (await multiResultReader.ReadAsync<NonDCSProductStatusModel>().ConfigureAwait(false)).FirstOrDefault();
                    var dcsProducts = await multiResultReader.ReadAsync<DCSProductStatusModel>().ConfigureAwait(false);

                    return new CustomerStatusModel(
                        customerStatus,
                        nonDCSProduct,
                        dcsProducts
                            .GroupBy(product => product.ProductId)
                            .Select(group => group.First()));
                }
            }
        }

        public async Task<DownlineListModel> GetDownlineList(SelectDownlineListByStatusCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = await objectDb.QueryMultipleAsync(criteria.GetSpParams()).ConfigureAwait(false))
                {
                    var downlineListModel = (await multiResultReader.ReadAsync<DownlineListModel>().ConfigureAwait(false)).FirstOrDefault();
                    var downlines = await multiResultReader.ReadAsync<DownlineDto>().ConfigureAwait(false);
                  
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

        public async Task<Result> UpdateAllDayBalanceTransfer(UpdateAllDayBalanceTransferCommand command)
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