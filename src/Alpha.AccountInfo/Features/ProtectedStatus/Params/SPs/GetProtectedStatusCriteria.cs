namespace Alpha.AccountInfo.Features.ProtectedStatus.Params.SPs
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Fanex.Data.Repository;

    public class GetProtectedStatusCriteria : CriteriaBase
    {
        public GetProtectedStatusCriteria(GetProtectedStatusParam param)
        {
            CustId = param.ViewerId;
            RoleId = param.ViewerLevelId;
            CustList = param.CustIds;
        }

        public int CustId { get; }

        public byte RoleId { get; }

        public string CustList { get; }

        public override string GetSettingKey()
            => "Age_CustomerSelProtectionModeList";

        public override bool IsValid()
            => CustId > 0
            && (RoleId == UserLevel.Super.Value || RoleId == UserLevel.Master.Value)
            && !string.IsNullOrWhiteSpace(CustList);
    }
}
