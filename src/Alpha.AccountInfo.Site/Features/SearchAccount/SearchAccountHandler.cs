namespace Alpha.AccountInfo.Site.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.Products.AccountInfo;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.Core.Models;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using Alpha.Products.Core.ViewModels;
    using Alpha.SecurityCode.Services;
    using MediatR;
    using Services.AccountInfo;

    public class SearchAccountHandler :
        IAsyncRequestHandler<SearchAccountQuery, AccountInfoViewModel>,
        IAsyncRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>,
        IAsyncRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IAsyncRequestHandler<DownlineListQuery, DownlineListViewModel>,
        IAsyncRequestHandler<CustomerStatusQuery, CustomerStatusViewModel>,
        IAsyncRequestHandler<MultipleCustomerStatusQuery, CustomerStatusViewModel>,
        IAsyncRequestHandler<OnlineListQuery, IEnumerable<OnlineUserModel>>,
        IAsyncRequestHandler<GivenCreditQuery, GivenCreditViewModel>,
        IAsyncRequestHandler<CustomerNonProductStatusCommand, Result>,
        IAsyncRequestHandler<GivenCreditCommand, Result>,
        IAsyncRequestHandler<UpdateAllBalanceTransferCommand, Result>        
    {
        private readonly SearchAccountService accountInfoService;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory;
        private readonly SecurityCodeChecker securityCodeChecker;
        private readonly Func<bool> getUpdateCreditReplicateServer;

        public SearchAccountHandler(SearchAccountService accountInfoService, SecurityCodeChecker securityCodeChecker) 
            : this(accountInfoService, null, null, null, securityCodeChecker, null)
        {
        }

        internal SearchAccountHandler(
            SearchAccountService accountInfoService,
            IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel,
            GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory,
            GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory,
            SecurityCodeChecker securityCodeChecker,
            Func<bool> getUpdateCreditReplicateServer)
        {
            this.accountInfoService = accountInfoService;

            this.getBalanceInfoViewModelByLevel = getBalanceInfoViewModelByLevel ??
                new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, IBalanceInfoModel, IBalanceInfoViewModel>>
                {
                    {UserLevel.Master, BuildMasterBalanceInfoViewModel},
                    {UserLevel.Agent, BuildAgentBalanceInfoViewModel},
                    {UserLevel.Member, BuildMemberBalanceInfoViewModel}
                };

            this.settingViewModelFactory = settingViewModelFactory ??
                SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>>();

            this.statusViewModelFactory = statusViewModelFactory ??
                 SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel>>();

            this.securityCodeChecker = securityCodeChecker;

            this.getUpdateCreditReplicateServer = getUpdateCreditReplicateServer ?? 
                SimpleServiceLocator.GetService<Func<bool>>();
        }

        public Task<AccountInfoViewModel> Handle(SearchAccountQuery message)
            => Task.FromResult(new AccountInfoViewModel(message));

        public async Task<IEnumerable<UsernameSuggestion>> Handle(UserNameSuggestionQuery message)
            => await accountInfoService.GetUsernamesSuggestion(message.ActorId, message.SuggestionQuery, message.IsInternal).ConfigureAwait(false);

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

        public async Task<CustomerStatusViewModel> Handle(CustomerStatusQuery message)
            => await BuildCustomerStatus(message).ConfigureAwait(false);

        public async Task<CustomerStatusViewModel> Handle(MultipleCustomerStatusQuery message)
           => await BuildCustomerStatus(message, true, message.CustomerIds, message.Usernames).ConfigureAwait(false);

        internal Task<CustomerStatusViewModel> BuildCustomerStatus(
            CustomerStatusQuery message,
            bool isMultiple = false,
            string customerIds = "",
            string usernames = "")
        {
            var responseTask = Task.Run(() => accountInfoService.GetCustomerStatus(message.SearchAccountId));
            var customerStatusModel = responseTask.Result;

            var actor = new Customer(
               message.ActorId,
               message.SubAccountId,
               message.ActorName,
               message.ActorRoleId,
               BuildActorProductStatus(message, customerStatusModel.DCSProducts.ToList()));

            var upline = new Customer(
                 message.UplineId,
                 message.SubAccountId,
                 message.UplineName,
                 message.UplineRoleId,
                 BuildUplineProductStatus(customerStatusModel.NonDCSProduct, customerStatusModel.DCSProducts.ToList()));

            var searchAccount = new Customer(
               message.SearchAccountId,
               message.SubAccountId,
               message.SearchAccountName,
               message.SearchAccountRoleId,
               BuildSearchAccountProductStatus(customerStatusModel.NonDCSProduct, customerStatusModel.DCSProducts.ToList()),
               upline);

            return Task.FromResult(new CustomerStatusViewModel(
                BuildCustomerProductStatus(actor, searchAccount, message, customerStatusModel),
                BuildCustomerProductSettings(actor, searchAccount, message, customerStatusModel, isMultiple)));
        }

        public async Task<IEnumerable<OnlineUserModel>> Handle(OnlineListQuery message)
            => await accountInfoService.GetOnlineList(message.SearchAccountId).ConfigureAwait(false);

        public async Task<GivenCreditViewModel> Handle(GivenCreditQuery message)
        {
            decimal memberCreditMin = 0;
            if (Enumeration.FromValue<UserLevel>(message.SearchAccountRoleId).IsMemberLevel())
            {
                var balanceInfo = (MemberBalanceInfoModel) await accountInfoService.GetBalanceInfo(message.SearchAccountId, message.SearchAccountRoleId).ConfigureAwait(false);

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

        internal IEnumerable<CustomerProductStatus> BuildSearchAccountProductStatus(
            NonDCSProductStatusModel nonDCSProduct,
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, nonDCSProduct.DisableNumber),
                new CustomerProductStatus(ProductConstants.RNGCasino, nonDCSProduct.DisableCasino, synced: nonDCSProduct.SyncCasino),
                new CustomerProductStatus(ProductConstants.ESport, nonDCSProduct.DisableEsports),
                new CustomerProductStatus(ProductConstants.Keno, hidden: nonDCSProduct.HideKeno),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, nonDCSProduct.DisableGoldDeluxe, hidden: nonDCSProduct.HideGoldDeluxe)
            };

            dcsProducts.ToList().ForEach(product =>
            {
                customerProductStatus.Add(
                    new CustomerProductStatus(product.ProductId, product.IsDisabled, product.IsHidden, product.IsSync));
            });

            return customerProductStatus;
        }

        internal IEnumerable<CustomerProductStatus> BuildUplineProductStatus(
            NonDCSProductStatusModel nonDCSProduct,
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, nonDCSProduct.UplineDisableNumber),
                new CustomerProductStatus(ProductConstants.RNGCasino, nonDCSProduct.UplineDisableCS),
                new CustomerProductStatus(ProductConstants.ESport, nonDCSProduct.UplineDisableEsports),
                new CustomerProductStatus(ProductConstants.Keno, hidden: nonDCSProduct.HideKeno),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, nonDCSProduct.UplineDisableGoldDeluxe, hidden: nonDCSProduct.UplineHideGoldDeluxe)
            };

            dcsProducts.ToList().ForEach(product =>
            {
                customerProductStatus.Add(
                    new CustomerProductStatus(product.ProductId, product.IsDisabledUpline, product.IsHiddenUpline, product.IsSyncedUpline));
            });

            return customerProductStatus;
        }

        internal IEnumerable<CustomerProductStatus> BuildActorProductStatus(
            CustomerStatusQuery message,
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, message.IsNumberGameDisabled),
                new CustomerProductStatus(ProductConstants.RNGCasino, message.IsCasinoDisabled, synced: message.IsCasinoSync),
                new CustomerProductStatus(ProductConstants.ESport, message.IsDisabledESports),
                new CustomerProductStatus(ProductConstants.Keno, hidden:message.IsHideKeno),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, message.DisableGoldDeluxe, hidden: message.IsHideGoldDeluxe)
            };

            dcsProducts.ToList().ForEach(product =>
            {
                var productId = product.ProductId;
                customerProductStatus.Add(
                    new CustomerProductStatus(
                        productId,
                        message.GetDisabledProduct(productId),
                        message.GetHideProduct(productId),
                        message.GetSyncedProduct(productId)));
            });

            return customerProductStatus;
        }

        internal IEnumerable<IStatusItemViewModel> BuildCustomerProductStatus(
            Customer actor,
            Customer searchAccount,
            CustomerStatusQuery message,
            CustomerStatusModel model)
        {
            var request = new StatusSearchAccountRequest(actor, searchAccount);

            request
                .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)

                .AddData(DataKey.SearchAccount.IsAllowedMasterAutoPT, !model.NonProduct.DisableAutoMPT)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedOutright, !model.NonProduct.UplineDisableOutright)
                .AddData(DataKey.SearchAccount.IsAllowedOutright, !model.NonProduct.DisableOutright)

                .AddData(DataKey.SearchAccount.Upline.IsClosed, model.NonProduct.UplineClosed)
                .AddData(DataKey.SearchAccount.IsClosed, model.NonProduct.Closed)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedDoubleComm, model.NonProduct.UplineDoubleCommLosing)
                .AddData(DataKey.SearchAccount.IsAllowedDoubleComm, model.NonProduct.DoubleCommLosing)

                .AddData(DataKey.SearchAccount.Upline.IsSuspended, model.NonProduct.UplineSuspend)
                .AddData(DataKey.SearchAccount.IsSuspended, model.NonProduct.Suspended)
                .AddData(DataKey.SearchAccount.IsDirectSuspend, model.NonProduct.IsDirectSuspend)

                .AddData(DataKey.SearchAccount.AllowedAllDayBalTransfer, message.AllowedAllDayBalTransfer)
                .AddData(DataKey.SearchAccount.IsAllDayBalTransfer, model.NonProduct.IsAllDayBalTransfer);
              

            return statusViewModelFactory.GetViewModels(request).Where(status => status.Visible).OrderBy(status => status.DisplayOrder);
        }

        internal IEnumerable<ISettingItemViewModel> BuildCustomerProductSettings(
            Customer actor,
            Customer searchAccount,
            CustomerStatusQuery message,
            CustomerStatusModel model,
            bool isMultiple = false,
            string customerIds = "",
            string usernames = "")
        {
            var request = new SettingSearchAccountRequest(actor, searchAccount);
            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, model.NonProduct.TransferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)
                 .AddData(DataKey.SearchAccount.IsEnableSecurityCode, securityCodeChecker.IsEnableSecurityCode());

            if (isMultiple)
            {
                request.AddData("customerIds", customerIds)
                 .AddData("usernames", usernames)
                 .AddData("customerNames", usernames)
                 .AddData("isMultiple", "1");
            }

            return settingViewModelFactory.GetViewModels(request).Where(setting => setting.Visible).OrderBy(setting => setting.DisplayOrder);
        }
    }
}