namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.Products.Core.Shared;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public class UpdateGivenCreditCriteria : CriteriaBase
    {
        private readonly IDictionary<UserLevel, string> settingKeys = 
            new Dictionary<UserLevel, string>
            {
                {UserLevel.Super, "Age_CustomerUpdCredit_SAg" },
                {UserLevel.Master, "Age_CustomerUpdCredit_MAg" },
                {UserLevel.Agent, "Age_CustomerUpdCredit_Ag" }
            };

        public UpdateGivenCreditCriteria(
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
            SearchAccountRoleId = roleId;
            CreditAmount = creditAmount;
            ActorId = actorId;
            ActorRoleId = actorRoleId;
            SubAccountId = subAccountId;
            Language = language;
            IP = ip;
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        [SpParam(Ignored = true)]
        public byte SearchAccountRoleId { get; }

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

        public override string GetSettingKey() => settingKeys[Enumeration.FromValue<UserLevel>(ActorRoleId)];

        public override bool IsValid() => SearchAccountId > 0;
    }
}
