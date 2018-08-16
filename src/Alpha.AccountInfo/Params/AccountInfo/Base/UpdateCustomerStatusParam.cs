namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class UpdateCustomerStatusParam
    {
        public UpdateCustomerStatusParam(
            int searchAccountId,
            int actorId,
            int subAccountId,
            string language,
            string ip)
        {
            SearchAccountId = searchAccountId;
            ActorId = actorId;
            SubAccountId = subAccountId;
            Language = language;
            IP = ip;
        }

        public int SearchAccountId { get; }

        public int ActorId { get; }

        public int SubAccountId { get; }

        public string Language { get; }

        public string IP { get; }
    }
}
