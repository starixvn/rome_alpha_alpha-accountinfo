namespace Alpha.AccountInfo.Features.SearchAccount.Params
{
    public class UpdateRnGCasino3rdParam
    {
        public UpdateRnGCasino3rdParam(
            int searchAccountId,
            string statusName,
            bool statusValue,
            int actorId,
            string actorName,
            string ip)
        {
            SearchAccountId = searchAccountId;
            ActorId = actorId;
            IP = ip;
            StatusValue = statusValue;
            StatusName = statusName;
            ActorName = actorName;
        }

        public int SearchAccountId { get; }

        public int ActorId { get; }

        public string ActorName { get; }

        public string IP { get; }

        public bool StatusValue { get; }

        public string StatusName { get; }
    }
}
