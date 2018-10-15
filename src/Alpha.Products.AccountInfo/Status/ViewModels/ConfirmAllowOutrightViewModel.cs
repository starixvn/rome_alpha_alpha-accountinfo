namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmAllowOutrightViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmAllowOutrightViewModel(ISearchAccountInfoRequest request, int displayOrder = 3)
        : base(request, displayOrder)
        {
        }

        public override string ConfirmMessage
        {
            get
            {
                var suffixMessage = Checked ?  "InCaseDisabledMessage" : "InCaseEnabledMessage";

                return Request.GetResource($"{GetType().Name}{suffixMessage}");
            }
        }

        public override bool Disabled
            => base.Disabled || !Request.GetData<bool>(DataKey.SearchAccount.Upline.IsAllowedOutright);

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsAllowedOutright);

        protected override string StatusName => DataKey.StatusMapping.DisableOutright;

        protected override bool StatusValue => Checked;

        protected override bool SyncedCasino3rd => false;
    }
}