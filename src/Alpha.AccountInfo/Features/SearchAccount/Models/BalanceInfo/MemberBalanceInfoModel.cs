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
             decimal casinoOutstanding,
             decimal casinoTransfer,
             decimal netPosition,
             decimal memMaxCreditMax)
        {
            CustId = custId;
            Credit = credit;

            CustomerCash = customerCash;
            YesterdayCustomerCash = yesterdayCustomerCash;
            CustomerOutstanding = customerOutstanding;
            CasinoOutstanding = casinoOutstanding;

            CasinoTransfer = casinoTransfer;
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

        public decimal CasinoOutstanding { get; private set; }

        public decimal CasinoTransfer { get; private set; }
    }
}
