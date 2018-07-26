namespace Alpha.AccountInfo.DataAccess
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
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

        Task<BalanceInfoModel> GetBalanceInfoModelAsync(ICriteria criteria, byte roleId);

        Task<DownlineListModel> GetDownlineListModelAsync(ICriteria criteria);

        Task<CustomerInfoModel> GetCustomerInfoModelAsync(ICriteria criteria);
    }

    public class AccountInfoDataAccessImpl : AccountInfoDataAccess
    {
        public async Task<BalanceInfoModel> GetBalanceInfoModelAsync(ICriteria criteria, byte roleId)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = await db.ExecuteReaderAsync(criteria.GetSpParams()))
                {
                    if (reader.Read())
                    {
                        switch (roleId)
                        {
                            case 3:
                                return await BuildMasterBalanceInfoModelAsync(reader);
                            case 2:
                                return await BuildAgentBalanceInfoModelAsync(reader);
                            default:
                                return await BuildMemberBalanceInfoModelAsync(reader);
                        }
                    }
                }
            }

            throw new NotImplementedException();
        }

        private async Task<BalanceInfoModel> BuildMasterBalanceInfoModelAsync(IDataReader reader)
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

        private async Task<BalanceInfoModel> BuildAgentBalanceInfoModelAsync(IDataReader reader)
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
                          masterPositionTaking: 0,// TODO Depend on level
                          masterUsedCredit: 0,
                          netPosition: 0,
                          masterCredit_Min: 0,
                          masterCredit_Max: 0,
                          memMaxCredit_Max: 0);
        }

        private async Task<BalanceInfoModel> BuildMemberBalanceInfoModelAsync(IDataReader reader)
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
