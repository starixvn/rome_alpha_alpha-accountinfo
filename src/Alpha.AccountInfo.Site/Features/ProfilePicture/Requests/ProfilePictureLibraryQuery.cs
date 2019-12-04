namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class ProfilePictureLibraryQuery : BaseRequest, IRequest<ProfielPictureLibraryViewModel>
    {
        public ProfilePictureLibraryQuery(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
        }

        public int CurrentPage { get; set; }

        public int ActorId { get; }

        public override bool IsValid() => ActorId > 0;
    }
}