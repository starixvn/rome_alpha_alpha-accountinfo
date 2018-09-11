namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    public interface IBalanceInfoViewModel
    {
        decimal TotalBalance { get; }

        decimal YesterdayBalance { get; }

        string TotalBalanceTransferLink { get; }

        string YesterdayBalanceTransferLink { get; }
    }
}