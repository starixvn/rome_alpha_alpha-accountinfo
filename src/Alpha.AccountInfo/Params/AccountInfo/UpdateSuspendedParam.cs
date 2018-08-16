namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class UpdateSuspendedParam : UpdateCustomerStatusParam
    {
        public UpdateSuspendedParam(
            int searchAccountId,
            string actorName,
            int actorId,
            bool suspended,
            bool syncedCasino,
            int subAccountId,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            ActorName = actorName;
            Suspended = suspended;
            SyncedCasino = syncedCasino;
        }

        public string ActorName { get; }

        public bool Suspended { get; }

        public bool SyncedCasino { get; }
    }
}
