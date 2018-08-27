namespace Alpha.AccountInfo.DataAccess
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class AccountInfoDataAccessImpl : AccountInfoDataAccess
    {
        private readonly int failErrorCode = 100;

        public CustomerAllStatusModel GetCustomerAllStatusModel(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var dataset = db.ExecuteDataSet(criteria.GetSpParams()))
                {
                    var customerStatusRow = dataset.Tables[0].Rows[0];
                    var nonDCSProductRow = dataset.Tables[1].Rows[0];
                    var dcsProductTable = dataset.Tables[2];

                    var customerStatus = new CustomerStatusModel(
                             hasDownline: Converter.ToBool(customerStatusRow["HasDownline"]),
                             disableAutoMPT: Converter.ToBool(customerStatusRow["DisableAutoMPT"]),
                             closed: Converter.ToBool(customerStatusRow["closed"]),
                             uplineClosed: Converter.ToBool(customerStatusRow["upLineClosed"]),
                             suspended: Converter.ToBool(customerStatusRow["suspended"]),
                             uplineSuspend: Converter.ToBool(customerStatusRow["upLineSuspend"]),
                             transferOption: Converter.ToString2(customerStatusRow["TransferOption"]),
                             doubleCommLosing: Converter.ToBool(customerStatusRow["DoubleCommLosing"]),
                             uplineDoubleCommLosing: Converter.ToBool(customerStatusRow["uplineDoubleCommLosing"]),
                             disableOutright: Converter.ToBool(customerStatusRow["DisableOutright"]),
                             uplineDisableOutright: Converter.ToBool(customerStatusRow["upLineDisableOutright"]));

                    var nonDCSProduct = new NonDCSProductModel(
                             syncCasino: Converter.ToBool(nonDCSProductRow["SyncCasino"]),
                             disableCasino: Converter.ToBool(nonDCSProductRow["DisableCasino"]),
                             uplineDisableCS: Converter.ToBool(nonDCSProductRow["uplineDisableCS"]),
                             disableNumber: Converter.ToBool(nonDCSProductRow["DisableNumber"]),
                             uplineDisableNumber: Converter.ToBool(nonDCSProductRow["uplineDisabledNumber"]),
                             disableEsports: Converter.ToBool(nonDCSProductRow["DisableEsports"]),
                             uplineDisableEsports: Converter.ToBool(nonDCSProductRow["uplineDisableEsports"]),
                             disableColossusBet: Converter.ToBool(nonDCSProductRow["DisableColossusBet"]),
                             hideColossusBet: Converter.ToBool(nonDCSProductRow["HideColossusBet"]),
                             uplineDisableColossusBet: Converter.ToBool(nonDCSProductRow["uplineDisableColossusBet"]),
                             hideKeno: Converter.ToBool(nonDCSProductRow["HideKeno"]));

                    var dcsProducts = new List<DCSProductModel>();

                    foreach(DataRow row in dcsProductTable.Rows)
                    {
                        dcsProducts.Add(
                            new DCSProductModel(
                                  productId: Converter.ToInt(row["ProductID"]),
                                  sportType: Converter.ToInt(row["SportType"]),
                                  betType: Converter.ToInt(row["BetType"]),
                                  isSync: Converter.ToBool(row["IsSync"]),
                                  isHidden: Converter.ToBool(row["isHidden"]),
                                  isDisabled: Converter.ToBool(row["isDisabled"]),
                                  isSyncedUpline: Converter.ToBool(row["isSyncedUpline"]),
                                  isHiddenUpline: Converter.ToBool(row["isHiddenUpline"]),
                                  isDisabledUpline: Converter.ToBool(row["isDisabledUpline"]),
                                  hasDownline: Converter.ToBool(row["HasDownline"])));
                    }

                    return new CustomerAllStatusModel(
                        customerStatus: customerStatus,
                        nonDCSProduct: nonDCSProduct,
                        dcsProducts: dcsProducts.GroupBy(product => product.ProductId).Select(group => group.First()));
                }
            }
        }

        public DownlineListModel GetDownlineListModel(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var dataset = db.ExecuteDataSet(criteria.GetSpParams()))
                {
                    var downlineListTable = dataset.Tables[0];
                    var totalDownlineRow = dataset.Tables[1].Rows[0];

                    var downlineList = new List<DownlineInfoModel>();
                    foreach(DataRow row in downlineListTable.Rows)
                    {
                        downlineList.Add(new DownlineInfoModel(
                            Converter.ToString2(row["UserName"]),
                             Converter.ToString2(row["ClosedReason"])));
                    }


                    return new DownlineListModel(
                          downlines: downlineList,
                          totalAgActive: Converter.ToInt(totalDownlineRow["TotalAgActive"]),
                          totalAgClosed: Converter.ToInt(totalDownlineRow["TotalAgClosed"]),
                          totalAgSuspended: Converter.ToInt(totalDownlineRow["TotalAgSuspended"]),
                          totalMemActive: Converter.ToInt(totalDownlineRow["TotalMemActive"]),
                          totalMemClosed: Converter.ToInt(totalDownlineRow["TotalMemClosed"]),
                          totalMemSuspended: Converter.ToInt(totalDownlineRow["TotalMemSuspended"])
                        );
                }
            }
        }

        public IEnumerable<OnlineUserModel> GetOnlineList(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    var onlineList = new List<OnlineUserModel>();
                    while (reader.Read())
                    {
                        onlineList.Add(new OnlineUserModel(
                            Converter.ToString2(reader["ip"]),
                             Converter.ToDateTime(reader["reload_time"]).ToString("dd MMM hh:mm tt")));
                    }

                    return onlineList;
                }
            }
        }

        public Result UpdateGivenCredit(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();

                var creditProblem = Converter.ToBool(outputParams["CreditProblem"]);

                return creditProblem ?
                    Result.CreateErrorResult(failErrorCode, Converter.ToString2(outputParams["ErrMsg"])) :
                    Result.CreateSuccessResult(Resources.UpdateSuccessfully);
            }
        }

        public Result ExecuteNonQuery(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();

                var success = Convert.ToInt32(outputParams["errcode"]).Equals(0);

                return success ? 
                    Result.CreateSuccessResult(Resources.UpdateSuccessfully) :
                    Result.CreateErrorResult(failErrorCode, Converter.ToString2(outputParams["ErrMsg"]));
            }
        }
    }
}
