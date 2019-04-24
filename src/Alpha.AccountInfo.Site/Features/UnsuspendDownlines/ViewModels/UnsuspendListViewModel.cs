namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;

    public class UnsuspendListViewModel : AccountInfoViewModelBase
    {
        public UnsuspendListViewModel(
            int searchAccountId,
            byte searchAccountRoleId,
            string custIds,
            UnsuspendViewListModel  unsuspendDownlinesViewList)
        {
            SearchAccountId = searchAccountId;
            SearchAccountRoleId = searchAccountRoleId;
            CustIds = custIds;
            UnsuspendDownlinesViewList = unsuspendDownlinesViewList;
        }

        public int SearchAccountId { get; private set; }

        public byte SearchAccountRoleId { get; private set; }

        public string CustIds { get; private set; }

        public string PreviousPage =>
            string.IsNullOrEmpty(CustIds) ?
            $"{AccountInfoUrl}UnsuspendDownlines/EditSingle?SearchAccountId={SearchAccountId}" :
            $"{AccountInfoUrl}UnsuspendDownlines/EditMultiple?CustIds={CustIds}&RoleId={SearchAccountRoleId}";

        public UnsuspendViewListModel UnsuspendDownlinesViewList { get; private set; }
    }
}