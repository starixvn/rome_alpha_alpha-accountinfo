namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class UpdateClosedParam : UpdateCustomerStatusParam
    {
        public UpdateClosedParam(
            int searchAccountId,
            string actorName,
            int actorId,
            bool closed,
            bool syncedCasino,
            int subAccountId,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            ActorName = actorName;
            Closed = closed;
            SyncedCasino = syncedCasino;
        }

        public string ActorName { get; }

        public bool Closed { get; }

        public bool SyncedCasino { get; }
    }
}
