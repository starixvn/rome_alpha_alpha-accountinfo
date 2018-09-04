namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class MasterBalanceInfoModel : IBalanceInfoModel
    {
        public MasterBalanceInfoModel()
        {
        }

        public MasterBalanceInfoModel(
             int custId,
             decimal credit,
             decimal masterCash,
             decimal yesterday_MasterCash,
             decimal masterPositionTaking,
             decimal agentCash,
             decimal yesterday_AgentCash,
             decimal customerCash,
             decimal yesterday_CustomerCash,
             decimal customerOutstanding,
             decimal casinoOutstanding,
             decimal casinoTransfer,
             decimal masterUsedCredit,
             decimal masterCredit_Min,
             decimal masterCredit_Max)
        {
            CustId = custId;
            Credit = credit;

            MasterCash = masterCash;
            Yesterday_MasterCash = yesterday_MasterCash;
            MasterPositionTaking = masterPositionTaking;

            AgentCash = agentCash;
            Yesterday_AgentCash = yesterday_AgentCash;

            CustomerCash = customerCash;
            Yesterday_CustomerCash = yesterday_CustomerCash;
            CustomerOutstanding = customerOutstanding;
            CasinoOutstanding = casinoOutstanding;

            CasinoTransfer = casinoTransfer;
            MasterUsedCredit = masterUsedCredit;

            MasterCredit_Min = masterCredit_Min;
            MasterCredit_Max = masterCredit_Max;
        }

        public int CustId { get; }

        public decimal Credit { get; }

        public decimal MasterCash { get; }

        public decimal Yesterday_MasterCash { get; }

        public decimal MasterCredit_Max { get; }

        public decimal MasterCredit_Min { get; }

        public decimal MasterPositionTaking { get; }

        public decimal AgentCash { get; }

        public decimal Yesterday_AgentCash { get; }

        public decimal CustomerCash { get; }

        public decimal Yesterday_CustomerCash { get; }

        public decimal CustomerOutstanding { get; }

        public decimal CasinoOutstanding { get; }

        public decimal CasinoTransfer { get; }

        public decimal MasterUsedCredit { get; }
    }
}
