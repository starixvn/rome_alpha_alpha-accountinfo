namespace Alpha.AccountInfo.Site.Features.ProtectedStatus.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProtectedStatus.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class EditMultipleRequest : BaseRequest, IRequest<EditMultipleViewModel>
    {
        public EditMultipleRequest(IUser viewer) : base(viewer)
        {
            ViewerId = viewer.Id;
            ViewerLevelId = viewer.LevelId;
            Language = viewer.Language;
        }

        public int ViewerId { get; }

        public byte ViewerLevelId { get; }

        public string Language { get; }

        public string CustIds { get; set; }

        public override bool IsValid()
            => ViewerId > 0
            && (ViewerLevelId == UserLevel.Super.Value || ViewerLevelId == UserLevel.Master.Value)
            && !string.IsNullOrEmpty(CustIds);
    }
}