namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class ProductQuery : BaseRequest, IRequest<ProductViewModel>
    {
        public ProductQuery(IUser viewer)
            : base(viewer)
        {
         
        }

        public int CustId { get; set; }

        public override bool IsValid() => CustId > 0;
    }
}