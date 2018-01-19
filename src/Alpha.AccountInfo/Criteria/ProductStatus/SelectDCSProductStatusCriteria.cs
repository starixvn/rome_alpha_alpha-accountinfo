namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Enumerations;
    using Models;

    public class SelectDCSProductStatusCriteria : SelectProductStatusCriteria
    {
        public SelectDCSProductStatusCriteria(int productId, int custId, byte roleId, int settingType = 13)
            : base(productId, custId)
        {
            RoleId = roleId;
            SettingType = settingType;
        }

        public byte RoleId { get; }

        public int SettingType { get; }

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_StatusSettingSelect";
        }

        public override bool IsValid()
        {
            var userRole = Enumeration.FromValue<UserLevel>(RoleId);

            return base.IsValid() && UserLevel.Member <= userRole && userRole <= UserLevel.Master && SettingType > 0;
        }
    }
}
