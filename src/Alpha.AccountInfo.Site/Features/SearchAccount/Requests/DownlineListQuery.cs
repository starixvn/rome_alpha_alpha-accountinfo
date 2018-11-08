namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class DownlineListQuery : SearchAccountQueryBase, IRequest<DownlineListViewModel>
    {
        public DownlineListQuery(IUser viewer) : base(viewer)
        {
        }

        public int PageIndex { get; set; }
    }
}