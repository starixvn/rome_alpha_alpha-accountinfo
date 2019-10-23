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
            this.ActorId = viewer.Id;
        }

        public int ActorId { get;}

        public override bool IsValid() => true;
    }
}