namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class UnsuspendViewListRequest : SearchAccountQueryBase, IRequest<UnsuspendListViewModel>
    {
        public UnsuspendViewListRequest(IUser viewer) : base(viewer)
        {

        }

        public override bool IsValid()
        {
            return true;
        }
    }
}