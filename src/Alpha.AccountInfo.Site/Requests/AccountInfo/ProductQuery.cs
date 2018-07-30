namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class ProductQuery : BaseRequest, IRequest<ProductViewModel>
    {
        private readonly IUser viewer;

        public ProductQuery(IUser viewer)
            : base(viewer)
        {
            this.viewer = viewer;
        }

        public int CustId { get; set; }

        public int SubAccountId => viewer.SubAccId;

        public override bool IsValid() => CustId > 0;
    }
}