namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class AgentBalanceInfoModel : IBalanceInfoModel
    {
        public AgentBalanceInfoModel()
        {
        }

        public AgentBalanceInfoModel(
             int custId,
             decimal credit,
             decimal agentCash,
             decimal yesterday_AgentCash,
             decimal customerCash,
             decimal yesterday_CustomerCash,
             decimal customerOutstanding,
             decimal casinoOutstanding,
             decimal agentPositionTaking,
             decimal casinoTransfer,
             decimal masterUsedCredit,
             decimal agentUsedCredit,
             decimal agentCredit_Min,
             decimal agentCredit_Max,
             decimal memMaxCredit_Min,
             decimal memberMaxCredit)
        {
            CustId = custId;
            Credit = credit;

            AgentCash = agentCash;
            Yesterday_AgentCash = yesterday_AgentCash;

            CustomerCash = customerCash;
            Yesterday_CustomerCash = yesterday_CustomerCash;
            CustomerOutstanding = customerOutstanding;
            CasinoOutstanding = casinoOutstanding;

            AgentPositionTaking = agentPositionTaking;
            CasinoTransfer = casinoTransfer;
            MasterUsedCredit = masterUsedCredit;
            AgentUsedCredit = agentUsedCredit;

            AgentCredit_Min = agentCredit_Min;
            AgentCredit_Max = agentCredit_Max;

            MemMaxCredit_Min = memMaxCredit_Min;
            MemberMaxCredit = memberMaxCredit;
        }

        public int CustId { get; }

        public decimal Credit { get; }

        public decimal AgentCredit_Min { get; }

        public decimal AgentCredit_Max { get; }

        public decimal MemMaxCredit_Min { get; }

        public decimal AgentCash { get; }

        public decimal Yesterday_AgentCash { get; }

        public decimal CustomerCash { get; }

        public decimal Yesterday_CustomerCash { get; }

        public decimal AgentPositionTaking { get; }

        public decimal CustomerOutstanding { get; }

        public decimal CasinoOutstanding { get; }

        public decimal CasinoTransfer { get; }

        public decimal AgentUsedCredit { get; }

        public decimal MemberMaxCredit { get; }

        public decimal MasterUsedCredit { get; }
    }
}
