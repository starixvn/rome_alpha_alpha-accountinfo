namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using Alpha.Utilities.IP;
    using MediatR;
    using Services.AccountInfo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SearchAccountHandler :
        IAsyncRequestHandler<SearchAccountQuery, AccountInfoViewModel>,
        IAsyncRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IAsyncRequestHandler<DownlineListQuery, DownlineListViewModel>,
        IAsyncRequestHandler<OnlineListQuery, IEnumerable<OnlineUserModel>>,
        IAsyncRequestHandler<GivenCreditQuery, GivenCreditViewModel>,
        IAsyncRequestHandler<CustomerNonProductStatusCommand, Result>,
        IAsyncRequestHandler<GivenCreditCommand, Result>,
        IAsyncRequestHandler<UpdateAllBalanceTransferCommand, Result>
    {
        private readonly SearchAccountService accountInfoService;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;
        private readonly Func<bool> getUpdateCreditReplicateServer;
        private readonly IPService ipService;

        public SearchAccountHandler(SearchAccountService accountInfoService, IPService ipService)
            : this(accountInfoService, ipService, null, null)
        {
        }

        internal SearchAccountHandler(
            SearchAccountService accountInfoService,
            IPService ipService,
            IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel,
            Func<bool> getUpdateCreditReplicateServer)
        {
            this.accountInfoService = accountInfoService;
            this.ipService = ipService;

            this.getBalanceInfoViewModelByLevel = getBalanceInfoViewModelByLevel ??
                new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, IBalanceInfoViewModel>>
                {
                    {UserLevel.Master, BuildMasterBalanceInfoViewModel},
                    {UserLevel.Agent, BuildAgentBalanceInfoViewModel},
                    {UserLevel.Member, BuildMemberBalanceInfoViewModel}
                };

            this.getUpdateCreditReplicateServer = getUpdateCreditReplicateServer ??
                SimpleServiceLocator.GetService<Func<bool>>();
        }

        public Task<AccountInfoViewModel> Handle(SearchAccountQuery message)
            => Task.FromResult(new AccountInfoViewModel(message));

        public async Task<CustomerInfoViewModel> Handle(CustomerInfoQuery message)
        {
            var customerInfo = await accountInfoService.GetCustomerInfo(message.SearchAccountName, message.ActorId, message.ActorRoleId).ConfigureAwait(false);

            if (customerInfo != null)
            {
                var searchAccountLevel = Enumeration.FromValue<UserLevel>(customerInfo.RoleId);

                var balanceInfo = getBalanceInfoViewModelByLevel[searchAccountLevel](
                    message,
                    customerInfo,
                    await accountInfoService.GetCreditInfo(customerInfo.CustId).ConfigureAwait(false),
                    await accountInfoService.GetBalanceInfo(customerInfo.CustId, customerInfo.RoleId).ConfigureAwait(false));

                var formatedIP = ipService.FormatIP(customerInfo.LastLoginIP, message.IsAdminCanViewVNIP);

                customerInfo.LastLoginIP = formatedIP.IP;
                customerInfo.AllowOpenInfoPopup = formatedIP.AllowOpenInfoPopup;

                return new CustomerInfoViewModel(
                    message,
                    customerInfo,
                    balanceInfo,
                    await accountInfoService.CheckFirstLoginByQRCode(customerInfo.CustId).ConfigureAwait(false),
                    message.CanCreateAccount);
            }

            return null;
        }

        public async Task<DownlineListViewModel> Handle(DownlineListQuery message)
        {
            var downlineList =
                await accountInfoService
                .GetDownlineList(
                    message.SearchAccountId,
                    message.PageIndex,
                    message.RoleId,
                    message.StatusId,
                    message.TotalPages)
                .ConfigureAwait(false);

            var totalDownlinesSummarys =
                message.TotalPages.Equals(0) ?
               await accountInfoService
               .GetTotalDownlinesSummary(message.SearchAccountId, message.RoleId)
               .ConfigureAwait(false) :
               Enumerable.Empty<TotalDownlinesSummary>();

            return new DownlineListViewModel(downlineList, totalDownlinesSummarys);
        }

        public async Task<IEnumerable<OnlineUserModel>> Handle(OnlineListQuery message)
            => await accountInfoService.GetOnlineList(message.SearchAccountId).ConfigureAwait(false);

        public async Task<GivenCreditViewModel> Handle(GivenCreditQuery message)
        {
            decimal memberCreditMin = 0;
            if (Enumeration.FromValue<UserLevel>(message.SearchAccountRoleId).IsMemberLevel())
            {
                var balanceInfo = (MemberBalanceInfoModel)await accountInfoService.GetBalanceInfo(message.SearchAccountId, message.SearchAccountRoleId).ConfigureAwait(false);

                memberCreditMin = Math.Max(0, balanceInfo.CustomerOutstanding - balanceInfo.CustomerCash + balanceInfo.PendingTransfer3rd);
            }

            return new GivenCreditViewModel(
                message,
                await accountInfoService.GetCreditInfo(message.SearchAccountId).ConfigureAwait(false),
                memberCreditMin);
        }

        public Task<Result> Handle(GivenCreditCommand message)
            => accountInfoService.UpdateGivenCredit(
                new UpdateGivenCreditParam(
                    message.SearchAccountId,
                    message.SearchAccountRoleId,
                    message.CreditAmount,
                    getUpdateCreditReplicateServer(),
                    message.ActorId,
                    message.SubAccountId,
                    message.LangKey,
                    message.IP));

        public Task<Result> Handle(CustomerNonProductStatusCommand message)
           => accountInfoService.UpdateCustomerStatus(
               new UpdateCustomerStatusParam(
                   message.SearchAccountId,
                   message.ActorId,
                   message.SubAccountId,
                   message.Language,
                   message.IP,
                   message.StatusValue,
                   message.StatusName,
                   message.ActorName));

        public Task<Result> Handle(UpdateAllBalanceTransferCommand message)
           => accountInfoService.UpdateAllDayBalanceTransfer(
               new UpdateAllDayBalanceTransferParam(
                   message.SearchAccountId,
                   message.ActorId,
                   message.Language,
                   message.IP,
                   message.StatusValue,
                   message.StatusName));

        internal static IBalanceInfoViewModel BuildMasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel)
            => new MasterBalanceInfoViewModel(message, customerInfo, creditInfo, balanceInfoModel);

        internal static IBalanceInfoViewModel BuildAgentBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel)
            => new AgentBalanceInfoViewModel(message, customerInfo, creditInfo, balanceInfoModel);

        internal static IBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel)
            => new MemberBalanceInfoViewModel(message, customerInfo, creditInfo, balanceInfoModel);
    }
}