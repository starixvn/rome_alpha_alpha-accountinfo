namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Data;
    using Params.ProductStatus;

    public class UpdateDCSProductStatusCriteria : NonQueryProductStatusCriteria
    {
        public UpdateDCSProductStatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            IEnumerable<BetTypeStatusParam> productStatusParams) : base(custId, subAccId, userId, ip)
        {
            ProductStatusXml = ParseProductStatusToXml(productStatusParams);
        }

        [SpParam(SqlParamName = "IsDisabled")]
        public string ProductStatusXml { get; }

        public override string GetSettingKey() => "Acc_CustSetting_DisableNewProductStatusUpd";

        public override bool IsValid() => base.IsValid() && !string.IsNullOrEmpty(ProductStatusXml);

        private static string ParseProductStatusToXml(IEnumerable<BetTypeStatusParam> statusParams)
        {
            if (statusParams != null && statusParams.Any())
            {
                var builder = new StringBuilder();

                foreach (var item in statusParams)
                {
                    builder.Append(BuildProductStatusElement(item));
                }

                return $"<ROOT>{builder.ToString()}</ROOT>";
            }

            return string.Empty;
        }

        private static string BuildProductStatusElement(BetTypeStatusParam param)
            => $"<ProductStatus SportType=\"{param.SportType}\" BetType=\"{param.BetType}\" IsDisabled=\"{Convert.ToInt32(param.IsDisabled)}\"/>";
    }
}
