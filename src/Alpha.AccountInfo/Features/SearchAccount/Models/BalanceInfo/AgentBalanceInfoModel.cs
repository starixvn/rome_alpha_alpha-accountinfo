namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
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
             decimal yesterdayAgentCash,
             decimal customerCash,
             decimal yesterdayCustomerCash,
             decimal customerOutstanding,
             decimal casinoOutstanding,
             decimal agentPositionTaking,
             decimal casinoTransfer,
             decimal masterUsedCredit,
             decimal agentUsedCredit,
             decimal agentCreditMin,
             decimal agentCreditMax,
             decimal memMaxCreditMin,
             decimal memberMaxCredit)
        {
            CustId = custId;
            Credit = credit;

            AgentCash = agentCash;
            YesterdayAgentCash = yesterdayAgentCash;

            CustomerCash = customerCash;
            YesterdayCustomerCash = yesterdayCustomerCash;
            CustomerOutstanding = customerOutstanding;
            CasinoOutstanding = casinoOutstanding;

            AgentPositionTaking = agentPositionTaking;
            CasinoTransfer = casinoTransfer;
            MasterUsedCredit = masterUsedCredit;
            AgentUsedCredit = agentUsedCredit;

            AgentCreditMin = agentCreditMin;
            AgentCreditMax = agentCreditMax;

            MemMaxCreditMin = memMaxCreditMin;
            MemberMaxCredit = memberMaxCredit;
        }

        public int CustId { get; private set; }

        public decimal Credit { get; private set; }

        public decimal AgentCreditMin { get; private set; }

        public decimal AgentCreditMax { get; private set; }

        public decimal MemMaxCreditMin { get; private set; }

        public decimal AgentCash { get; private set; }

        public decimal YesterdayAgentCash { get; private set; }

        public decimal CustomerCash { get; private set; }

        public decimal YesterdayCustomerCash { get; private set; }

        public decimal AgentPositionTaking { get; private set; }

        public decimal CustomerOutstanding { get; private set; }

        public decimal CasinoOutstanding { get; private set; }

        public decimal CasinoTransfer { get; private set; }

        public decimal AgentUsedCredit { get; private set; }

        public decimal MemberMaxCredit { get; private set; }

        public decimal MasterUsedCredit { get; private set; }
    }
}
