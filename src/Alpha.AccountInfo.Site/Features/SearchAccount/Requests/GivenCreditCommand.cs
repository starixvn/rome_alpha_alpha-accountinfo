namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;

    public class GivenCreditCommand : SearchAccountQueryBase, IRequest<Result>
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

        public string LangKey => viewer.LangKey;

        public string IP => viewer.IP;

        public override bool IsValid() => base.IsValid() && CanUpdateAccount && IsValidSearchAccountRoleId(SearchAccountRoleId);
    }
}