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

        public override bool Disabled => base.Disabled || 
            Request.GetData<bool>(DataKey.SearchAccount.Upline.IsClosed) ||
            Request.GetData<bool>(DataKey.SearchAccount.Upline.IsSuspended);

        public override bool Checked => Request.GetData<bool>(DataKey.SearchAccount.IsSuspended);

        public override bool ReloadCustomerInfo => true;

        internal override string SuffixMessage =>
            Request.SearchAccount.IsMember ?
                    Checked ? "InCaseUnsuspendedMemberMessage" : "InCaseSuspendedMemberMessage" :
                    Checked ? "InCaseUnsuspendedMasterOrAgentMessage" : "InCaseSuspendedMasterOrAgentMessage";

        internal override string StatusName => DataKey.StatusMapping.Suspended;

        internal override bool StatusValue => !Checked;

        public override bool IsPopup => Checked;

        public override string Url => Checked ?
            $"{AccountInfoUrl}UnsuspendDownlines/EditSingle?SearchAccountId={SearchAccount.Id}&SearchAccountName={SearchAccount.Username}" :
            base.Url;
    }
}