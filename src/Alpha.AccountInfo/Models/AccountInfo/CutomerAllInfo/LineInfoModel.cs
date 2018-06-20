namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class LineInfoModel
    {
        public LineInfoModel(
             int memberId,
             string memberName,
             int agentId,
             string agentName,
             int masterId,
             string masterName,
             int superId,
             string superName)
        {
            MemberId = memberId;
            MemberName = memberName;

            AgentId = agentId;
            AgentName = agentName;

            MasterId = masterId;
            MasterName = masterName;

            SuperId = superId;
            SuperName = superName;
        }

        public int MemberId { get; }

        public string MemberName { get; }

        public int AgentId { get; }

        public string AgentName { get; }

        public int MasterId { get; }

        public string MasterName { get; }

        public int SuperId { get; }

        public string SuperName { get; }
    }
}
