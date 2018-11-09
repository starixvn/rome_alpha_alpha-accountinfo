namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class TotalDownlineSummyQuery : SearchAccountQueryBase, IRequest<DownlineListViewModel>
    {
        public TotalDownlineSummyQuery(IUser viewer) : base(viewer)
        {
        }

        public byte RoleId { get; set; }
    }
}