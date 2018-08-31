namespace Alpha.AccountInfo.DataAccess
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Fanex.Data.Repository;
    using System.Collections;
    using System.Collections.Generic;

    public interface AccountInfoDataAccess
    {
        CustomerAllStatusModel GetCustomerAllStatus(ICriteria criteria);

        DownlineListModel GetDownlineList(ICriteria criteria);

        IEnumerable<OnlineUserModel> GetOnlineList(ICriteria criteria);

        Result UpdateGivenCredit(ICriteria criteria);

        Result ExecuteNonQuery(string settingKey, Hashtable param);
    }
}
