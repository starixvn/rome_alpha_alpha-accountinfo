using Alpha.AccountInfo.Configurations;
using Alpha.AccountInfo.Criteria.ProductStatus;

namespace Alpha.AccountInfo.Features.ProductStatusPopup.Params.SPs
{
    public class UpdateProductGroupStatusCriteria : NonQueryProductStatusCriteria
    {
        public UpdateProductGroupStatusCriteria(
            int custId,
            int groupId,
            string isDisabled,
            int subAccId,
            int actorId,
            string ip)
            : base(custId, subAccId, actorId, ip)
        {
            GroupId = groupId;
            IsDisabled = isDisabled;
        }

        public string IsDisabled { get; }

        public int GroupId { get; }

        public byte SettingStatus => AppSettings.SettingStatus;

        public override string GetSettingKey() => "Acc_CustSetting_DisableProductStatus_160_Upd";
    }
}