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
            int agentId)
        {
            CustId = custId;
            RoleId = roleId;
            SuperId = superId;
            MasterId = masterId;
            AgentId = agentId;
        }

        public int CustId { get; }
        
        public byte RoleId { get; }

        public int SuperId { get; }

        public int MasterId { get; }

        public int AgentId { get; }
    }
}
