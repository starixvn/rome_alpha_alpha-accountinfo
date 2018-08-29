namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.AccountInfo;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.Core.Models;
    using System.Collections.Generic;

    public class SettingViewModel
    {
        public SettingViewModel(
            CustomerAllStatusQuery message,
            bool transferDaily,
            Customer actor,
            Customer searchAccount)
        {
            var viewModelfactory = new SettingViewModelFactory();
            var request = new SettingSearchAccountRequest(actor, searchAccount);

            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, transferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount);

            Settings = viewModelfactory.GetViewModels(request);
        }

        public IEnumerable<ISettingItemViewModel> Settings {get;}
    }
}