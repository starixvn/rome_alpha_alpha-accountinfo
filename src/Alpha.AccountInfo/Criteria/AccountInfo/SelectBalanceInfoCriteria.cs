namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Fanex.Data;
    using System.Collections.Generic;

    public class SelectBalanceInfoCriteria : AccountInfoCriteria
    {
        private readonly IDictionary<UserLevel, string> settingKeys = 
            new Dictionary<UserLevel, string>
            {
                {UserLevel.Master, "Age_BalanceMasterSel" },
                {UserLevel.Agent, "Age_BalanceAgentSel" },
                {UserLevel.Member, "Age_BalanceMemberSel" }
            };

        public SelectBalanceInfoCriteria(int custId, byte roleId) : base(custId)
        {
            RoleId = roleId;
        }

        [SpParam(Ignored = true)]
        public byte RoleId { get; }

        public override string GetSettingKey() => settingKeys[Enumeration.FromValue<UserLevel>(RoleId)];
    }
}
