namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using Params.ProductStatus;

    public static class ProductStatusQueryExtensions
    {
        public static ProductStatusAuthorizationParam ToProductStatusAuthorizationParam(
            this ProductStatusQuery query)
        {
            return new ProductStatusAuthorizationParam(
                query.ProductId,
                query.CustId,
                query.CustRole,
                query.ViewerId,
                query.ViewerLevel);
        }
    }
}