namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using MediatR;

    public class ProductStatusCommand : ProductStatusQuery, IRequest<Result>
    {
        public ProductStatusCommand(IUser viewer)
            : base(viewer)
        {
            UplineUserId = viewer.UserId;
            UplineSubAccId = viewer.SubAccId;
            LangKey = viewer.LangKey;
            IP = viewer.IP;
        }

        public string ProductStatus { get; set; }

        public int UplineUserId { get; }

        public int UplineSubAccId { get; }

        public string LangKey { get; }

        public string IP { get; }

        public override bool IsValid()
            => base.IsValid() && !string.IsNullOrWhiteSpace(ProductStatus);
    }
}