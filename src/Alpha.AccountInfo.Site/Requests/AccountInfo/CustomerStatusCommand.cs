namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using MediatR;

    public class CustomerStatusCommand : AccountInfoQueryBase, IRequest<Result>
    {
        private readonly IUser viewer;

        public CustomerStatusCommand(IUser viewer) : base(viewer)
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