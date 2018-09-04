namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Status;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.Core.Models;
    using System.Collections.Generic;
    using System.Linq;
    using static Alpha.Products.AccountInfo.DataKey;

    public class CustomererAllStatusViewModel
    {
        private readonly CustomerAllStatusQuery message;
        private readonly CustomerStatusModel customerStatus;
        private readonly Customer actor;
        private readonly Customer searchAccount;

        public CustomererAllStatusViewModel(
            CustomerAllStatusQuery message,
            CustomerStatusModel customerStatus,
            Customer actor,
            Customer searchAccount)
        {
            this.message = message;
            this.customerStatus = customerStatus;
            this.actor = actor;
            this.searchAccount = searchAccount;
        }

        public IEnumerable<IStatusItemViewModel> Status
        {
            get
            {
                var viewModelfactory = new StatusViewModelFactory();
                var request = new StatusSearchAccountRequest(actor, searchAccount);

                request
                    .AddData(Actor.HasUpdateAccountPermission, message.CanUpdateAccount)

                    .AddData(SearchAccount.IsAllowedMasterAutoPT, !customerStatus.DisableAutoMPT)

                    .AddData(SearchAccount.Upline.IsAllowedOutright, !customerStatus.UplineDisableOutright)
                    .AddData(SearchAccount.IsAllowedOutright, !customerStatus.DisableOutright)

                    .AddData(SearchAccount.Upline.IsClosed, customerStatus.UplineClosed)
                    .AddData(SearchAccount.IsClosed, customerStatus.Closed)

                    .AddData(SearchAccount.Upline.IsAllowedDoubleComm, customerStatus.UplineDoubleCommLosing)
                    .AddData(SearchAccount.IsAllowedDoubleComm, customerStatus.DoubleCommLosing)

                    .AddData(SearchAccount.Upline.IsSuspended, customerStatus.UplineSuspend)
                    .AddData(SearchAccount.IsSuspended, customerStatus.Suspended);

                return viewModelfactory.GetViewModels(request).Where(status => status.Visible);
            }
        }

        public IEnumerable<ISettingItemViewModel> Settings
        {
            get
            {
                var viewModelfactory = new SettingViewModelFactory();
                var request = new SettingSearchAccountRequest(actor, searchAccount);

                request
                     .AddData(Actor.CanEditDailyTransferOption, customerStatus.TransferDaily)
                     .AddData(Actor.HasUpdateAccountPermission, message.CanUpdateAccount);

                return viewModelfactory.GetViewModels(request).Where(setting => setting.Visible);
            }
        }
    }
}