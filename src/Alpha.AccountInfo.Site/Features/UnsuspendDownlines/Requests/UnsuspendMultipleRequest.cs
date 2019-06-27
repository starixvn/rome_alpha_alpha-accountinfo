namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class UnsuspendMultipleRequest : BaseRequest, IRequest<UnsuspendMultipleViewModel>
    {
        private readonly IUser viewer;

        public UnsuspendMultipleRequest(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string CustIds { get; set; }

        public byte RoleId { get; set; }

        public string Language => viewer.Language;

        public override bool IsValid() => !string.IsNullOrEmpty(CustIds) && RoleId < viewer.LevelId;
    }
}