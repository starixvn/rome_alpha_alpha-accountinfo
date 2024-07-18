using Alpha.AccountInfo.Configurations;
using Fanex.Data.Repository;

namespace Alpha.AccountInfo.Features.ProductStatusPopup.Params.SPs
{
    public class SelectAllProductStatusCriteria : CriteriaBase
    {
        public SelectAllProductStatusCriteria(int custId, int roleId)
        {
            CustId = custId;
            RoleId = roleId;
        }

        public int CustId { get; }

        public int RoleId { get; }

        public byte SettingStatus { get; } = AppSettings.SettingStatus;

        public override string GetSettingKey() => "Acc_CustSetting_StatusSettingSelect";

        public override bool IsValid()
            => CustId > 0;
    }
}