namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using MediatR;

    public class UnsuspendSingleCommand : SearchAccountQueryBase, IRequest<Result>
    {
        private readonly IUser viewer;

        public UnsuspendSingleCommand(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string ActorName => viewer.Name;

        public int ActorId => viewer.Id;

        public int SubAccountId => viewer.SubAccId;

        public string Language => viewer.LangKey;

        public string IP => viewer.IP;

        public override bool IsValid() => base.IsValid() && CanUpdateAccount && !IsInternal;
    }
}