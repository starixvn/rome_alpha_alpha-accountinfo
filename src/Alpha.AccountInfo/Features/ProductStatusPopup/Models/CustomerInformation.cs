namespace Alpha.AccountInfo.Models
{
    public class CustomerInformation
    {
        public CustomerInformation()
        {
        }

        public CustomerInformation(
            int custId,
            byte roleId,
            int superId,
            int masterId,
            int agentId,
            bool syncCasino)
        {
            CustId = custId;
            RoleId = roleId;
            SuperId = superId;
            MasterId = masterId;
            AgentId = agentId;
            SyncCasino = syncCasino;
        }

        public int CustId { get; }
        
        public byte RoleId { get; }

        public int SuperId { get; }

        public int MasterId { get; }

        public int AgentId { get; }

        public bool SyncCasino { get; }
    }
}
