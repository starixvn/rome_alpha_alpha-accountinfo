namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class UnsuspendMultipleRequest : BaseRequest, IRequest<IEnumerable<UnsuspendMultipleByUserModel>>
    {
        public UnsuspendMultipleRequest(IUser viewer) : base(viewer)
        {

        }

        public override bool IsValid()
        {
            return true;
        }
    }
}