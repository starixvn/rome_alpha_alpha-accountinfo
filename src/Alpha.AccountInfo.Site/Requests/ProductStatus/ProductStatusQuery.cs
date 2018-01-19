namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Enumerations;
    using MediatR;
    using ViewModels.ProductStatus;

    public class ProductStatusQuery : IAsyncRequest<ProductStatusViewModel>
    {
        public int ProductId { get; set; }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustRoleId { get; set; }

        public bool IsValid()
        {
            var custRole = Enumeration.FromValue<UserLevel>(CustRoleId);

            return ProductId > 0 && CustId > 0 && UserLevel.Member <= custRole && custRole <= UserLevel.Master;
        }
    }
}