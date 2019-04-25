namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;

    public class UnsuspendListViewModel : AccountInfoViewModelBase
    {
        public UnsuspendListViewModel(
            int searchAccountId,
            UnsuspendViewListModel  unsuspendDownlinesViewList)
        {
            SearchAccountId = searchAccountId;
            UnsuspendDownlinesViewList = unsuspendDownlinesViewList;
        }

        public int SearchAccountId { get; private set; }

        public UnsuspendViewListModel UnsuspendDownlinesViewList { get; private set; }
    }
}