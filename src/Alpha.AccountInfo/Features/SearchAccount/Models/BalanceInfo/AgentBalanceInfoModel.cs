namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public class AgentBalanceInfoModel : IBalanceInfoModel
    {
        public AgentBalanceInfoModel()
        {
        }

        public AgentBalanceInfoModel(
             int custId,
             decimal agentCash,
             decimal yesterdayAgentCash,
             decimal customerCash,
             decimal yesterdayCustomerCash,
             decimal customerOutstanding,
             decimal agentPositionTaking,
             decimal pendingTransfer3rd)
        {
            CustId = custId;

            AgentCash = agentCash;
            YesterdayAgentCash = yesterdayAgentCash;

            CustomerCash = customerCash;
            YesterdayCustomerCash = yesterdayCustomerCash;
            CustomerOutstanding = customerOutstanding;

            AgentPositionTaking = agentPositionTaking;
            PendingTransfer3rd = pendingTransfer3rd;
        }

        public int CustId { get; private set; }

        public decimal AgentCash { get; private set; }

        public decimal YesterdayAgentCash { get; private set; }

        public decimal CustomerCash { get; private set; }

        public decimal YesterdayCustomerCash { get; private set; }

        public decimal AgentPositionTaking { get; private set; }

        public decimal CustomerOutstanding { get; private set; }

        public decimal PendingTransfer3rd { get; private set; }
    }
}