namespace Alpha.AccountInfo.Features.Shared.Params
{
    public class KickoutCustomerParam
    {
        public KickoutCustomerParam(
            int custId,
            byte custLevelId,
            int serverId,
            string ip,
            int subAccId,
            string mode)
        {
            CustId = custId;
            CustLevelId = custLevelId;
            ServerId = serverId;
            IP = ip;
            SubAccId = subAccId;
            Mode = mode;
        }

        public int CustId { get; }

        public byte CustLevelId { get; }

        public int ServerId { get; }

        public string IP { get; }

        public int SubAccId { get; }

        public string Mode { get; }
    }
}
