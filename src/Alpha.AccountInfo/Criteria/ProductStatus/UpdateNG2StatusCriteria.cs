namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Params.ProductStatus;

    public class UpdateNG2StatusCriteria : UpdateDCSProductStatusCriteria
    {
        public UpdateNG2StatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            ProductStatusParam[] productStatusParams) : base(custId, subAccId, userId, ip, productStatusParams)
        {
        }

        public override string GetSettingKey() => "Acc_CustSetting_DisableGameStatusUpd";
    }
}
