namespace Alpha.AccountInfo.Models
{
    public class CustomerStatus
    {
        public CustomerStatus()
        {
        }

        public CustomerStatus(
            int custId,
            string userName,
            byte roleId,
            bool closed,
            bool suspend,
            int superId,
            int masterId,
            int agentId,
            bool syncCasino)
        {
            CustId = custId;
            UserName = userName;
            RoleId = roleId;
            Closed = closed;
            Suspend = suspend;
            SuperId = superId;
            MasterId = masterId;
            AgentId = agentId;
            SyncCasino = syncCasino;
        }

        public int CustId { get; }

        public string UserName { get; }

        public byte RoleId { get; }

        public bool Closed { get; }

        public bool Suspend { get; }

        public int SuperId { get; }

        public int MasterId { get; }

        public int AgentId { get; }

        public bool SyncCasino { get; }
    }
}
