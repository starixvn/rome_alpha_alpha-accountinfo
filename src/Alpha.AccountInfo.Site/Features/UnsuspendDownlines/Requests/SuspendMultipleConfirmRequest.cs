namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class SuspendMultipleConfirmRequest : SearchAccountQueryBase, IRequest<SuspendMultipleConfirmViewModel>
    {
        private readonly IUser viewer;

        public SuspendMultipleConfirmRequest (IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string UserNames { get; set; }

        public string Ids { get; set; }

        public byte RoleId { get; set; }
        public string Language => viewer.Language;
        public override bool IsValid() => !string.IsNullOrEmpty(UserNames) && !string.IsNullOrEmpty(Ids);
    }
}