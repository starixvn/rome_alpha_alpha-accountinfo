namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class UnsuspendRequest : BaseRequest, IRequest<UnsuspendModel>
    {
        public UnsuspendRequest(IUser viewer) : base(viewer)
        {

        }

        public override bool IsValid()
        {
            return true;
        }
    }
}