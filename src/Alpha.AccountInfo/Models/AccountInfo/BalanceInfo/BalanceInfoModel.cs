namespace Alpha.AccountInfo.Models.AccountInfo
{
    using System;

    public class BalanceInfoModel
    {
        public BalanceInfoModel(
             decimal masterCash,
             decimal yesterday_MasterCash,
             decimal masterPositionTaking,
             decimal agentCash,
             decimal yesterday_AgentCash,
             decimal agentPositionTaking,
             decimal customerCash,
             decimal yesterday_CustomerCash,
             decimal customerOutstanding,
             decimal casinoOutstanding,
             decimal casinoTransfer,
             decimal masterUsedCredit,
             decimal agentUsedCredit,
             decimal netPosition,
             decimal masterCredit_Min,
             decimal masterCredit_Max,
             decimal agentCredit_Min,
             decimal agentCredit_Max,
             decimal memMaxCredit_Min,
             decimal memMaxCredit_Max)
        {
            MasterCash = masterCash;
            Yesterday_MasterCash = yesterday_MasterCash;
            MasterPositionTaking = masterPositionTaking;

            AgentCash = agentCash;
            Yesterday_AgentCash = yesterday_AgentCash;
            AgentPositionTaking = agentPositionTaking;

            CustomerCash = customerCash;
            Yesterday_CustomerCash = yesterday_CustomerCash;
            CustomerOutstanding = customerOutstanding;
            CasinoOutstanding = casinoOutstanding;

            CasinoTransfer = casinoTransfer;
            MasterUsedCredit = masterUsedCredit;
            AgentUsedCredit = agentUsedCredit;

            NetPosition = netPosition;
            MasterCredit_Min = masterCredit_Min;
            MasterCredit_Max = masterCredit_Max;
            AgentCredit_Min = agentCredit_Min;
            AgentCredit_Max = agentCredit_Max;
            MemMaxCredit_Min = memMaxCredit_Min;
            MemMaxCredit_Max = memMaxCredit_Max;
        }

        public decimal MasterCash { get; }

        public decimal Yesterday_MasterCash { get; }

        public decimal MasterPositionTaking { get; }

        public decimal AgentCash { get; }

        public decimal Yesterday_AgentCash { get; }

        public decimal AgentPositionTaking { get; }

        public decimal CustomerCash { get; }

        public decimal Yesterday_CustomerCash { get; }

        public decimal CustomerOutstanding { get; }

        public decimal CasinoOutstanding { get; }

        public decimal CasinoTransfer { get; }

        public decimal MasterUsedCredit { get; }

        public decimal AgentUsedCredit { get; }

        public decimal NetPosition { get; }

        public decimal MasterCredit_Min { get; }

        public decimal MasterCredit_Max { get; }

        public decimal AgentCredit_Min { get; }

        public decimal AgentCredit_Max { get; }

        public decimal MemMaxCredit_Min { get; }

        public decimal MemMaxCredit_Max { get; }
    }
}
