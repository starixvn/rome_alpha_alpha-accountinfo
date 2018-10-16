namespace Alpha.AccountInfo.Site.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.Products.Core.Shared;
    using MediatR;
    using Services.AccountInfo;

    public class SearchAccountHandler :
        IAsyncRequestHandler<SearchAccountQuery, AccountInfoViewModel>,
        IAsyncRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>,
        IAsyncRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IAsyncRequestHandler<DownlineListQuery, DownlineListViewModel>,
        IAsyncRequestHandler<CustomerStatusQuery, CustomererStatusViewModel>,
        IAsyncRequestHandler<OnlineListQuery, IEnumerable<OnlineUserModel>>,
        IAsyncRequestHandler<GivenCreditQuery, GivenCreditViewModel>,
        IAsyncRequestHandler<GivenCreditCommand, Result>,
        IAsyncRequestHandler<CustomerNonProductStatusCommand, Result>
    {
        private readonly SearchAccountService accountInfoService;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;

        public SearchAccountHandler(SearchAccountService accountInfoService) : this(accountInfoService, null)
        {
        }

        internal SearchAccountHandler(
            SearchAccountService accountInfoService,
            IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel)
        {
            this.accountInfoService = accountInfoService;

            this.getBalanceInfoViewModelByLevel = getBalanceInfoViewModelByLevel ??
                new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>>
                {
                    {UserLevel.Master, BuildMasterBalanceInfoViewModel},
                    {UserLevel.Agent, BuildAgentBalanceInfoViewModel},
                    {UserLevel.Member, BuildMemberBalanceInfoViewModel}
                };
        }

        public Task<AccountInfoViewModel> Handle(SearchAccountQuery message)
            => Task.FromResult(new AccountInfoViewModel(message));

        public Task<IEnumerable<UsernameSuggestion>> Handle(UserNameSuggestionQuery message)
            => accountInfoService.GetUsernamesSuggestion(message.ActorId, message.SuggestionQuery, message.IsInternal);

        public async Task<CustomerInfoViewModel> Handle(CustomerInfoQuery message)
        {
            var customerInfo = await accountInfoService.GetCustomerInfo(message.SearchAccountName, message.ActorId).ConfigureAwait(false);

            if (customerInfo != null)
            {
                var searchAccountLevel = Enumeration.FromValue<UserLevel>(customerInfo.RoleId);

                var balanceInfo = getBalanceInfoViewModelByLevel[searchAccountLevel](
                    message,
                    customerInfo,
                    await accountInfoService.GetBalanceInfo(customerInfo.CustId, customerInfo.RoleId).ConfigureAwait(false));

                return new CustomerInfoViewModel(message, customerInfo, balanceInfo);
            }

            return null;
        }

        public async Task<DownlineListViewModel> Handle(DownlineListQuery message)
            => new DownlineListViewModel(await accountInfoService.GetDownlineList(message.SearchAccountId).ConfigureAwait(false));

        public async Task<CustomererStatusViewModel> Handle(CustomerStatusQuery message)
            => new CustomererStatusViewModel(message, await accountInfoService.GetCustomerStatus(message.SearchAccountId).ConfigureAwait(false));

        public async Task<IEnumerable<OnlineUserModel>> Handle(OnlineListQuery message)
            => await accountInfoService.GetOnlineList(message.SearchAccountId).ConfigureAwait(false);

        public Task<GivenCreditViewModel> Handle(GivenCreditQuery message)
           => Task.FromResult(new GivenCreditViewModel(message));

        public Task<Result> Handle(GivenCreditCommand message)
            => accountInfoService.UpdateGivenCredit(
                new UpdateGivenCreditParam(
                    message.SearchAccountId,
                    message.SearchAccountRoleId,
                    message.CreditAmount,
                    message.ActorId,
                    message.SubAccountId,
                    message.Language,
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
                   message.ActorName,
                   message.SyncedCasino3rd));

        private static IBalanceInfoViewModel BuildMasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new MasterBalanceInfoViewModel(message, balanceInfoModel, customerInfo);

        private static IBalanceInfoViewModel BuildAgentBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new AgentBalanceInfoViewModel(message, balanceInfoModel, customerInfo);

        private static IBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new MemberBalanceInfoViewModel(message, balanceInfoModel, customerInfo);
    }
}