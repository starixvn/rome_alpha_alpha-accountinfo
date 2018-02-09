namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Models;

    public class SelectDCSProductStatusCriteria : SelectProductStatusCriteria
    {
        public SelectDCSProductStatusCriteria(int productId, int custId, byte roleId)
            : base(productId, custId)
        {
            RoleId = roleId;
        }

        public byte RoleId { get; }

        public int SettingType { get; } = 13;

        public override string GetSettingKey() => "Acc_CustSetting_StatusSettingSelect";

        public override bool IsValid()
            => base.IsValid()
            && UserLevel.Member.Value <= RoleId
            && RoleId <= UserLevel.Master.Value;
    }
}
