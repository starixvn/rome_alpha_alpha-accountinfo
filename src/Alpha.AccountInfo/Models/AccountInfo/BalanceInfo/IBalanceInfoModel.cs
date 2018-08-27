namespace Alpha.AccountInfo.Models.AccountInfo
{
    public interface IBalanceInfoModel
    {
        int CustId { get; }

        decimal Credit { get; }

        decimal CustomerCash { get; }

        decimal Yesterday_CustomerCash { get; }
    }
}
