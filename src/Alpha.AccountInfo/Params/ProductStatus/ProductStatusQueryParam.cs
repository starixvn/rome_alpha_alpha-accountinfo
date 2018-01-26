namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class ProductStatusQueryParam
    {
        public ProductStatusQueryParam(
            int productId,
            int custId,
            byte custRoleId)
        {
            ProductId = productId;
            CustId = custId;
            CustRoleId = custRoleId;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public byte CustRoleId { get; }
    }
}
