namespace Alpha.AccountInfo.Site.Requests.SearchAccount
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;

    public class OnlineListQuery : SearchAccountQueryBase, IRequest<IEnumerable<OnlineUserModel>>
    {
        public OnlineListQuery(IUser viewer) : base(viewer)
        {
        }
    }
}