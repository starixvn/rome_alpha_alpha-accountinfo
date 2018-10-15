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

        public override string ConfirmMessage
        {
            get
            {
                var suffixMessage = !Request.SearchAccount.IsMember && Checked ?
                    "InCaseClosedDownlinesMessage" : "InCaseChangeClosedStatusMessage";

                return Request.GetResource($"{GetType().Name}{suffixMessage}");
            }
        }

        public override bool Disabled => base.Disabled || Request.GetData<bool>(DataKey.SearchAccount.Upline.IsClosed);

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsClosed);

        protected override string StatusName => DataKey.StatusMapping.Closed;

        protected override bool StatusValue => !Checked;

        protected override bool SyncedCasino3rd => Request.SearchAccount.ProductStatusOf(ProductConstants.RNGCasino).Synced;
    }
}