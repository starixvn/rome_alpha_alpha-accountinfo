namespace Alpha.AccountInfo.DataAccess
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface AccountInfoDataAccess
    {
       CustomerAllStatusModel GetCustomerAllStatusModelAsync(ICriteria criteria);

        BalanceInfoModel GetBalanceInfoModelAsync(ICriteria criteria, byte roleId);

        DownlineListModel GetDownlineListModelAsync(ICriteria criteria);
    }

    public class AccountInfoDataAccessImpl : AccountInfoDataAccess
    {
        public BalanceInfoModel GetBalanceInfoModelAsync(ICriteria criteria, byte roleId)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    if (reader.Read())
                    {
                        switch (roleId)
                        {
                            case 3:
                                return BuildMasterBalanceInfoModelAsync(reader);
                            case 2:
                                return BuildAgentBalanceInfoModelAsync(reader);
                            default:
                                return BuildMemberBalanceInfoModelAsync(reader);
                        }
                    }
                }
            }

            throw new NotImplementedException();
        }

        private BalanceInfoModel BuildMasterBalanceInfoModelAsync(IDataReader reader)
        {
            return new BalanceInfoModel(
                          custId: Converter.ToInt(reader["custid"]),
                          credit: Converter.ToDecimal(reader["credit"]),
                          masterCash: Converter.ToDecimal(reader["masterCash"]),
                          yesterday_MasterCash: Converter.ToDecimal(reader["yesterday_masterCash"]),
                          masterCredit_Max: Converter.ToDecimal(reader["MasterCredit_Max"]),
                           masterCredit_Min: Converter.ToDecimal(reader["MasterCredit_Min"]),
                          masterPositionTaking: Converter.ToDecimal(reader["masterPositionTaking"]),
                          agentCash: Converter.ToDecimal(reader["agentCash"]),
                          yesterday_AgentCash: Converter.ToDecimal(reader["yesterday_agentCash"]),
                          customerCash: Converter.ToDecimal(reader["customerCash"]),
                          yesterday_CustomerCash: Converter.ToDecimal(reader["yesterday_customerCash"]),
                          customerOutstanding: Converter.ToDecimal(reader["customerOutstanding"]),
                          casinoOutstanding: Converter.ToDecimal(reader["csOutstanding"]),
                          casinoTransfer: Converter.ToDecimal(reader["csTransfer"]),
                          masterUsedCredit: Converter.ToDecimal(reader["masterUsedCredit"]),

                          agentPositionTaking: 0,
                          agentUsedCredit: 0,
                          netPosition: 0,
                          agentCredit_Min: 0,
                          agentCredit_Max: 0,
                          memMaxCredit_Min: 0,
                          memMaxCredit_Max: 0,
                          memberMaxCredit: 0);
        }

        private BalanceInfoModel BuildAgentBalanceInfoModelAsync(IDataReader reader)
        {
            return new BalanceInfoModel(
                          custId: Converter.ToInt(reader["custid"]),
                          credit: Converter.ToDecimal(reader["credit"]),
                          agentCredit_Min: Converter.ToDecimal(reader["AgentCredit_Min"]),
                          agentCredit_Max: Converter.ToDecimal(reader["AgentCredit_Max"]),
                          memMaxCredit_Min: Converter.ToDecimal(reader["MemMaxCredit_Min"]),
                          agentCash: Converter.ToDecimal(reader["agentCash"]),
                          yesterday_AgentCash: Converter.ToDecimal(reader["yesterday_agentCash"]),
                          agentPositionTaking: Converter.ToDecimal(reader["agentPositionTaking"]),
                          customerCash: Converter.ToDecimal(reader["customerCash"]),
                          yesterday_CustomerCash: Converter.ToDecimal(reader["yesterday_customerCash"]),
                          customerOutstanding: Converter.ToDecimal(reader["customerOutstanding"]),
                          casinoOutstanding: Converter.ToDecimal(reader["csOutstanding"]),
                          casinoTransfer: Converter.ToDecimal(reader["csTransfer"]),
                          agentUsedCredit: Converter.ToDecimal(reader["agentUsedCredit"]),
                          memberMaxCredit: Converter.ToDecimal(reader["memberMaxCredit"]),

                          masterCash: 0,
                          yesterday_MasterCash: 0,
                          masterPositionTaking: 0,
                          masterUsedCredit: 0,
                          netPosition: 0,
                          masterCredit_Min: 0,
                          masterCredit_Max: 0,
                          memMaxCredit_Max: 0);
        }

        private BalanceInfoModel BuildMemberBalanceInfoModelAsync(IDataReader reader)
        {
            return new BalanceInfoModel(
                          custId: Converter.ToInt(reader["custid"]),
                          credit: Converter.ToDecimal(reader["credit"]),
                          netPosition: Converter.ToDecimal(reader["netPosition"]),
                          memMaxCredit_Max: Converter.ToDecimal(reader["MemMaxCredit_Max"]),
                          customerCash: Converter.ToDecimal(reader["Cash"]),
                          yesterday_CustomerCash: Converter.ToDecimal(reader["yesterday_Cash"]),
                          customerOutstanding: Converter.ToDecimal(reader["outstanding"]),
                          casinoOutstanding: Converter.ToDecimal(reader["csOutstanding"]),
                          casinoTransfer: Converter.ToDecimal(reader["csTransfer"]),

                          masterCash: 0,
                          yesterday_MasterCash: 0,
                          masterPositionTaking: 0,
                          agentCash: 0,
                          yesterday_AgentCash: 0,
                          agentPositionTaking: 0,
                          masterUsedCredit: 0,
                          agentUsedCredit: 0,
                          masterCredit_Min: 0,
                          masterCredit_Max: 0,
                          agentCredit_Min: 0,
                          agentCredit_Max: 0,
                          memMaxCredit_Min: 0, 
                          memberMaxCredit: 0);
        }

        public CustomerAllStatusModel GetCustomerAllStatusModelAsync(ICriteria criteria)
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
                             transferOption: Converter.ToBool(customerStatusRow["TransferOption"]),
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
                        dcsProducts: dcsProducts);
                }
            }

            throw new NotImplementedException();
        }

        public DownlineListModel GetDownlineListModelAsync(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var dataset = db.ExecuteDataSet(criteria.GetSpParams()))
                {
                    var downlineListTable = dataset.Tables[0];
                    var row2 = dataset.Tables[1].Rows[0];

                    var downlineList = new List<DownlineInfoModel>();
                    foreach(DataRow row in downlineListTable.Rows)
                    {
                        downlineList.Add(new DownlineInfoModel(
                            Converter.ToString2(row["UserName"]),
                             Converter.ToString2(row["ClosedReason"])));
                    }


                    return new DownlineListModel(
                          downlines: downlineList,
                          totalAgActive: Converter.ToInt(row2["TotalAgActive"]),
                          totalAgClosed: Converter.ToInt(row2["TotalAgClosed"]),
                          totalAgSuspended: Converter.ToInt(row2["TotalAgSuspended"]),
                          totalMemActive: Converter.ToInt(row2["TotalMemActive"]),
                          totalMemClosed: Converter.ToInt(row2["TotalMemClosed"]),
                          totalMemSuspended: Converter.ToInt(row2["TotalMemSuspended"])
                        );
                }
            }
        }
    }
}
