namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;

    public class ConfirmDoubleCommViewModel : ConfirmStatusItemViewModelBase
    {
        public ConfirmDoubleCommViewModel(ISearchAccountInfoRequest request, int displayOrder = 5)
           : base(request, displayOrder)
        {
        }

        public override string ConfirmMessage
        {
            get
            {
                var suffixMessage = Request.SearchAccount.IsMember ?
                    Checked ? "InCaseDisabledMemberMessage" : "InCaseEnabledMemberMessage" :
                    Checked ? "InCaseDisabledMasterOrAgentMessage" : "InCaseEnableMasterOrAgentMessage" ;

                return Request.GetResource($"{GetType().Name}{suffixMessage}");
            }
        }

        public override bool Disabled
            => base.Disabled
                || !Request.GetData<bool>(DataKey.SearchAccount.Upline.IsAllowedDoubleComm)
                || Request.ActorIsNotEdittingDirectDownline;

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsAllowedDoubleComm);

        protected override string StatusName => DataKey.StatusMapping.DoubleComm;

        protected override bool StatusValue => !Checked;

        protected override bool SyncedCasino3rd => false;
    }
}