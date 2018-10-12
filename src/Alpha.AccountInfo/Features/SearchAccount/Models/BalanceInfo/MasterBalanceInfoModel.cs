namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
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
             decimal yesterdayMasterCash,
             decimal masterPositionTaking,
             decimal agentCash,
             decimal yesterdayAgentCash,
             decimal customerCash,
             decimal yesterdayCustomerCash,
             decimal customerOutstanding,
             decimal casinoOutstanding,
             decimal casinoTransfer,
             decimal masterUsedCredit,
             decimal masterCreditMin,
             decimal masterCreditMax)
        {
            CustId = custId;
            Credit = credit;

            MasterCash = masterCash;
            YesterdayMasterCash = yesterdayMasterCash;
            MasterPositionTaking = masterPositionTaking;

            AgentCash = agentCash;
            YesterdayAgentCash = yesterdayAgentCash;

            CustomerCash = customerCash;
            YesterdayCustomerCash = yesterdayCustomerCash;
            CustomerOutstanding = customerOutstanding;
            CasinoOutstanding = casinoOutstanding;

            CasinoTransfer = casinoTransfer;
            MasterUsedCredit = masterUsedCredit;

            MasterCreditMin = masterCreditMin;
            MasterCreditMax = masterCreditMax;
        }

        public int CustId { get; private set; }

        public decimal Credit { get; private set; }

        public decimal MasterCash { get; private set; }

        public decimal YesterdayMasterCash { get; private set; }

        public decimal MasterCreditMax { get; private set; }

        public decimal MasterCreditMin { get; private set; }

        public decimal MasterPositionTaking { get; private set; }

        public decimal AgentCash { get; private set; }

        public decimal YesterdayAgentCash { get; private set; }

        public decimal CustomerCash { get; private set; }

        public decimal YesterdayCustomerCash { get; private set; }

        public decimal CustomerOutstanding { get; private set; }

        public decimal CasinoOutstanding { get; private set; }

        public decimal CasinoTransfer { get; private set; }

        public decimal MasterUsedCredit { get; private set; }
    }
}
