namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using Newtonsoft.Json;
    using Params.ProductStatus;

    public static class ProductStatusCommandExtensions
    {
        public static UpdateProductStatusParam ToUpdateProductStatusParam(
            this ProductStatusCommand command)
        {
            var productStatusParams = JsonConvert.DeserializeObject<ProductStatusParam[]>(command.ProductStatus);

            return new UpdateProductStatusParam(
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