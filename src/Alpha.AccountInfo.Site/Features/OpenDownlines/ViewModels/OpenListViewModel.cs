namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.OpenDownlines.Models;

    public class OpenListViewModel : AccountInfoViewModelBase
    {
        public OpenListViewModel(
            int searchAccountId,
            string searchAccountName,
            OpenViewListModel data)
        {
            SearchAccountId = searchAccountId;
            SearchAccountName = searchAccountName;
            Data = data;
        }

        public int SearchAccountId { get; }

        public string SearchAccountName { get; }

        public OpenViewListModel Data { get; }
    }
}