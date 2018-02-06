namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using System.Text;
    using Params.ProductStatus;

    public class UpdateDCSProductStatusCriteria : NonQueryProductStatusCriteria
    {
        public UpdateDCSProductStatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            ProductStatusParam[] productStatusParams) : base(custId, subAccId, userId, ip)
        {
            IsDisabled = GetIsDisabledXmlParam(productStatusParams);
        }

        public string IsDisabled { get; }

        public override string GetSettingKey() => "Acc_CustSetting_DisableNewProductStatusUpd";

        public override bool IsValid()
        {
            return base.IsValid() && !string.IsNullOrEmpty(IsDisabled);
        }

        private static string GetIsDisabledXmlParam(ProductStatusParam[] statusParams)
        {
            var max = statusParams?.Length;

            if (max > 0)
            {
                var builder = new StringBuilder();

                for (int i = 0; i < max; i++)
                {
                    builder.Append(BuildProductStatusElement(statusParams[i]));
                }

                return $"<ROOT>{builder.ToString()}</ROOT>";
            }

            return string.Empty;
        }

        private static string BuildProductStatusElement(ProductStatusParam param)
        {
            return $"<ProductStatus SportType=\"{param.SportType}\" BetType=\"{param.BetType}\" IsDisabled=\"{(param.IsDisabled ? 1 : 0)}\"/>";
        }
    }
}
