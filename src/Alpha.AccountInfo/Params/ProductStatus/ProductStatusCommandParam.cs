namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class ProductStatusCommandParam
    {
        public ProductStatusCommandParam(
            int productId,
            int custId,
            int uplineId,
            int userId,
            int subAccId,
            string langKey,
            string ip,
            ProductStatusParam[] productStatusParams)
        {
            ProductId = productId;
            CustId = custId;
            UplineId = uplineId;
            UserId = userId;
            SubAccId = subAccId;
            LangKey = langKey;
            IP = ip;
            ProductStatusParams = productStatusParams;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public int UplineId { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string LangKey { get; }

        public string IP { get; }

        public ProductStatusParam[] ProductStatusParams { get; }
    }
}
