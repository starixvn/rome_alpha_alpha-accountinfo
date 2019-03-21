namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared;
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

        public byte SearchAccountRoleId { get; set; }

        public bool SyncedCasino3rd { get; set; }

        public bool StatusValue { get; set; }

        public string StatusName { get; set; }

        public int ActorId => viewer.Id;

        public int SubAccountId => viewer.SubAccId;

        public string Language => viewer.LangKey;

        public string IP => viewer.IP;

        public override bool IsValid()
        {
            var isValid = base.IsValid() && IsValidName(StatusName) && CanUpdateAccount;
            return !StatusName.Equals(StatusNames.DoubleComm) ? isValid : isValid && IsValidDoubleComm;
        }

        internal bool IsValidDoubleComm => viewer.LevelId == SearchAccountRoleId + 1;
    }
}