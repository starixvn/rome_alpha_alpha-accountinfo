namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    public interface IBalanceInfoViewModel
    {
        decimal TotalBalance { get; }

        decimal YesterdayBalance { get; }

        string TransferLinkOnTotalBalance { get; }

        string TransferLinkOnYesterdayBalance { get; }
    }
}