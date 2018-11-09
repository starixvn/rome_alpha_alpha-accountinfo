using Fanex.Data;

namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectTotalDownlineSummaryCriteria : AccountInfoCriteriaBase
    {
        public SelectTotalDownlineSummaryCriteria(int searchAccountId, byte roleId) : base(searchAccountId)
        {
            RoleId = roleId;
        }

        public byte RoleId { get; }

        public override string GetSettingKey() => "Acc_CustomerSelTotalDownline";
    }
}
