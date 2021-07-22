namespace Alpha.AccountInfo.Site.Features.OpenDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;
    using SearchAccount.Requests;
    using SearchAccount.ViewModels;

    public class OpenViewListRequest : SearchAccountQueryBase, IRequest<OpenListViewModel>
    {
        public OpenViewListRequest(IUser viewer) : base(viewer)
        {
        }

        public string SearchAccountName { get; set; }

        public bool IsAffected { get; set; }

        public byte DownlineRoleId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override bool IsValid() => base.IsValid() && IsValidSearchAccountRoleId(DownlineRoleId);
    }
}