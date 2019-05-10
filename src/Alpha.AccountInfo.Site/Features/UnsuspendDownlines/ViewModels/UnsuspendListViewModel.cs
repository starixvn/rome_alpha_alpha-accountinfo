namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;

    public class UnsuspendListViewModel : AccountInfoViewModelBase
    {
        public UnsuspendListViewModel(
            int searchAccountId,
            string searchAccountName,
            bool isAffected,
            UnsuspendViewListModel data)
        {
            SearchAccountId = searchAccountId;
            SearchAccountName = searchAccountName;
            IsAffected = isAffected;
            Data = data;
        }

        public int SearchAccountId { get; private set; }

        public string SearchAccountName { get; private set; }

        public bool IsAffected { get; set; }

        public UnsuspendViewListModel Data { get; private set; }
    }
}