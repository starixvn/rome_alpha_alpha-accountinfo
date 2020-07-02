namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using System.Collections.Generic;
    using Params.ProductStatus;

    public class UpdateDCSProductStatusFor4Criteria : UpdateDCSProductStatusCriteria
    {
        public UpdateDCSProductStatusFor4Criteria(
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
