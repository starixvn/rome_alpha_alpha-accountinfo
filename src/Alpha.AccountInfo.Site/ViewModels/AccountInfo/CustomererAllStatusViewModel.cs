namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.AccountInfo;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Status;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.Core.Models;
    using Alpha.Products.Core.Shared;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomererAllStatusViewModel
    {
        private readonly CustomerAllStatusQuery message;
        private readonly CustomerAllStatusModel model;

        public CustomererAllStatusViewModel(
            CustomerAllStatusQuery message,
            CustomerAllStatusModel model)
        {
            this.message = message;
            this.model = model;
        }

        public IEnumerable<IStatusItemViewModel> Status
        {
            get
            {
                var viewModelfactory = new StatusViewModelFactory();
                var request = new StatusSearchAccountRequest(Actor, SearchAccount);

                request
                    .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)

                    .AddData(DataKey.SearchAccount.IsAllowedMasterAutoPT, !model.CustomerStatus.DisableAutoMPT)

                    .AddData(DataKey.SearchAccount.Upline.IsAllowedOutright, !model.CustomerStatus.UplineDisableOutright)
                    .AddData(DataKey.SearchAccount.IsAllowedOutright, !model.CustomerStatus.DisableOutright)

                    .AddData(DataKey.SearchAccount.Upline.IsClosed, model.CustomerStatus.UplineClosed)
                    .AddData(DataKey.SearchAccount.IsClosed, model.CustomerStatus.Closed)

                    .AddData(DataKey.SearchAccount.Upline.IsAllowedDoubleComm, model.CustomerStatus.UplineDoubleCommLosing)
                    .AddData(DataKey.SearchAccount.IsAllowedDoubleComm, model.CustomerStatus.DoubleCommLosing)

                    .AddData(DataKey.SearchAccount.Upline.IsSuspended, model.CustomerStatus.UplineSuspend)
                    .AddData(DataKey.SearchAccount.IsSuspended, model.CustomerStatus.Suspended);

                return viewModelfactory.GetViewModels(request).Where(status => status.Visible);
            }
        }

        public IEnumerable<ISettingItemViewModel> Settings
        {
            get
            {
                var viewModelfactory = new SettingViewModelFactory();
                var request = new SettingSearchAccountRequest(Actor, SearchAccount);

                request
                     .AddData(DataKey.Actor.CanEditDailyTransferOption, model.CustomerStatus.TransferDaily)
                     .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount);

                return viewModelfactory.GetViewModels(request).Where(setting => setting.Visible);
            }
        }

        private Customer Actor 
            => new Customer(
               message.ActorId,
               message.SubAccountId,
               message.ActorName,
               message.ActorRoleId,
               GetActorProductStatus(message, model.DCSProducts.ToList()));

        private Customer Upline 
            => new Customer(
                 message.UplientId,
                 message.SubAccountId,
                 message.UplineName,
                 message.UplineRoleId,
                 GetUplineProductStatus(model.NonDCSProduct, model.DCSProducts.ToList()));

        private Customer SearchAccount
            => new Customer(
               message.SearchAccountId,
               message.SubAccountId,
               message.SearchAccountName,
               message.SearchAccountRoleId,
               GetSearchAccountProductStatus(model.NonDCSProduct, model.DCSProducts.ToList()),
               Upline);

        private static IEnumerable<CustomerProductStatus> GetSearchAccountProductStatus(
            NonDCSProductModel nonDCSProduct,
            List<DCSProductModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.NumberGame, nonDCSProduct.DisableNumber),
                new CustomerProductStatus(ProductConstants.RNGCasino, nonDCSProduct.DisableCasino, synced: nonDCSProduct.SyncCasino),
                new CustomerProductStatus(ProductConstants.ESport, nonDCSProduct.DisableEsports),
                new CustomerProductStatus(ProductConstants.ColossusBets, nonDCSProduct.DisableColossusBet, hidden: nonDCSProduct.HideColossusBet),
                new CustomerProductStatus(ProductConstants.Keno, nonDCSProduct.HideKeno)
            };

            dcsProducts.ForEach(product =>
            {
                customerProductStatus.Add(
                    new CustomerProductStatus(product.ProductId, product.IsDisabled, product.IsHidden, product.IsSync));
            });

            return customerProductStatus;
        }

        private static IEnumerable<CustomerProductStatus> GetUplineProductStatus(
            NonDCSProductModel nonDCSProduct,
            List<DCSProductModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.NumberGame, nonDCSProduct.UplineDisableNumber),
                new CustomerProductStatus(ProductConstants.RNGCasino, nonDCSProduct.UplineDisableCS),
                new CustomerProductStatus(ProductConstants.ESport, nonDCSProduct.UplineDisableEsports),
                new CustomerProductStatus(ProductConstants.ColossusBets, nonDCSProduct.UplineDisableColossusBet, hidden: nonDCSProduct.UplineDisableColossusBet),
                new CustomerProductStatus(ProductConstants.Keno, nonDCSProduct.HideKeno)
            };

            dcsProducts.ForEach(product =>
            {
                customerProductStatus.Add(
                    new CustomerProductStatus(product.ProductId, product.IsDisabledUpline, product.IsHiddenUpline, product.IsSyncedUpline));
            });

            return customerProductStatus;
        }

        private static IEnumerable<CustomerProductStatus> GetActorProductStatus(
            CustomerAllStatusQuery message,
            List<DCSProductModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.NumberGame, message.IsNumberGameDisabled),
                new CustomerProductStatus(ProductConstants.RNGCasino, message.IsCasinoDisabled, synced: message.IsCasinoSync),
                new CustomerProductStatus(ProductConstants.ESport, message.IsDisabledESports),
                new CustomerProductStatus(ProductConstants.ColossusBets, message.DisableColossusBets, hidden: message.IsHideColossusBets),
                new CustomerProductStatus(ProductConstants.Keno, message.IsHideKeno)
            };

            dcsProducts.ForEach(product =>
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
    }
}