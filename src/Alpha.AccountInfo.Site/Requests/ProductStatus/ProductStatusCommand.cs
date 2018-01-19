namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using MediatR;

    public class ProductStatusCommand : ProductStatusQuery, IRequest<Result>
    {
        public ProductStatusCommand(IUser viewer)
            : base(viewer)
        {
            ViewerUserId = viewer.UserId;
            ViewerSubAccId = viewer.SubAccId;
            LangKey = viewer.LangKey;
            IP = viewer.IP;
        }

        public string ProductStatus { get; set; }

        public int ViewerUserId { get; }

        public int ViewerSubAccId { get; }

        public string LangKey { get; }

        public string IP { get; }

        public override bool IsValid()
        {
            return base.IsValid() && !string.IsNullOrWhiteSpace(ProductStatus);
        }
    }
}