namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using MediatR;
    using System.Collections.Generic;

    public class OnlineListQuery : BaseRequest, IRequest<IEnumerable<OnlineListModel>>
    {
        public OnlineListQuery(IUser viewer)
            : base(viewer)
        {

        }
        public int CustId { get; set; }

        public override bool IsValid() => CustId > 0;
    }
}