namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
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
    using MediatR;
    using Services.AccountInfo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SearchAccountHandler :
        IAsyncRequestHandler<SearchAccountQuery, AccountInfoViewModel>,
        IAsyncRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>,
        IAsyncRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IAsyncRequestHandler<DownlineListQuery, DownlineListViewModel>,
        IAsyncRequestHandler<CustomerStatusQuery, CustomerStatusViewModel>,
        IAsyncRequestHandler<OnlineListQuery, IEnumerable<OnlineUserModel>>,
        IAsyncRequestHandler<GivenCreditQuery, GivenCreditViewModel>,
        IAsyncRequestHandler<GivenCreditCommand, Result>,
        IAsyncRequestHandler<CustomerNonProductStatusCommand, Result>
    {
        private readonly SearchAccountService accountInfoService;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory;

        public SearchAccountHandler(SearchAccountService accountInfoService) : this(accountInfoService, null, null, null)
        {
        }

        internal SearchAccountHandler(
            SearchAccountService accountInfoService,
            IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel,
            GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory,
            GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory)
        {
            this.accountInfoService = accountInfoService;

            this.getBalanceInfoViewModelByLevel = getBalanceInfoViewModelByLevel ??
                new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>>
                {
                    {UserLevel.Master, BuildMasterBalanceInfoViewModel},
                    {UserLevel.Agent, BuildAgentBalanceInfoViewModel},
                    {UserLevel.Member, BuildMemberBalanceInfoViewModel}
                };

            this.settingViewModelFactory = settingViewModelFactory ??
                SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>>();

            this.statusViewModelFactory = statusViewModelFactory ??
                 SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel>>();
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

        public Task<CustomerStatusViewModel> Handle(CustomerStatusQuery message)
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
                BuildCustomerProductSettings(actor, searchAccount, message, customerStatusModel)));
        }

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
                   message.ActorName,
                   message.SyncedCasino3rd));

        internal static IBalanceInfoViewModel BuildMasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new MasterBalanceInfoViewModel(message, balanceInfoModel, customerInfo);

        internal static IBalanceInfoViewModel BuildAgentBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new AgentBalanceInfoViewModel(message, balanceInfoModel, customerInfo);

        internal static IBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new MemberBalanceInfoViewModel(message, balanceInfoModel, customerInfo);

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
                new CustomerProductStatus(ProductConstants.ColossusBets, nonDCSProduct.DisableColossusBet, hidden: nonDCSProduct.HideColossusBet),
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
                new CustomerProductStatus(ProductConstants.ColossusBets, nonDCSProduct.UplineDisableColossusBet),
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
                new CustomerProductStatus(ProductConstants.ColossusBets, message.DisableColossusBets, hidden: message.IsHideColossusBets),
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
                .AddData(DataKey.SearchAccount.IsSuspended, model.NonProduct.Suspended);

            return statusViewModelFactory.GetViewModels(request).Where(status => status.Visible).OrderBy(status => status.DisplayOrder);
        }

        internal IEnumerable<ISettingItemViewModel> BuildCustomerProductSettings(
            Customer actor,
            Customer searchAccount,
            CustomerStatusQuery message,
            CustomerStatusModel model)
        {
            var request = new SettingSearchAccountRequest(actor, searchAccount);
            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, model.NonProduct.TransferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount);

            return settingViewModelFactory.GetViewModels(request).Where(setting => setting.Visible).OrderBy(setting => setting.DisplayOrder);
        }
    }
}