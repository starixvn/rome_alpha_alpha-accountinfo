namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class MemberBalanceInfoQuery : BaseRequest, IRequest<IBalanceInfoViewModel>
    {
        public MemberBalanceInfoQuery(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public IUser Viewer { get; }

        public string CustName { get; set; }

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(CustName);
    }
}