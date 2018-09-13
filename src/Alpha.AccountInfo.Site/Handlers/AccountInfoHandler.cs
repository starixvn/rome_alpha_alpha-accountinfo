namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using Alpha.Products.Core.Shared;
    using MediatR;
    using Requests.ProductStatus;
    using Services.AccountInfo;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.AccountInfo;

    public class AccountInfoHandler :
        IAsyncRequestHandler<AccountInfoQuery, AccountInfoViewModel>,
        IRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>,
        IRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IRequestHandler<DownlineListQuery, DownlineListViewModel>,
        IRequestHandler<CustomerAllStatusQuery, CustomererAllStatusViewModel>,
        IRequestHandler<OnlineListQuery, IEnumerable<OnlineUserModel>>,
        IAsyncRequestHandler<GivenCreditQuery, GivenCreditViewModel>,
        IRequestHandler<GivenCreditCommand, Result>,
        IRequestHandler<CustomerStatusCommand, Result>
    {
        private readonly AccountInfoService accountInfoService;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;

        public AccountInfoHandler(AccountInfoService accountInfoService) : this(accountInfoService, null)
        {
        }

        internal AccountInfoHandler(
            AccountInfoService accountInfoService,
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

        public async Task<AccountInfoViewModel> Handle(AccountInfoQuery message)
            => new AccountInfoViewModel(message);

        public IEnumerable<UsernameSuggestion> Handle(UserNameSuggestionQuery message)
            => accountInfoService.GetUsernamesSuggestion(message.ActorId, message.SuggestionQuery, message.IsInternal);

        public CustomerInfoViewModel Handle(CustomerInfoQuery message)
        {
            var customerInfo = accountInfoService.GetCustomerInfoModel(message.SearchAccountName, message.ActorId);

            if (customerInfo != null)
            {
                var searchAccountLevel = Enumeration.FromValue<UserLevel>(customerInfo.RoleId);

                var balanceInfo = getBalanceInfoViewModelByLevel[searchAccountLevel](
                    message,
                    customerInfo,
                    accountInfoService.GetBalanceInfoModel(customerInfo.CustId, customerInfo.RoleId));

                return new CustomerInfoViewModel(message, customerInfo, balanceInfo);
            }

            return null;
        }

        public DownlineListViewModel Handle(DownlineListQuery message)
            => new DownlineListViewModel(accountInfoService.GetDownlineListModel(message.SearchAccountId));

        public CustomererAllStatusViewModel Handle(CustomerAllStatusQuery message)
            => new CustomererAllStatusViewModel(message, accountInfoService.GetCustomerAllStatusModel(message.SearchAccountId));

        public IEnumerable<OnlineUserModel> Handle(OnlineListQuery message)
            => accountInfoService.GetOnlineListModel(message.SearchAccountId);

        public async Task<GivenCreditViewModel> Handle(GivenCreditQuery message)
           => new GivenCreditViewModel(message);

        public Result Handle(GivenCreditCommand message)
            => accountInfoService.UpdateGivenCredit(
                new UpdateGivenCreditParam(
                    message.SearchAccountId,
                    message.SearchAccountRoleId,
                    message.CreditAmount,
                    message.ActorId,
                    message.ActorRoleId,
                    message.SubAccountId,
                    message.Language,
                    message.IP));

        public Result Handle(CustomerStatusCommand message)
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