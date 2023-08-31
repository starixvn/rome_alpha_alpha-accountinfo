namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using AlphaPlus.Utilities.IP;
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
        IAsyncRequestHandler<UpdateAllBalanceTransferCommand, Result>,
        IAsyncRequestHandler<MemberBalanceInfoQuery, IBalanceInfoViewModel>,
        IAsyncRequestHandler<GetLastTxnQuery, string>
    {
        private readonly SearchAccountService accountInfoService;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, bool, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;
        private readonly Func<bool> getUpdateCreditReplicateServer;
        private readonly IPService ipService;
        private readonly KickoutCustomerService kickoutCustomerService;

        public SearchAccountHandler(SearchAccountService accountInfoService, IPService ipService, KickoutCustomerService kickoutCustomerService)
            : this(accountInfoService, ipService, kickoutCustomerService, null, null)
        {
        }

        internal SearchAccountHandler(
            SearchAccountService accountInfoService,
            IPService ipService,
            KickoutCustomerService kickoutCustomerService,
            IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, bool, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel,
            Func<bool> getUpdateCreditReplicateServer)
        {
            this.accountInfoService = accountInfoService;
            this.ipService = ipService;
            this.kickoutCustomerService = kickoutCustomerService;

            this.getBalanceInfoViewModelByLevel = getBalanceInfoViewModelByLevel ??
                new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, bool, IBalanceInfoViewModel>>
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
            var customerInfo = await accountInfoService.GetCustomerInfo(message.SearchAccountName, message.ActorId, message.ActorRoleId);

            if (customerInfo != null)
            {
                var searchAccountLevel = Enumeration.FromValue<UserLevel>(customerInfo.RoleId);

                var balanceInfo = getBalanceInfoViewModelByLevel[searchAccountLevel](
                    message,
                    customerInfo,
                    await accountInfoService.GetCreditInfo(customerInfo.CustId),
                    await accountInfoService.GetBalanceInfo(customerInfo.CustId, customerInfo.RoleId),
                    await accountInfoService.IsAllDayBalanceTransfer(message.ActorId));

                var formatedIP = ipService.FormatIP(customerInfo.LastLoginIP, message.IsAdminCanViewVNIP);

                customerInfo.LastLoginIP = formatedIP.IP;
                customerInfo.AllowOpenInfoPopup = formatedIP.AllowOpenInfoPopup;

                return new CustomerInfoViewModel(
                    message,
                    customerInfo,
                    balanceInfo,
                    await accountInfoService.CheckFirstLoginByQRCode(customerInfo.CustId),
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
                ;

            var totalDownlinesSummarys =
                message.TotalPages.Equals(0) ?
               await accountInfoService
               .GetTotalDownlinesSummary(message.SearchAccountId, message.RoleId)
                :
               Enumerable.Empty<TotalDownlinesSummary>();

            return new DownlineListViewModel(downlineList, totalDownlinesSummarys);
        }

        public async Task<IEnumerable<OnlineUserModel>> Handle(OnlineListQuery message)
            => await accountInfoService.GetOnlineList(message.SearchAccountId, message.IsAdminCanViewVNIP);

        public async Task<GivenCreditViewModel> Handle(GivenCreditQuery message)
        {
            decimal memberCreditMin = 0;
            if (Enumeration.FromValue<UserLevel>(message.SearchAccountRoleId).IsMemberLevel())
            {
                var balanceInfo = (MemberBalanceInfoModel)await accountInfoService.GetBalanceInfo(message.SearchAccountId, message.SearchAccountRoleId);

                memberCreditMin = Math.Max(0, balanceInfo.CustomerOutstanding - balanceInfo.CustomerCash + balanceInfo.PendingTransfer3rd);
            }

            return new GivenCreditViewModel(
                message,
                await accountInfoService.GetCreditInfo(message.SearchAccountId),
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

        public async Task<Result> Handle(CustomerNonProductStatusCommand message)
        {
            var result = await accountInfoService.UpdateCustomerStatus(
                 new UpdateCustomerStatusParam(
                     message.SearchAccountId,
                     message.ActorId,
                     message.SubAccountId,
                     message.Language,
                     message.IP,
                     message.StatusValue,
                     message.StatusName,
                     message.ActorName));

            if (result.IsSuccess && message.StatusValue)
            {
                await kickoutCustomerService.KickoutDueToProtectedStatusAsync(
                    message.SearchAccountId,
                    message.SearchAccountRoleId,
                    message.ServerId,
                    message.IP);
            }

            return result;
        }

        public Task<Result> Handle(UpdateAllBalanceTransferCommand message)
           => accountInfoService.UpdateAllDayBalanceTransfer(
               new UpdateAllDayBalanceTransferParam(
                   message.SearchAccountId,
                   message.ActorId,
                   message.Language,
                   message.IP,
                   message.StatusValue
                   message.StatusName,
                   message.Currency));

        internal static IBalanceInfoViewModel BuildMasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel,
            bool isAllDayBalanceInfo)
            => new MasterBalanceInfoViewModel(message, customerInfo, creditInfo, balanceInfoModel, isAllDayBalanceInfo);

        internal static IBalanceInfoViewModel BuildAgentBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel,
            bool isAllDayBalanceInfo)
            => new AgentBalanceInfoViewModel(message, customerInfo, creditInfo, balanceInfoModel, isAllDayBalanceInfo);

        internal static IBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel,
            bool isAllDayBalanceInfo)
            => new MemberBalanceInfoViewModel(message, customerInfo, creditInfo, balanceInfoModel, isAllDayBalanceInfo);

        public async Task<IBalanceInfoViewModel> Handle(MemberBalanceInfoQuery message)
        {
            var customerInfoQuery = new CustomerInfoQuery(message.Viewer)
            {
                SearchAccountName = message.CustName
            };
            var customerInfo = await accountInfoService.GetCustomerInfo(
                customerInfoQuery.SearchAccountName,
                customerInfoQuery.ActorId,
                customerInfoQuery.ActorRoleId);

            if (customerInfo != null && Enumeration.FromValue<UserLevel>(customerInfo.RoleId).IsMemberLevel())
            {
                var creditInfoTask = accountInfoService.GetCreditInfo(customerInfo.CustId);
                var balanceInfoTask = accountInfoService.GetBalanceInfo(customerInfo.CustId, UserLevel.Member.Value);
                var isAllDayBalanceTransferTask = accountInfoService.IsAllDayBalanceTransfer(customerInfoQuery.ActorId);
                await Task.WhenAll(creditInfoTask, balanceInfoTask, isAllDayBalanceTransferTask);

                return BuildMemberBalanceInfoViewModel(customerInfoQuery, customerInfo, await creditInfoTask, await balanceInfoTask, await isAllDayBalanceTransferTask);
            }

            return null;
        }

        public async Task<string> Handle(GetLastTxnQuery message)
        {
            var lastTxn = await accountInfoService.GetLastTxn(message.Username, message.UplineId, message.IsInternal);

            return DateTime.MinValue == lastTxn ? string.Empty : lastTxn.ToString();
        }
    }
}