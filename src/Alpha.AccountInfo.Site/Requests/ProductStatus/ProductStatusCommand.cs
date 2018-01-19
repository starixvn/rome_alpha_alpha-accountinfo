namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using AccountInfo.Models.ProductStatus;
    using Enumerations;
    using MediatR;

    public class ProductStatusCommand : IAsyncRequest<Result>
    {
        public int ProductId { get; set; }

        public int CustId { get; set; }

        public byte CustRoleId { get; set; }

        public ProductStatusModel[] ProductStatus { get; set; }

        public bool IsValid()
        {
            var custRole = Enumeration.FromValue<UserLevel>(CustRoleId);
            var isValidRole = UserLevel.Member <= custRole && custRole <= UserLevel.Master;

            var isValidProductStatus = ProductStatus != null && ProductStatus.Length > 0;

            return isValidRole && isValidProductStatus && ProductId > 0 && CustId > 0;
        }
    }
}