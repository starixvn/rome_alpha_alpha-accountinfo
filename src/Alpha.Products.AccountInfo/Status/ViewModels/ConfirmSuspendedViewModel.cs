namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Shared;

    public class ConfirmSuspendedViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmSuspendedViewModel(ISearchAccountInfoRequest request, int displayOrder = 1)
         : base(request, displayOrder)
        {
        }

        public override string ConfirmMessage
        {
            get
            {
                var suffixMessage = Request.SearchAccount.IsMember ?
                    Checked ? "InCaseUnsuspendedMemberMessage" : "InCaseSuspendedMemberMessage" :
                    Checked ? "InCaseUnsuspendedMasterOrAgentMessage" : "InCaseSuspendedMasterOrAgentMessage";

                return Request.GetResource($"{GetType().Name}{suffixMessage}");
            }
        }

        public override bool Disabled => base.Disabled || Request.GetData<bool>(DataKey.SearchAccount.Upline.IsSuspended);

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsSuspended);

        protected override string StatusName => DataKey.StatusMapping.Suspended;

        protected override bool StatusValue => !Checked;

        protected override bool SyncedCasino3rd => Request.SearchAccount.ProductStatusOf(ProductConstants.RNGCasino).Synced;
    }
}