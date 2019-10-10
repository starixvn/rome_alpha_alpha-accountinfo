namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public class MasterBalanceInfoModel : IBalanceInfoModel
    {
        public MasterBalanceInfoModel()
        {
        }

        public MasterBalanceInfoModel(
             int custId,
             decimal masterCash,
             decimal yesterdayMasterCash,
             decimal masterPositionTaking,
             decimal agentCash,
             decimal yesterdayAgentCash,
             decimal customerCash,
             decimal yesterdayCustomerCash,
             decimal customerOutstanding)

        {
            CustId = custId;

            MasterCash = masterCash;
            YesterdayMasterCash = yesterdayMasterCash;
            MasterPositionTaking = masterPositionTaking;

            AgentCash = agentCash;
            YesterdayAgentCash = yesterdayAgentCash;

            CustomerCash = customerCash;
            YesterdayCustomerCash = yesterdayCustomerCash;
            CustomerOutstanding = customerOutstanding;
        }

        public int CustId { get; private set; }

        public decimal MasterCash { get; private set; }

        public decimal YesterdayMasterCash { get; private set; }

        public decimal MasterPositionTaking { get; private set; }

        public decimal AgentCash { get; private set; }

        public decimal YesterdayAgentCash { get; private set; }

        public decimal CustomerCash { get; private set; }

        public decimal YesterdayCustomerCash { get; private set; }

        public decimal CustomerOutstanding { get; private set; }
    }
}