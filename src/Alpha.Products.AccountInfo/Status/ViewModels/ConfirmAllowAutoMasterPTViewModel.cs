namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmAllowAutoMasterPTViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmAllowAutoMasterPTViewModel(ISearchAccountInfoRequest request, int displayOrder = 4)
          : base(request, displayOrder)
        {
        }

        public override string ConfirmMessage
        {
            get
            {
                var suffixMessage = Checked ? "InCaseDisabledMessage" :"InCaseEnabledMessage";

                return Request.GetResource($"{GetType().Name}{suffixMessage}");
            }
        }

        public override bool Visible
            => base.Visible
            && Actor.IsSuper
            && SearchAccount.IsMaster;

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsAllowedMasterAutoPT);

        protected override string StatusName => DataKey.StatusMapping.DisableAutoMasterPT;

        protected override bool StatusValue => Checked;

        protected override bool SyncedCasino3rd => false;
    }
}