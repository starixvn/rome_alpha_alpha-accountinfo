namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class ProfilePictureQuery : BaseRequest, IRequest<ProfielPictureViewModel>
    {
        public ProfilePictureQuery(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
            ActorRoleId = viewer.LevelId;
            Language = viewer.Language;
            AllowUpdate = viewer.CanUpdateAccount && !viewer.IsSubAcc && !viewer.IsInternal;
            IsOfferProtectedMode = viewer.IsOfferProtectedMode;
        }

        public int ActorId { get;}

        public byte ActorRoleId { get; private set; }

        public string Language { get; private set; }

        public bool AllowUpdate { get; private set; }

        public bool IsOfferProtectedMode { get; }

        public override bool IsValid() => ActorId > 0;
    }
}