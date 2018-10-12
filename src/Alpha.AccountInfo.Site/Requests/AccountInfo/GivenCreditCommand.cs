namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;

    public class GivenCreditCommand : AccountInfoQueryBase, IRequest<Result>
    {
        private readonly IUser viewer;

        public GivenCreditCommand(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public byte SearchAccountRoleId { get; set; }

        public decimal CreditAmount { get; set; }

        public int ActorId => viewer.Id;

        public int SubAccountId => viewer.SubAccId;

        public string Language => viewer.Language;

        public string IP => viewer.IP;

        public override bool IsValid() =>
            base.IsValid() &&
            IsValidSearchAccountRoleId(SearchAccountRoleId);
    }
}