namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Fanex.Data.Repository;
    using System.Threading.Tasks;

    public interface AccountInfoService
    {
        Task<CustomerInfoModel> GetCustomerInfoModelAsync(string username, int uplineId);

        Task<BalanceInfoModel> GetBalanceInfoModelAsync(int custId);

        Task<DownlineListModel> GetDownlineListModelAsync(int custId);

        Task<CustomerAllStatusModel> GetCustomerAllStatusModelAsync(int custId);
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

        public async Task<BalanceInfoModel> GetBalanceInfoModelAsync(int custId)
            => await accountInfoDataAccess.GetBalanceInfoModelAsync(
               new SelectBalanceInfoCriteria(custId));

        public async Task<CustomerAllStatusModel> GetCustomerAllStatusModelAsync(int custId)
            => await accountInfoDataAccess.GetCustomerAllStatusModelAsync(
                new SelectCustomerAllStatusModelCriteria(custId));

        public async Task<CustomerInfoModel> GetCustomerInfoModelAsync(string username, int uplineId)
        {
          //  var model = await accountInfoDataAccess.GetCustomerInfoModelAsync(new SelectCustomerInfoCriteria(username, uplineId));
            // repository.GetAsync<CustomerInfoModel>(new SelectCustomerInfoCriteria(username, uplineId));
           // var model = await accountInfoDataAccess.GetCustomerInfoModelAsync(new SelectCustomerInfoCriteria(username, uplineId));
            return await accountInfoDataAccess.GetCustomerInfoModelAsync(new SelectCustomerInfoCriteria(username, uplineId));
        }

        public async Task<DownlineListModel> GetDownlineListModelAsync(int custId)
            => await accountInfoDataAccess.GetDownlineListModelAsync(
                new SelectDownlineListCriteria(custId));
    }
}
