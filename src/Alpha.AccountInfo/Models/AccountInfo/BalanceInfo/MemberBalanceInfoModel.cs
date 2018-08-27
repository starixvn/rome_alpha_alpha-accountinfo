namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class MemberBalanceInfoModel : IBalanceInfoModel
    {
        public MemberBalanceInfoModel()
        {
        }

        public MemberBalanceInfoModel(
             int custId,
             decimal credit,
             decimal agentPositionTaking,
             decimal customerCash,
             decimal yesterday_CustomerCash,
             decimal customerOutstanding,
             decimal casinoOutstanding,
             decimal casinoTransfer,
             decimal netPosition,
             decimal memMaxCredit_Max)
        {
            CustId = custId;
            Credit = credit;

            CustomerCash = customerCash;
            Yesterday_CustomerCash = yesterday_CustomerCash;
            CustomerOutstanding = customerOutstanding;
            CasinoOutstanding = casinoOutstanding;

            CasinoTransfer = casinoTransfer;
            NetPosition = netPosition;
            MemMaxCredit_Max = memMaxCredit_Max;
        }

        public int CustId { get; }

        public decimal Credit { get; }

        public decimal NetPosition { get; }

        public decimal MemMaxCredit_Max { get; }

        public decimal CustomerCash { get; }

        public decimal Yesterday_CustomerCash { get; }

        public decimal CustomerOutstanding { get; }

        public decimal CasinoOutstanding { get; }

        public decimal CasinoTransfer { get; }
    }
}
