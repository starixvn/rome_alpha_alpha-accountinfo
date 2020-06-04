namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public interface IBalanceInfoModel
    {
        int CustId { get; }

        decimal CustomerCash { get; }

        decimal YesterdayCustomerCash { get; }

        decimal PendingTransfer3rd { get; }
    }
}