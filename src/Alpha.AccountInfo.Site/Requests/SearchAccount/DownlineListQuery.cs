namespace Alpha.AccountInfo.Site.Requests.SearchAccount
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.ViewModels.SearchAccount;
    using MediatR;

    public class DownlineListQuery : SearchAccountQueryBase, IRequest<DownlineListViewModel>
    {
        public DownlineListQuery(IUser viewer) : base(viewer)
        {
        }
    }
}