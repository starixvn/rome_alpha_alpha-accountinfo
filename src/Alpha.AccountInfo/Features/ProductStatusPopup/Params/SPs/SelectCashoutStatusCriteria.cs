namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.Features.Shared.Models;

    public class SelectCashoutStatusCriteria : SelectProductStatusCriteria
    {
        private const int CashoutSettingType = 4;

        public SelectCashoutStatusCriteria(int productId, int custId, byte roleId)
            : base(productId, custId)
        {
            RoleId = roleId;
        }

        public byte RoleId { get; }

        public int SettingType { get; } = CashoutSettingType;

        public byte SettingStatus => AppSettings.SettingStatus;

        public override string GetSettingKey() => "Acc_CustSetting_StatusSettingSelect";

        public override bool IsValid()
            => base.IsValid()
            && UserLevel.Member.Value <= RoleId
            && RoleId <= UserLevel.Master.Value;
    }
}