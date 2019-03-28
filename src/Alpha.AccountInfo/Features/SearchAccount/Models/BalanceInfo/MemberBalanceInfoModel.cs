namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public class MemberBalanceInfoModel : IBalanceInfoModel
    {
        public MemberBalanceInfoModel()
        {
        }

        public MemberBalanceInfoModel(
             int custId,
             decimal credit,
             decimal customerCash,
             decimal yesterdayCustomerCash,
             decimal customerOutstanding,
             decimal netPosition,
             decimal memMaxCreditMax)
        {
            CustId = custId;
            Credit = credit;

            CustomerCash = customerCash;
            YesterdayCustomerCash = yesterdayCustomerCash;

            CustomerOutstanding = customerOutstanding;
            NetPosition = netPosition;
            MemMaxCreditMax = memMaxCreditMax;
        }

        public int CustId { get; private set; }

        public decimal Credit { get; private set; }

        public decimal NetPosition { get; private set; }

        public decimal MemMaxCreditMax { get; private set; }

        public decimal CustomerCash { get; private set; }

        public decimal YesterdayCustomerCash { get; private set; }

        public decimal CustomerOutstanding { get; private set; }
    }
}
