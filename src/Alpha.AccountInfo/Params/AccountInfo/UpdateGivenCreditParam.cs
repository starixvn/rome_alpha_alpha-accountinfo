namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class UpdateGivenCreditParam
    {
        public UpdateGivenCreditParam(
            int searchAccountId,
            byte roleId,
            decimal creditAmount,
            int actorId,
            byte actorRoleId,
            int subAccountId,
            string language,
            string ip) 
        {
            SearchAccountId = searchAccountId;
            ActorId = actorId;
            SubAccountId = subAccountId;
            Language = language;
            IP = ip;
            RoleId = roleId;
            CreditAmount = creditAmount;
            ActorRoleId = actorRoleId;
        }

        public int SearchAccountId { get; }

        public int ActorId { get; }

        public int SubAccountId { get; }

        public string Language { get; }

        public string IP { get; }

        public byte RoleId { get; }

        public byte ActorRoleId { get; }

        public decimal CreditAmount { get; }
    }
}
