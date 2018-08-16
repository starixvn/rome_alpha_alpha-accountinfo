namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class UpdateGivenCreditParam : UpdateCustomerStatusParam
    {
        public UpdateGivenCreditParam(
            int searchAccountId,
            byte roleId,
            decimal creditAmount,
            int actorId,
            byte actorRoleId,
            int subAccountId,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            RoleId = roleId;
            CreditAmount = creditAmount;
            ActorRoleId = actorRoleId;
        }
        public byte RoleId { get; }

        public byte ActorRoleId { get; }

        public decimal CreditAmount { get; }
    }
}
