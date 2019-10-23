namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Collections.Generic;

    public class ProfilePictureLibaryQuery : BaseRequest, IRequest<IEnumerable<string>>
    {
        private readonly IUser viewer ;

        public ProfilePictureLibaryQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public override bool IsValid() => true;
    }
}