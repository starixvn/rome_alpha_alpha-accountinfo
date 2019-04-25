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

        public bool IsAffected { get; set; }

        public string CustIds { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public byte DownlineRoleId { get; set; }

        public int PageSize { get; set; } = 50;

        public int PageIndex { get; set; }

        public override bool IsValid() => base.IsValid() && IsValidSearchAccountRoleId(DownlineRoleId);
    }
}