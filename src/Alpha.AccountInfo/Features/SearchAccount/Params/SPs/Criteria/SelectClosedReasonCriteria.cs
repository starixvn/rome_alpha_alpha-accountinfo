namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectClosedReasonCriteria : AccountInfoCriteriaBase
    {
        public SelectClosedReasonCriteria(int searchAccountId, byte roleId) : base(searchAccountId)
            => RoleId = roleId;

        public byte RoleId { get; }

        public override string GetSettingKey()
            => "Age_ClosedReasonSelByCust";

        public override bool IsValid()
            => base.IsValid() && RoleId > 0;
    }
}
