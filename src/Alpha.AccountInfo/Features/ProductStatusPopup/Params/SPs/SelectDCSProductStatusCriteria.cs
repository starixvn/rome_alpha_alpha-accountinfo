namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.Features.Shared.Models;

    public class SelectDCSProductStatusCriteria : SelectProductStatusCriteria
    {
        private const short DefaultDcsSettingType = 13;

        public SelectDCSProductStatusCriteria(int productId, int custId, byte roleId)
            : base(productId, custId)
        {
            RoleId = roleId;
        }

        public byte RoleId { get; }

        public virtual short SettingType { get; } = DefaultDcsSettingType;

        public byte SettingStatus => AppSettings.SettingStatus;

        public override string GetSettingKey() => "Acc_CustSetting_StatusSettingSelect";

        public override bool IsValid()
            => base.IsValid()
            && UserLevel.Member.Value <= RoleId
            && RoleId <= UserLevel.Master.Value;
    }
}