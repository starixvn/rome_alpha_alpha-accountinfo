namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EditProductHandler :
        IAsyncRequestHandler<CustomerStatusQuery, CustomerStatusViewModel>,
        IAsyncRequestHandler<CustomerStatusMultipleQuery, IEnumerable<ISettingItemMultipleViewModel>>
    {
        private readonly SearchAccountService accountInfoService;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel> settingMultipleViewModelFactory;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory;
        private readonly SecurityCodeChecker securityCodeChecker;

        public EditProductHandler(SearchAccountService accountInfoService, SecurityCodeChecker securityCodeChecker) 
            : this(accountInfoService, null, null, null, securityCodeChecker)
        {
        }

        internal EditProductHandler(
            SearchAccountService accountInfoService,
            GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel> settingMultipleViewModelFactory,
            GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory,
            GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory,
            SecurityCodeChecker securityCodeChecker)
        {
            this.accountInfoService = accountInfoService;

            this.settingMultipleViewModelFactory = settingMultipleViewModelFactory ??
                SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel>>();

            this.settingViewModelFactory = settingViewModelFactory ??
            SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>>();

            this.statusViewModelFactory = statusViewModelFactory ??
                 SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel>>();

            this.securityCodeChecker = securityCodeChecker;
        }

        public async Task<CustomerStatusViewModel> Handle(CustomerStatusQuery message)
            => await BuildCustomerStatus(message).ConfigureAwait(false);

        public async Task<IEnumerable<ISettingItemMultipleViewModel>> Handle(CustomerStatusMultipleQuery message)
           => await BuildCustomerStatusMultiple(message, message.CustomerIds, message.Usernames).ConfigureAwait(false);

        internal Task<CustomerStatusViewModel> BuildCustomerStatus(CustomerStatusQuery message)
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

        internal Task<IEnumerable<ISettingItemMultipleViewModel>> BuildCustomerStatusMultiple(
            CustomerStatusMultipleQuery message,
            string customerIds,
            string usernames)
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

            return Task.FromResult(BuildCustomerProductMultipleSettings(actor, searchAccount, message, customerStatusModel, customerIds, usernames));

        }

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
            CustomerStatusQueryBase message,
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
            CustomerStatusQueryBase message,
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
            CustomerStatusQueryBase message,
            CustomerStatusModel model)
        {
            var request = new SettingSearchAccountRequest(actor, searchAccount);
            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, model.NonProduct.TransferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)
                 .AddData(DataKey.SearchAccount.IsEnableSecurityCode, securityCodeChecker.IsEnableSecurityCode());

            return settingViewModelFactory.GetViewModels(request).Where(setting => setting.Visible).OrderBy(setting => setting.DisplayOrder);
        }

        internal IEnumerable<ISettingItemMultipleViewModel> BuildCustomerProductMultipleSettings(
           Customer actor,
           Customer searchAccount,
           CustomerStatusMultipleQuery message,
           CustomerStatusModel model,
           string customerIds,
           string usernames)
        {
            var request = new SettingSearchAccountRequest(actor, searchAccount);
            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, model.NonProduct.TransferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)
                 .AddData(DataKey.SearchAccount.IsEnableSecurityCode, securityCodeChecker.IsEnableSecurityCode())
                 .AddData("customerIds", customerIds)
                 .AddData("usernames", usernames)
                 .AddData("customerNames", usernames);

            return settingMultipleViewModelFactory.GetViewModels(request).Where(setting => setting.Visible).OrderBy(setting => setting.DisplayOrder);
        }
    }
}