namespace Alpha.AccountInfo.DataAccess
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Fanex.Data;

    public interface SearchAccountDataAccess
    {
        Task<DownlineListModel> GetDownlineList(SelectDownlineListByStatusCriteria criteria);

        Task<Result> UpdateCustomerStatus(UpdateCustomerStatusCommand command);

        Task<Result> UpdateAllDayBalanceTransfer(UpdateAllDayBalanceTransferCommand command);

        Task<bool> IsAllDayBalanceTransfer(SelectIsAllDayBalTransferCriteria criteria);
    }

    public class SearchAccountDataAccessImpl : SearchAccountDataAccess
    {
        public async Task<DownlineListModel> GetDownlineList(SelectDownlineListByStatusCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = await objectDb.QueryMultipleAsync(criteria.GetSpParams()))
                {
                    var downlineListModel = (await multiResultReader.ReadAsync<DownlineListModel>()).FirstOrDefault();
                    var downlines = await multiResultReader.ReadAsync<DownlineDto>();
                  
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
                await objectDb.ExecuteNonQueryAsync(command.GetSpParams());

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
                await objectDb.ExecuteNonQueryAsync(command.GetSpParams());

                var outputParams = objectDb.GetOuputParams();

                return new Result(
                    int.Parse(outputParams["errcode"].ToString()),
                    outputParams["errmsg"]?.ToString());
            }
        }

        public async Task<bool> IsAllDayBalanceTransfer(SelectIsAllDayBalTransferCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = await db.ExecuteReaderAsync(criteria.GetSpParams()))
                {
                    return reader.Read() && Convert.ToBoolean(reader["IsAllDayBalTransfer"]);
                }
            }
        }
    }
}