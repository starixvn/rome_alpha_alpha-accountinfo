namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.ProfilePicture.Models;
    using MediatR;
    using System.Collections.Generic;

    public class ProfilePictureLibraryQuery : BaseRequest, IRequest<IEnumerable<ProfilePictureModel>>
    {
        public ProfilePictureLibraryQuery(IUser viewer) : base(viewer)
        {
        }

        public override bool IsValid() => true;
    }
}