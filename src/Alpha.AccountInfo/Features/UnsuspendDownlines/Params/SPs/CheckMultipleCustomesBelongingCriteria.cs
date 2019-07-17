namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class CheckMultipleCustomesBelongingCriteria : NonQueryCommand
    {
        public CheckMultipleCustomesBelongingCriteria(int actorId, int subAccountId, string custIds)
        {
            ActorId = actorId;
            SubAccountId = subAccountId;
            CustIds = custIds;
        }

        [SpParam(SqlParamName = "SessionCustID")]
        public int ActorId { get; }

        public int SubAccountId { get; }

        [SpParam(SqlParamName = "custid")]
        public string CustIds { get; }

        public byte RoleId { get; set; }

        public bool IsActive { get; set; }

        public override string GetSettingKey() => "Age_CustomerSelCheckBelonging_MultiCust";

        public override bool IsValid() => !string.IsNullOrEmpty(CustIds);
    }
}
