namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.Features.Shared.Models;

    public class SelectDCSProductStatusCriteria : SelectProductStatusCriteria
    {
        private const int NotNG2SettingType = 13;

        public SelectDCSProductStatusCriteria(int productId, int custId, byte roleId)
            : base(productId, custId)
        {
            RoleId = roleId;
        }

        public byte RoleId { get; }

        public int SettingType { get; } = NotNG2SettingType;

        public byte SettingStatus => AppSettings.SettingStatus;

        public override string GetSettingKey() => "Acc_CustSetting_StatusSettingSelect";

        public override bool IsValid()
            => base.IsValid()
            && UserLevel.Member.Value <= RoleId
            && RoleId <= UserLevel.Master.Value;
    }
}