namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Fanex.Data.Repository;

    public interface AccountInfoService
    {
       CustomerInfoModel GetCustomerInfoModelAsync(string username, int uplineId);

        BalanceInfoModel GetBalanceInfoModelAsync(int custId, byte roleId);

        DownlineListModel GetDownlineListModelAsync(int custId);

       CustomerAllStatusModel GetCustomerAllStatusModelAsync(int custId);
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
    }
}
