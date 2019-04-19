namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class UnsuspendRequest : SearchAccountQueryBase, IRequest<UnsuspendInfoViewModel>
    {
        public UnsuspendRequest(IUser viewer) : base(viewer)
        {
        }
    }
}