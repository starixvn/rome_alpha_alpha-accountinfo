namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.Products.Core.Models;
    using MediatR;
    using Requests.ProductStatus;
    using Services.AccountInfo;
    using System.Threading.Tasks;
    using ViewModels.AccountInfo;
    using static Alpha.AccountInfo.Site.Integration.StubCustomerProductStatusGetter;

    public class AccountInfoHandler :
        IAsyncRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IAsyncRequestHandler<DownlineListQuery, DownlineListViewModel>,
        IAsyncRequestHandler<ProductQuery, ProductViewModel>
    {
        private readonly AccountInfoService accountInfoService;

        public AccountInfoHandler(AccountInfoService accountInfoService)
        {
            this.accountInfoService = accountInfoService;
        }

        public async Task<CustomerInfoViewModel> Handle(CustomerInfoQuery message)
        {
            var custormerinfo = await accountInfoService.GetCustomerInfoModelAsync(message.UserName, message.ActorId);
            var balanceinfo = await accountInfoService.GetBalanceInfoModelAsync(custormerinfo.CustId, custormerinfo.RoleId);

            return new CustomerInfoViewModel(message, custormerinfo, balanceinfo);
        }


        public async Task<DownlineListViewModel> Handle(DownlineListQuery message)
            => new DownlineListViewModel(await accountInfoService.GetDownlineListModelAsync(message.CustId));

        public async Task<ProductViewModel> Handle(ProductQuery message)
        {
            var customerStatus = await accountInfoService.GetCustomerAllStatusModelAsync(message.CustId);

            int subAccountId = 0;
            var actor = new Customer(1, subAccountId, "Actor_Name", 4, GetActorProductStatus());
            var upline = new Customer(3, subAccountId, "Upline_Name", 4, GetUplineProductStatus());
            var searchAccount = new Customer(5, subAccountId, "Search_Account_Name", 3, GetSearchAccountProductStatus(), upline);

            var setting = new SettingViewModel(message, customerStatus, actor, searchAccount);
            var status = new StatusViewModel(message, customerStatus, actor, searchAccount);
            return new ProductViewModel(status, setting);
        }
    }
}