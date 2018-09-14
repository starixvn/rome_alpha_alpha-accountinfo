namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using MediatR;
    using System.Collections.Generic;

    public class OnlineListQuery : AccountInfoQueryBase, IRequest<IEnumerable<OnlineUserModel>>
    {
        public OnlineListQuery(IUser viewer) : base(viewer)
        {
        }
    }
}