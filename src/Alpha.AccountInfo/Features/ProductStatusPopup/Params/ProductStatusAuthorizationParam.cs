namespace Alpha.AccountInfo.Params.ProductStatus
{
    using Alpha.AccountInfo.Features.Shared.Models;

    public class ProductStatusAuthorizationParam
    {
        public ProductStatusAuthorizationParam(
            bool isGroup,
            int productId,
            int custId,
            UserLevel custRole,
            int uplineId,
            UserLevel uplineLevel,
            int[] offeredProductGroupIds)
        {
            IsGroup = isGroup;
            ProductId = productId;
            CustId = custId;
            CustRole = custRole;
            UplineId = uplineId;
            UplineLevel = uplineLevel;
            OfferedProductGroupIds = offeredProductGroupIds;
        }

        public bool IsGroup { get; }

        public int ProductId { get; }

        public int CustId { get; }

        public UserLevel CustRole { get; }

        public int UplineId { get; }

        public UserLevel UplineLevel { get; }

        public int[] OfferedProductGroupIds { get; }
    }
}