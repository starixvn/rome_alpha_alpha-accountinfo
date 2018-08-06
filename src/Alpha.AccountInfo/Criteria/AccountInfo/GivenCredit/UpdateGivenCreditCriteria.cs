namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public class UpdateGivenCreditCriteria : CriteriaBase
    {
        private readonly IDictionary<byte, string> settingKeys;

        public UpdateGivenCreditCriteria(
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

            settingKeys = new Dictionary<byte, string>
            {
                {4, "Age_CustomerUpdCredit_SAg" },
                {3, "Age_CustomerUpdCredit_MAg" },
                {2, "Age_CustomerUpdCredit_Ag" }
            };
        }

        public int CustId { get; }

        [SpParam(Ignored = true)]
        public byte RoleId { get; }

        [SpParam(Ignored = true)]
        public byte ActorRoleId { get; }

        [SpParam(SqlParamName = "CreditAmt")]
        public decimal CreditAmount { get; }

        [SpParam(SqlParamName = "UserId")]
        public int ActorId { get; }

        [SpParam(SqlParamName = "SubAccId")]
        public int SubAccountId { get; }

        public string Language { get; }

        public string IP { get; }

        public bool CreditProblem { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => settingKeys[ActorRoleId];

        public override bool IsValid() => CustId > 0;
    }
}
