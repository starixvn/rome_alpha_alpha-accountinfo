namespace Alpha.AccountInfo.Params.ProductStatus
{
    using Models;

    public class ProductStatusAuthorizationParam
    {
        public ProductStatusAuthorizationParam(
            int productId,
            int custId,
            UserLevel custRole,
            int viewerId,
            UserLevel viewerLevel)
        {
            ProductId = productId;
            CustId = custId;
            CustRole = custRole;
            ViewerId = viewerId;
            ViewerLevel = viewerLevel;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public UserLevel CustRole { get; }

        public int ViewerId { get; }

        public UserLevel ViewerLevel { get; }
    }
}
