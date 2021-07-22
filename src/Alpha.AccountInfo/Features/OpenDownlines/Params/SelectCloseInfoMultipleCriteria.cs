namespace Alpha.AccountInfo.Features.OpenDownlines.Params
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectCloseInfoMultipleCriteria : CriteriaBase
    {
        public SelectCloseInfoMultipleCriteria(string custIds, byte roleId)
        {
            CustIds = custIds;
            RoleId = roleId;
        }

        [SpParam(SqlParamName = "CustIdList")]
        public string CustIds { get; }

        public byte RoleId { get; }

        public override string GetSettingKey() => "Acc_CustomerList_CloseInfoSel";

        public override bool IsValid() => !string.IsNullOrEmpty(CustIds);
    }
}