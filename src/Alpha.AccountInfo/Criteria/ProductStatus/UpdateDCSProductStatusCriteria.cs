namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
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

        public override string ParseErrMsg()
        {
            var xmlRoot = XElement.Parse(ErrMsg);
            var attributes = xmlRoot.Attributes("Msg");

            return string.Join("<br/>", attributes.Select(a => a.Value));
        }

        private static string GetIsDisabledXmlParam(ProductStatusParam[] statusParams)
        {
            var builder = new StringBuilder("<ROOT>");

            for (int i = 0; i < statusParams?.Length; i++)
            {
                builder.Append(BuildProductStatusElement(statusParams[i]));
            }

            builder.Append("</ROOT>");

            return builder.ToString();
        }

        private static string BuildProductStatusElement(ProductStatusParam param)
        {
            return $"<ProductStatus SportType=\"{param.SportType}\" BetType=\"{param.BetType}\" IsDisabled=\"{(param.IsDisabled ? 1 : 0)}\"/>";
        }
    }
}
