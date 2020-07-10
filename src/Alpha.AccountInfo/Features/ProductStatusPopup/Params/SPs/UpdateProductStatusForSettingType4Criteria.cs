namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using System.Collections.Generic;
    using Params.ProductStatus;

    public class UpdateProductStatusForSettingType4Criteria : UpdateDCSProductStatusCriteria
    {
        public UpdateProductStatusForSettingType4Criteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            IEnumerable<BetTypeStatusParam> productStatusParams) : base(custId, subAccId, userId, ip, productStatusParams)
        {
        }

        public override string GetSettingKey() => "Acc_CustSetting_DisableProductStatusUpd";
    }
}
