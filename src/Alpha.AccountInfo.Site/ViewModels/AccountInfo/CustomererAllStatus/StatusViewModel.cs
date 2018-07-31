namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.AccountInfo;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Status;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.Core.Models;
    using System.Collections.Generic;
    using static Alpha.Products.AccountInfo.DataKey;

    public class StatusViewModel
    {
        public StatusViewModel(
              CustomerAllStatusQuery message,
              CustomerStatusModel customerStatus,
              Customer actor,
              Customer searchAccount)
        {
            var viewModelfactory = new StatusViewModelFactory();

            var request = new StatusSearchAccountRequest(actor, searchAccount);

            request
                .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)

                .AddData(SearchAccount.IsAllowedMasterAutoPT, !customerStatus.DisableAutoMPT)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedOutright, !customerStatus.UplineDisableOutright)
                .AddData(DataKey.SearchAccount.IsAllowedOutright, !customerStatus.DisableOutright)

                .AddData(DataKey.SearchAccount.Upline.IsClosed, customerStatus.UplineClosed)
                .AddData(DataKey.SearchAccount.IsClosed, customerStatus.Closed)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedDoubleComm, customerStatus.UplineDoubleCommLosing)
                .AddData(DataKey.SearchAccount.IsAllowedDoubleComm, customerStatus.DoubleCommLosing)

                .AddData(SearchAccount.Upline.IsSuspended, customerStatus.UplineSuspend)
                .AddData(SearchAccount.IsSuspended, customerStatus.Suspended);

            Settings = viewModelfactory.GetViewModels(request);
        }

        public IEnumerable<IStatusItemViewModel> Settings { get; }
    }
}