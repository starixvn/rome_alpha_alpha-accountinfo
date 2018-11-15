namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Shared;

    public class ConfirmClosedViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmClosedViewModel(ISearchAccountInfoRequest request, int displayOrder = 2)
            : base(request, displayOrder)
        {
        }

        public override bool Disabled => base.Disabled || Request.GetData<bool>(DataKey.SearchAccount.Upline.IsClosed);

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsClosed);

        public override bool ReloadCustomerInfo => true;

        internal override string SuffixMessage => 
            !Request.SearchAccount.IsMember && Checked ?
                    "InCaseClosedDownlinesMessage" : "InCaseChangeClosedStatusMessage";

        internal override string StatusName => DataKey.StatusMapping.Closed;

        internal override bool StatusValue => !Checked;

        internal override bool SyncedCasino3rd => Request.SearchAccount.ProductStatusOf(ProductConstants.RNGCasino).Synced;
    }
}