namespace Alpha.AccountInfo.DataAccess
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public interface AccountInfoDataAccess
    {
        Task<CustomerAllStatusModel> GetCustomerAllStatusModelAsync(ICriteria criteria);

        Task<BalanceInfoModel> GetBalanceInfoModelAsync(ICriteria criteria);

        Task<DownlineListModel> GetDownlineListModelAsync(ICriteria criteria);

        Task<CustomerInfoModel> GetCustomerInfoModelAsync(ICriteria criteria);
    }

    public class AccountInfoDataAccessImpl : AccountInfoDataAccess
    {
        public async Task<BalanceInfoModel> GetBalanceInfoModelAsync(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var dataset = await db.ExecuteDataSetAsync(criteria.GetSpParams()))
                {
                    var row1 = dataset.Tables[0].Rows[0];
                    var row2 = dataset.Tables[1].Rows[0];
                    return new BalanceInfoModel(
                            custId: Converter.ToInt(row1["custid"]),
                            credit: Converter.ToDecimal(row2["credit"]),
                            masterCash: Converter.ToDecimal(row2["masterCash"]),
                            yesterday_MasterCash: Converter.ToDecimal(row2["yesterday_masterCash"]),
                            masterPositionTaking: Converter.ToDecimal(row2["masterPositionTaking"]),// TODO Depend on level
                            agentCash: Converter.ToDecimal(row2["agentCash"]),
                            yesterday_AgentCash: Converter.ToDecimal(row2["yesterday_agentCash"]),
                            agentPositionTaking: Converter.ToDecimal(row2["agentPositionTaking"]), // TODO Depend on level
                            customerCash: Converter.ToDecimal(row2["customerCash"]),
                            yesterday_CustomerCash: Converter.ToDecimal(row2["yesterday_customerCash"]),
                            customerOutstanding: Converter.ToDecimal(row2["customerOutstanding"]),
                            casinoOutstanding: Converter.ToDecimal(row2["csOutstanding"]),
                            casinoTransfer: Converter.ToDecimal(row2["csTransfer"]),
                            masterUsedCredit: Converter.ToDecimal(row2["masterUsedCredit"]),
                            agentUsedCredit: Converter.ToDecimal(row2["agentUsedCredit"]),
                            netPosition: Converter.ToDecimal(row1["netPosition"]),
                            masterCredit_Min: Converter.ToDecimal(row1["MasterCredit_Min"]),
                            masterCredit_Max: Converter.ToDecimal(row1["MasterCredit_Max"]),
                            agentCredit_Min: Converter.ToDecimal(row1["AgentCredit_Min"]),
                            agentCredit_Max: Converter.ToDecimal(row1["AgentCredit_Max"]),
                            memMaxCredit_Min: Converter.ToDecimal(row1["MemMaxCredit_Min"]),
                            memMaxCredit_Max: Converter.ToDecimal(row1["MemMaxCredit_Max"]),
                            memberMaxCredit: Converter.ToDecimal(row1["memberMaxCredit"])
                        ); 
                }
            }
        }

        public async Task<CustomerInfoModel> GetCustomerInfoModelAsync(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = await db.ExecuteReaderAsync(criteria.GetSpParams()))
                {
                    if (reader.Read())
                    {
                        return new CustomerInfoModel(
                           custId: Converter.ToInt(reader["custid"]),
          roleId: (byte)reader["roleid"],
          userName: Converter.ToString2(reader["username"]),
          userName2: Converter.ToString2(reader["username2"]),
          firstName: Converter.ToString2(reader["firstname"]),
          lastName: Converter.ToString2(reader["lastname"]),
          phone: Converter.ToString2(reader["phone"]),
          mobilePhone: Converter.ToString2(reader["mobilephone"]),
          fax: Converter.ToString2(reader["fax"]),
          closedReason: Converter.ToBool(reader["ClosedReason"]),
          enabled: Converter.ToBool(reader["enabled"]),
          uplineEnabled: Converter.ToBool(reader["upLineEnabled"]),
          closed: Converter.ToBool(reader["closed"]),
          suspended: Converter.ToBool(reader["suspended"]),
          createDate: Converter.ToDateTime(reader["creatdate"]),
          lastLogged: Converter.ToDateTime(reader["lastlogged"]),
          lastTxn: Converter.ToDateTime(reader["lastTxn"]),
          lastLoginIP: Converter.ToString2(reader["lastloginip"]),
          pwdExpiry: Converter.ToDateTime(reader["pwdexpiry"]),
          memberId: Converter.ToInt(reader["memberID"]),
         memberName: Converter.ToString2(reader["memberName"]),
         agentId: Converter.ToInt(reader["agentID"]),
         agentName: Converter.ToString2(reader["agentName"]),
         masterId: Converter.ToInt(reader["custid"]),
         masterName: Converter.ToString2(reader["masterName"]),
         superId: Converter.ToInt(reader["superID"]),
         superName: Converter.ToString2(reader["superName"])
                            );
                    }

                }
            }

            throw new NotImplementedException();
        }

        public async Task<CustomerAllStatusModel> GetCustomerAllStatusModelAsync(ICriteria criteria)
        {
            throw new NotImplementedException();
        }


        public async Task<DownlineListModel> GetDownlineListModelAsync(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var dataset = await db.ExecuteDataSetAsync(criteria.GetSpParams()))
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
