namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data;
    using System.Collections.Generic;

    public class UpdateGivenCreditCriteria : UpdateCustomerStatusCriteria
    {
        private readonly IDictionary<byte, string> settingKeys;

        public UpdateGivenCreditCriteria(
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

            settingKeys = new Dictionary<byte, string>
            {
                {4, "Age_CustomerUpdCredit_SAg" },
                {3, "Age_CustomerUpdCredit_MAg" },
                {2, "Age_CustomerUpdCredit_Ag" }
            };
        }

        [SpParam(Ignored = true)]
        public byte RoleId { get; }

        [SpParam(Ignored = true)]
        public byte ActorRoleId { get; }

        [SpParam(SqlParamName = "CreditAmt")]
        public decimal CreditAmount { get; }

        public override string GetSettingKey() => settingKeys[ActorRoleId];
    }
}
