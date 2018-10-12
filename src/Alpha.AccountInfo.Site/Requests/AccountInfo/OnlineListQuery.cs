namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;

    public class OnlineListQuery : AccountInfoQueryBase, IRequest<IEnumerable<OnlineUserModel>>
    {
        public OnlineListQuery(IUser viewer) : base(viewer)
        {
        }
    }
}