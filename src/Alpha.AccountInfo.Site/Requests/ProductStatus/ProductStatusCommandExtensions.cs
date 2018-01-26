namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using Newtonsoft.Json;
    using Params.ProductStatus;

    public static class ProductStatusCommandExtensions
    {
        public static ProductStatusCommandParam ToUpdateProductStatusParam(
            this ProductStatusCommand command)
        {
            var productStatusParams = JsonConvert.DeserializeObject<ProductStatusParam[]>(command.ProductStatus);

            return new ProductStatusCommandParam(
                command.ProductId,
                command.CustId,
                command.ViewerId,
                command.ViewerUserId,
                command.ViewerSubAccId,
                command.LangKey,
                command.IP,
                productStatusParams);
        }
    }
}