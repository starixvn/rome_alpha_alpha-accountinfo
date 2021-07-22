namespace Alpha.AccountInfo.Site.Features.OpenDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;
    using SearchAccount.ViewModels;
    using Shared;

    public class OpenMultipleRequest : BaseRequest, IRequest<OpenMultipleViewModel>
    {
        private readonly IUser viewer;

        public OpenMultipleRequest(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string CustIds { get; set; }

        public byte RoleId { get; set; }

        public string Language => viewer.Language;

        public override bool IsValid() => !string.IsNullOrEmpty(CustIds) && RoleId < viewer.LevelId;
    }
}