namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using System.Collections.Generic;

    public interface AccountInfoService
    {
       IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal);

       CustomerInfoModel GetCustomerInfoModelAsync(string username, int uplineId);

        BalanceInfoModel GetBalanceInfoModelAsync(int custId, byte roleId);

        DownlineListModel GetDownlineListModelAsync(int custId);

       CustomerAllStatusModel GetCustomerAllStatusModelAsync(int custId);

        IEnumerable<OnlineUserModel> GetOnlineList(int custid);

        Result UpdateGivenCredit(UpdateGivenCreditParam param);

        Result UpdateOutright(UpdateOutrightParam param);

        Result UpdateClosed(UpdateClosedParam param);

        Result UpdateSuspended(UpdateSuspendedParam param);

        Result UpdateAutoMasterPT(UpdateAutoMasterPTParam param);

        Result UpdateDoubleComm(UpdateDoubleCommParam param);
    }
}
