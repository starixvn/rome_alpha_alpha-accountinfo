namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;
    using Requests.ProductStatus;
    using System.Threading.Tasks;

    public class AccountInfoHandler :
        IAsyncRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IAsyncRequestHandler<BalanceInfoQuery, BalanceInfoViewModel>,
        IAsyncRequestHandler<DownlineListQuery, DownlineListViewModel>
       // IAsyncRequestHandler<ProductQuery, ProductViewModel>
    {
        private readonly AccountInfoService accountInfoService;

        public AccountInfoHandler(AccountInfoService accountInfoService)
        {
            this.accountInfoService = accountInfoService;
        }

        public async Task<CustomerInfoViewModel> Handle(CustomerInfoQuery message)
        {
            var model = await accountInfoService.GetCustomerInfoModelAsync(message.UserName, message.UplineId);

            return new CustomerInfoViewModel(message.UplineLevel.Value, model);
        }

        public async Task<BalanceInfoViewModel> Handle(BalanceInfoQuery message)
        {
            var model = await accountInfoService.GetBalanceInfoModelAsync(message.CustId);

            return new BalanceInfoViewModel(message.RoleId, model);
        }

        public async Task<DownlineListViewModel> Handle(DownlineListQuery message)
        {
            var model = await accountInfoService.GetDownlineListModelAsync(message.CustId);

            return new DownlineListViewModel(model);
        }
    }
}