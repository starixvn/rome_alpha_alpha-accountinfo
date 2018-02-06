namespace Alpha.AccountInfo.Params.ProductStatus
{
    using Models;

    public class ProductStatusCommandParam
    {
        public ProductStatusCommandParam(
            int productId,
            int custId,
            UserLevel custRole,
            int uplineId,
            UserLevel uplineLevel,
            int userId,
            int subAccId,
            string langKey,
            string ip,
            ProductStatusParam[] productStatusParams)
        {
            ProductId = productId;
            CustId = custId;
            CustRole = custRole;
            UplineId = uplineId;
            UplineLevel = uplineLevel;
            UserId = userId;
            SubAccId = subAccId;
            LangKey = langKey;
            IP = ip;
            ProductStatusParams = productStatusParams;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public UserLevel CustRole { get; }

        public int UplineId { get; }

        public UserLevel UplineLevel { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string LangKey { get; }

        public string IP { get; }

        public ProductStatusParam[] ProductStatusParams { get; }
    }
}
