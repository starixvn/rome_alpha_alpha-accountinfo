namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Web;

    public class CreateProfilePictureQuery : BaseRequest, IRequest<string>
    {
        public CreateProfilePictureQuery(IUser viewer) : base(viewer)
        {
            this.ActorId = viewer.Id;
        }

        public string OriginalImage { get; set; }

        public int ActorId { get;}

        public override bool IsValid() => ActorId > 0;
    }
}