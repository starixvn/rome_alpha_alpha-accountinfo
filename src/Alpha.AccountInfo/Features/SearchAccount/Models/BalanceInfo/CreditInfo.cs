namespace Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo
{
    public class CreditInfo
    {
        public CreditInfo()
        {
        }

        public CreditInfo(
            decimal credit,
            decimal masterCreditMin,
            decimal masterCreditMax,
            decimal masterUsedCredit,
            decimal agentCreditMin,
            decimal agentCreditMax,
            decimal agentUsedCredit,
            decimal memberMaxCredit,
            decimal memMaxCreditMin,
            decimal memMaxCreditMax)
        {
            Credit = credit;
            MasterCreditMin = masterCreditMin;
            MasterCreditMax = masterCreditMax;
            MasterUsedCredit = masterUsedCredit;
            AgentCreditMin = agentCreditMin;
            AgentCreditMax = agentCreditMax;
            AgentUsedCredit = agentUsedCredit;
            MemberMaxCredit = memberMaxCredit;
            MemMaxCreditMin = memMaxCreditMin;
            MemMaxCreditMax = memMaxCreditMax;
        }

        public decimal Credit { get; }

        public decimal MasterCreditMin { get; }

        public decimal MasterCreditMax { get; }

        public decimal MasterUsedCredit { get; }

        public decimal AgentCreditMin { get; }

        public decimal AgentCreditMax { get; }

        public decimal AgentUsedCredit { get; }

        public decimal MemberMaxCredit { get; }

        public decimal MemMaxCreditMin { get; }

        public decimal MemMaxCreditMax { get; }
    }
}