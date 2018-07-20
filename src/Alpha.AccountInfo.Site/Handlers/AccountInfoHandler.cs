namespace Alpha.AccountInfo.Site.Handlers
{
    using MediatR;
    using Requests.ProductStatus;
    using Services.AccountInfo;
    using System.Threading.Tasks;
    using ViewModels.AccountInfo;

    public class AccountInfoHandler :
        IAsyncRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IAsyncRequestHandler<DownlineListQuery, DownlineListViewModel>,
        IAsyncRequestHandler<MasterBalanceInfoQuery, MasterBalanceInfoViewModel>,
        IAsyncRequestHandler<AgentBalanceInfoQuery, AgentBalanceInfoViewModel>,
        IAsyncRequestHandler<MemberBalanceInfoQuery, MemberBalanceInfoViewModel>
       // IAsyncRequestHandler<ProductQuery, ProductViewModel>
    {
        private readonly AccountInfoService accountInfoService;

        public AccountInfoHandler(AccountInfoService accountInfoService)
        {
            this.accountInfoService = accountInfoService;
        }

        public async Task<CustomerInfoViewModel> Handle(CustomerInfoQuery message)
            => new CustomerInfoViewModel(
                message.UplineLevel.Value, 
                await accountInfoService.GetCustomerInfoModelAsync(message.UserName, message.UplineId));

        public async Task<DownlineListViewModel> Handle(DownlineListQuery message)
            => new DownlineListViewModel(await accountInfoService.GetDownlineListModelAsync(message.CustId));

        public async Task<MasterBalanceInfoViewModel> Handle(MasterBalanceInfoQuery message)
            => new MasterBalanceInfoViewModel(message, await accountInfoService.GetBalanceInfoModelAsync(message.CustId));

        public async Task<AgentBalanceInfoViewModel> Handle(AgentBalanceInfoQuery message)
            => new AgentBalanceInfoViewModel(message, await accountInfoService.GetBalanceInfoModelAsync(message.CustId));

        public async Task<MemberBalanceInfoViewModel> Handle(MemberBalanceInfoQuery message)
         => new MemberBalanceInfoViewModel(message, await accountInfoService.GetBalanceInfoModelAsync(message.CustId));
    }
}