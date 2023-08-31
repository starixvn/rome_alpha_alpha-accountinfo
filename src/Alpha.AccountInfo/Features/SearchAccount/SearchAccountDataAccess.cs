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

        Task<DateTime> GetLastTxn(SelectLastTxnCriteria criteria);

        Task<byte> GetClosedReason(SelectClosedReasonCriteria criteria);
    }

    public class SearchAccountDataAccessImpl : SearchAccountDataAccess
    {
        public async Task<DownlineListModel> GetDownlineList(SelectDownlineListByStatusCriteria criteria)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var multiResultReader = await objectDb.QueryMultipleAsync(criteria))
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
                await objectDb.ExecuteNonQueryAsync(command);

                var outputParams = objectDb.GetOutputParams();

                return new Result(
                    int.Parse(outputParams["errcode"].ToString()),
                    outputParams["errmsg"]?.ToString());
            }
        }

        public async Task<Result> UpdateAllDayBalanceTransfer(UpdateAllDayBalanceTransferCommand command)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(command.GetSettingKey()))
            {
                await objectDb.ExecuteNonQueryAsync(command);
                return new Result(int.Parse(command.ErrCode), command.ErrMsg);
            }
        }

        public async Task<bool> IsAllDayBalanceTransfer(SelectIsAllDayBalTransferCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = await db.ExecuteReaderAsync(criteria))
                {
                    return reader.Read() && Convert.ToBoolean(reader["IsAllDayBalTransfer"]);
                }
            }
        }

        public async Task<DateTime> GetLastTxn(SelectLastTxnCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = await db.ExecuteReaderAsync(criteria.GetSpParams()))
                {
                    if (reader.Read())
                    {
                        var lastTxn = reader["MaxTransDate"];

                        return lastTxn == null || lastTxn == DBNull.Value
                            ? DateTime.MinValue
                            : Convert.ToDateTime(lastTxn);
                    }
                }
            }

            return DateTime.MinValue;
        }

        public async Task<byte> GetClosedReason(SelectClosedReasonCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = await db.ExecuteReaderAsync(criteria.GetSpParams()))
                {
                    if (reader.Read())
                    {
                        var closedReason = reader["ClosedReason"];

                        return closedReason == null || closedReason == DBNull.Value
                            ? (byte)0
                            : Convert.ToByte(closedReason);
                    }
                }
            }

            return 0;
        }
    }
}