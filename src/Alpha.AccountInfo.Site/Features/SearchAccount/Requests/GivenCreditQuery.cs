namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class GivenCreditQuery : SearchAccountQueryBase, IRequest<GivenCreditViewModel>
    {
        public GivenCreditQuery(IUser viewer) : base(viewer)
        {
            Language = viewer.Language;
        }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public decimal CurrentGivenCredit { get; set; }

        public string Language { get; }

        public override bool IsValid() =>
            base.IsValid() &&
            IsValidSearchAccountRoleId(SearchAccountRoleId) &&
            !string.IsNullOrEmpty(SearchAccountName);
    }
}