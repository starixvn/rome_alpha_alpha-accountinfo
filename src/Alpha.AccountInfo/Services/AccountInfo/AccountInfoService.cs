namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
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

        Result UpdateGivenCredit(GivenCreditParam param);
    }

    public class AccountInfoServiceImpl : AccountInfoService
    {
        private readonly IDynamicRepository repository;
        private readonly AccountInfoDataAccess accountInfoDataAccess;

        public AccountInfoServiceImpl(IDynamicRepository dynamicRepository, AccountInfoDataAccess accountInfoDataAccess)
        {
            repository = dynamicRepository;
            this.accountInfoDataAccess = accountInfoDataAccess;
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

        public Result UpdateGivenCredit(GivenCreditParam param)
            => accountInfoDataAccess.UpdateGivenCredit(
                new UpdateGivenCreditCriteria(
                    param.CustId,
                    param.RoleId,
                    param.CreditAmount,
                    param.ActorId,
                    param.ActorRoleId,
                    param.SubAccountId,
                    param.Language,
                    param.IP));
    }
}
