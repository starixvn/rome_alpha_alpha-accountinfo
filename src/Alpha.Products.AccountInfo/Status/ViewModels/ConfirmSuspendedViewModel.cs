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

        private bool UplineSuspended => Request.GetData<bool>(DataKey.SearchAccount.Upline.IsSuspended);

        private bool SearchAccountSuspended => Request.GetData<bool>(DataKey.SearchAccount.IsSuspended);

        public override bool Disabled => base.Disabled || UplineSuspended ||
            Request.GetData<bool>(DataKey.SearchAccount.Upline.IsClosed);

        public override bool Checked => SearchAccountSuspended;

        public override bool ReloadCustomerInfo => true;

        internal override string SuffixMessage =>
            Request.SearchAccount.IsMember ?
                    Checked ? "InCaseUnsuspendedMemberMessage" : "InCaseSuspendedMemberMessage" :
                    Checked ? "InCaseUnsuspendedMasterOrAgentMessage" : "InCaseSuspendedMasterOrAgentMessage";

        internal override string StatusName => DataKey.StatusMapping.Suspended;

        internal override bool StatusValue => !Checked;

        public string Tooltip =>
            UplineSuspended && !Request.GetData<bool>(DataKey.SearchAccount.IsDirectSuspend)
            ? Request.GetResource("SuspendedByUplineTooltip") : string.Empty;

        public override bool IsPopup => Checked && SearchAccount.IsNotMember;

        public override string Url => Checked ?
            $"{AccountInfoUrl}UnsuspendDownlines/EditSingle?SearchAccountId={SearchAccount.Id}&SearchAccountName={SearchAccount.Username}" :
            base.Url;
    }
}