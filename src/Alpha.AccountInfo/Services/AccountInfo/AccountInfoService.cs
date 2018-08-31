namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using System.Collections.Generic;

    public interface AccountInfoService
    {
       IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal);

       CustomerInfoModel GetCustomerInfoModel(string username, int uplineId);

        IBalanceInfoModel GetBalanceInfoModel(int custId, byte roleId);

        DownlineListModel GetDownlineListModel(int custId);

        CustomerAllStatusModel GetCustomerAllStatusModel(int custId);

        IEnumerable<OnlineUserModel> GetOnlineList(int custid);

        Result UpdateGivenCredit(UpdateGivenCreditParam param);

        Result UpdateCustomerStatus(UpdateCustomerStatusParam param);
    }
}
