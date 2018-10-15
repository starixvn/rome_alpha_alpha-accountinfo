namespace Alpha.AccountInfo.Site.Requests.SearchAccount
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;

    public class CustomerNonProductStatusCommand : SearchAccountQueryBase, IRequest<Result>
    {
        private readonly IUser viewer;

        public CustomerNonProductStatusCommand(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string ActorName => viewer.Name;

        public bool SyncedCasino3rd { get; set; }

        public bool StatusValue { get; set; }

        public string StatusName { get; set; }

        public int ActorId => viewer.Id;

        public int SubAccountId => viewer.SubAccId;

        public string Language => viewer.Language;

        public string IP => viewer.IP;

        public override bool IsValid() => base.IsValid() && IsValidName(StatusName);
    }
}