namespace Alpha.AccountInfo.Site.ViewModels.SearchAccount
{
    public interface IBalanceInfoViewModel
    {
        decimal TotalBalance { get; }

        decimal YesterdayBalance { get; }

        string TotalBalanceTransferLink { get; }

        string YesterdayBalanceTransferLink { get; }
    }
}