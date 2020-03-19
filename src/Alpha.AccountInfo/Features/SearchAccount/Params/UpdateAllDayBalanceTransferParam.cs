namespace Alpha.AccountInfo.Features.SearchAccount.Params
{
    public class UpdateAllDayBalanceTransferParam
    {
        public UpdateAllDayBalanceTransferParam(
            int searchAccountId,
            int actorId,
            string language,
            string ip,
            bool statusValue,
            string statusName)
        {
            SearchAccountId = searchAccountId;
            ActorId = actorId;
            Language = language;
            IP = ip;
            StatusValue = statusValue;
            StatusName = statusName;
        }

        public int SearchAccountId { get; }

        public int ActorId { get; }

        public string Language { get; }

        public string IP { get; }

        public bool StatusValue { get; }

        public string StatusName { get; }
    }
}