namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using Alpha.AccountInfo.Services.ProductStatus;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public interface AccountInfoService
    {
       IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal);

       CustomerInfoModel GetCustomerInfoModelAsync(string username, int uplineId);

        BalanceInfoModel GetBalanceInfoModelAsync(int custId, byte roleId);

        DownlineListModel GetDownlineListModelAsync(int custId);

       CustomerAllStatusModel GetCustomerAllStatusModelAsync(int custId);

        IEnumerable<OnlineListModel> GetOnlineList(int custid);

        Result UpdateGivenCredit(UpdateGivenCreditParam param);

        Result UpdateOutright(UpdateOutrightParam param);

        Result UpdateClosed(UpdateClosedParam param);

        Result UpdateSuspended(UpdateSuspendedParam param);

        Result UpdateAutoMasterPT(UpdateAutoMasterPTParam param);

        Result UpdateDoubleComm(UpdateDoubleCommParam param);
    }

    public class AccountInfoServiceImpl : AccountInfoService
    {
        private readonly IDynamicRepository repository;
        private readonly AccountInfoDataAccess accountInfoDataAccess;
        private readonly RnGCasinoService rnGCasinoService;

        public AccountInfoServiceImpl(
            IDynamicRepository dynamicRepository, 
            AccountInfoDataAccess accountInfoDataAccess,
            RnGCasinoService rnGCasinoService)
        {
            repository = dynamicRepository;
            this.accountInfoDataAccess = accountInfoDataAccess;
            this.rnGCasinoService = rnGCasinoService;
        }

        public IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal)
            => repository.Fetch<UsernameSuggestion>(new SelectAutoSuggestion(actorId, suggestionQuery, isInternal));

        public BalanceInfoModel GetBalanceInfoModelAsync(int custId, byte roleId)
            => accountInfoDataAccess.GetBalanceInfoModelAsync(
               new SelectBalanceInfoCriteria(custId), roleId);

        public CustomerAllStatusModel GetCustomerAllStatusModelAsync(int custId)
            => accountInfoDataAccess.GetCustomerAllStatusModelAsync(
                new SelectCustomerAllStatusModelCriteria(custId));

        public CustomerInfoModel GetCustomerInfoModelAsync(string username, int uplineId)
            =>  repository.Get<CustomerInfoModel>(new SelectCustomerInfoCriteria(username, uplineId));

        public DownlineListModel GetDownlineListModelAsync(int custId)
            =>  accountInfoDataAccess.GetDownlineListModelAsync(new SelectDownlineListCriteria(custId));

        public IEnumerable<OnlineListModel> GetOnlineList(int custid)
            =>  repository.Fetch<OnlineListModel>(new SelectOnlineListCriteria(custid));

        public Result UpdateGivenCredit(UpdateGivenCreditParam param)
            => accountInfoDataAccess.UpdateGivenCredit(
                new UpdateGivenCreditCriteria(
                    param.SearchAccountId,
                    param.RoleId,
                    param.CreditAmount,
                    param.ActorId,
                    param.ActorRoleId,
                    param.SubAccountId,
                    param.Language,
                    param.IP));

        public Result UpdateOutright(UpdateOutrightParam param)
            => accountInfoDataAccess.ExecuteNonQuery(
               new UpdateOutrightCriteria(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.DisableOutright,
                   param.Language,
                   param.IP));

        public Result UpdateClosed(UpdateClosedParam param)
        {
            var result = accountInfoDataAccess.ExecuteNonQuery(
                new UpdateClosedCriteria(
                    param.SearchAccountId,
                    param.ActorId,
                    param.SubAccountId,
                    param.Closed,
                    param.Language,
                    param.IP));

            if(result.IsSuccess && param.SyncedCasino)
            {
                rnGCasinoService.Update3rdRnGCasinoClosed(
                    param.SearchAccountId,
                    param.Closed,
                    param.IP,
                    param.ActorId,
                    param.ActorName);
            }

            return result;
        }

        public Result UpdateSuspended(UpdateSuspendedParam param)
        {
            var result = accountInfoDataAccess.ExecuteNonQuery(
                new UpdateSuspendedCriteria(
                    param.SearchAccountId,
                    param.ActorId,
                    param.SubAccountId,
                    param.Suspended,
                    param.Language,
                    param.IP));

            if (result.IsSuccess && param.SyncedCasino)
            {
                rnGCasinoService.Update3rdRnGCasinoSuspended(
                    param.SearchAccountId,
                    param.Suspended,
                    param.IP,
                    param.ActorId,
                    param.ActorName);
            }

            return result;
        }
        public Result UpdateAutoMasterPT(UpdateAutoMasterPTParam param)
            => accountInfoDataAccess.ExecuteNonQuery(
               new UpdateAutoMasterPTCriteria(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.DisableAutoMasterPT,
                   param.Language,
                   param.IP));

        public Result UpdateDoubleComm(UpdateDoubleCommParam param)
            => accountInfoDataAccess.ExecuteNonQuery(
                new UpdateDoubleCommCriteria(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.DoubleComm,
                   param.Language,
                   param.IP));
    }
}
