namespace Alpha.AccountInfo.DataAccess
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public interface AccountInfoDataAccess
    {
        CustomerAllStatusModel GetCustomerAllStatusModel(ICriteria criteria);

        BalanceInfoModel GetBalanceInfoModel(ICriteria criteria, byte roleId);

        DownlineListModel GetDownlineListModel(ICriteria criteria);

        IEnumerable<OnlineUserModel> GetOnlineList(ICriteria criteria);

        Result UpdateGivenCredit(ICriteria criteria);

        Result ExecuteNonQuery(ICriteria criteria);
    }
}
