namespace Alpha.AccountInfo.Site.Features.OpenDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;
    using SearchAccount.Requests;

    public class OpenSingleCommand : SearchAccountQueryBase, IRequest<Result>
    {
        private readonly IUser viewer;

        public OpenSingleCommand(IUser viewer) : base(viewer)
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