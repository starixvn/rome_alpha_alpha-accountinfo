namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class ProfilePictureQuery : BaseRequest, IRequest<string>
    {
        private readonly IUser viewer ;

        public ProfilePictureQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public override bool IsValid() => true;
    }
}