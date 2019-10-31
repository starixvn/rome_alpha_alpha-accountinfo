namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Collections.Generic;

    public class ProfilePictureQuery : BaseRequest, IRequest<IEnumerable<DefaultProfielPictureViewModel>>
    {
        public ProfilePictureQuery(IUser viewer) : base(viewer)
        {
            this.ActorId = viewer.Id;
        }

        public int ActorId { get;}

        public override bool IsValid() => ActorId > 0;
    }
}