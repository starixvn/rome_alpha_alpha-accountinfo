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
            IP = viewer.IP;
            ServerId = viewer.ServerId;
        }

        public string ActorName => viewer.Name;

        public byte SearchAccountRoleId { get; set; }

        public bool StatusValue { get; set; }

        public string StatusName { get; set; }

        public int AgentId { get; set; }

        public int ActorId => viewer.Id;

        public int SubAccountId => viewer.SubAccId;

        public string Language => viewer.LangKey;

        public string IP { get; }

        public int ServerId { get; }

        public override bool IsValid()
        {
            var isValid = base.IsValid() && IsValidName(StatusName) && CanUpdateAccount && !IsInternal;
            return !StatusName.Equals(StatusNames.DoubleComm) ? isValid : isValid && IsValidDoubleComm;
        }

        private bool IsDirectMember
            => (viewer.LevelId == UserLevel.Super.Value && AgentId == DirectMemberAgentIds.Super)
            || (viewer.LevelId == UserLevel.Master.Value && AgentId == DirectMemberAgentIds.Master);

        internal bool IsValidDoubleComm => IsDirectMember || viewer.LevelId == SearchAccountRoleId + 1;
    }
}