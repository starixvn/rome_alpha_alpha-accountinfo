namespace Alpha.AccountInfo.Models.AccountInfo
{
    using System;

    public class CustomerCashInfoModel
    {
        public CustomerCashInfoModel(
             double masterCash,
             double yesterday_MasterCash,
             double masterPositionTaking,
             double agentCash,
             double yesterday_AgentCash,
             double customerCash,
             double yesterday_CustomerCash,
             double customerOutstanding,
             double casinoOutstanding,
             double casinoTransfer,
             DateTime lastTxn,
             double masterUsedCredit)
        {
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
            LastTxn = lastTxn;
            MasterUsedCredit = masterUsedCredit;
        }

        public double MasterCash { get; }

        public double Yesterday_MasterCash { get; }

        public double MasterPositionTaking { get; }

        public double AgentCash { get; }

        public double Yesterday_AgentCash { get; }

        public double CustomerCash { get; }

        public double Yesterday_CustomerCash { get; }

        public double CustomerOutstanding { get; }

        public double CasinoOutstanding { get; }

        public double CasinoTransfer { get; }

        public DateTime LastTxn { get; }

        public double MasterUsedCredit { get; }
    }
}
