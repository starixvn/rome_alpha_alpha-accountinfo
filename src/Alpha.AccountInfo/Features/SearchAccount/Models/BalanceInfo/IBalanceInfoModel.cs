namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public interface IBalanceInfoModel
    {
        int CustId { get; }

        decimal Credit { get; }

        decimal CustomerCash { get; }

        decimal YesterdayCustomerCash { get; }
    }
}
