namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;

    public class UpdateAllBalanceTransferCommand : SearchAccountQueryBase, IRequest<Result>
    {
        private readonly IUser viewer;

        public UpdateAllBalanceTransferCommand(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public bool StatusValue { get; set; }

        public int ActorId => viewer.Id;

        public string Language => viewer.LangKey;

        public string IP => viewer.IP;

        public string Currency => viewer.Currency;

        public override bool IsValid()
        {
            var isValid = base.IsValid() && CanUpdateAccount && !IsInternal;
            return isValid;
        }
    }
}