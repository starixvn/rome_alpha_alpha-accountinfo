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
              ProductQuery message,
              CustomerAllStatusModel model,
              Customer actor,
              Customer searchAccount)
        {
            var viewModelfactory = new StatusViewModelFactory();

            var request = new StatusSearchAccountRequest(actor, searchAccount);

            request
                .AddData(DataKey.Actor.HasUpdateAccountPermission, true)

                .AddData(SearchAccount.IsAllowedMasterAutoPT, true)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedOutright, true)
                .AddData(DataKey.SearchAccount.IsAllowedOutright, true)

                .AddData(DataKey.SearchAccount.Upline.IsClosed, true)
                .AddData(DataKey.SearchAccount.IsClosed, true)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedDoubleComm, true)
                .AddData(DataKey.SearchAccount.IsAllowedDoubleComm, true)

                .AddData(SearchAccount.Upline.IsSuspended, true)
                .AddData(SearchAccount.IsSuspended, true);

            Settings = viewModelfactory.GetViewModels(request);
        }

        public IEnumerable<IStatusItemViewModel> Settings { get; }
    }
}