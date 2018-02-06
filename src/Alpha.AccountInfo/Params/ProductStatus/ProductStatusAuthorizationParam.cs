namespace Alpha.AccountInfo.Params.ProductStatus
{
    using Models;

    public class ProductStatusAuthorizationParam
    {
        public ProductStatusAuthorizationParam(
            int productId,
            int custId,
            UserLevel custRole,
            int uplineId,
            UserLevel uplineLevel)
        {
            ProductId = productId;
            CustId = custId;
            CustRole = custRole;
            UplineId = uplineId;
            UplineLevel = uplineLevel;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public UserLevel CustRole { get; }

        public int UplineId { get; }

        public UserLevel UplineLevel { get; }
    }
}
