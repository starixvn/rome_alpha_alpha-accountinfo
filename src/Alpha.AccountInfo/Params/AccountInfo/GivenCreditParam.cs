namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class GivenCreditParam
    {
        public GivenCreditParam()
        {
        }

        public GivenCreditParam(
            int custId,
            byte roleId,
            decimal creditAmount,
            int actorId,
            byte actorRoleId,
            int subAccountId,
            string language,
            string ip)
        {
            CustId = custId;
            RoleId = roleId;
            CreditAmount = creditAmount;
            ActorId = actorId;
            ActorRoleId = actorRoleId;
            SubAccountId = subAccountId;
            Language = language;
            IP = ip;
        }

        public int CustId { get; }

        public byte RoleId { get; }

        public byte ActorRoleId { get; }

        public decimal CreditAmount { get; }

        public int ActorId { get; }

        public int SubAccountId { get; }

        public string Language { get; }

        public string IP { get; }

    }
}
