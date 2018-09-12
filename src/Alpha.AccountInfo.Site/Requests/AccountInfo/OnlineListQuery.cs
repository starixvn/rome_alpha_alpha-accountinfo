namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using MediatR;
    using System.Collections.Generic;

    public class OnlineListQuery : BaseRequest, IRequest<IEnumerable<OnlineUserModel>>
    {
        public OnlineListQuery(IUser viewer)
            : base(viewer)
        {

        }
        public int SearchAccountId { get; set; }

        public override bool IsValid() => SearchAccountId > 0;
    }
}