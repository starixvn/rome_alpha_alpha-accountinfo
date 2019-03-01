namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class GivenCreditQuery : SearchAccountQueryBase, IRequest<GivenCreditViewModel>
    {
        private IUser viewer;

        public GivenCreditQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public decimal CurrentGivenCredit { get; set; }

        public decimal GivenCreditMin { get; set; }

        public decimal GivenCreditMax { get; set; }

        public string Language => viewer.Language;

        public override bool IsValid() =>
            base.IsValid() &&
            IsValidSearchAccountRoleId(SearchAccountRoleId) &&
            !string.IsNullOrEmpty(SearchAccountName) &&
            viewer.CanUpdateAccount;
    }
}