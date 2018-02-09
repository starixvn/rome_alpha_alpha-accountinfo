namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using System.Collections.Generic;
    using Params.ProductStatus;

    public class UpdateNG2StatusCriteria : UpdateDCSProductStatusCriteria
    {
        public UpdateNG2StatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            IEnumerable<BetTypeStatusParam> productStatusParams) : base(custId, subAccId, userId, ip, productStatusParams)
        {
        }

        public override string GetSettingKey() => "Acc_CustSetting_DisableGameStatusUpd";
    }
}
