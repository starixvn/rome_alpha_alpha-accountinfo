namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using Alpha.ProfilePicture.Models;
    using MediatR;

    public class UpdateActorProfilePictureQuery : BaseRequest, IRequest<ProfilePictureResult>
    {
        public UpdateActorProfilePictureQuery(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
            IP = viewer.IP;
        }

        public int ActorId { get;}

        public int ImageId { get; set; }

        public string Image { get; set; }

        public byte[] ImageBinary 
            => string.IsNullOrEmpty(Image) ? null : ImageConverter.ToLimitedImageSize(Image);

        public string IP { get; }

        public override bool IsValid() => ActorId > 0 && (ImageId > 0 || (ImageId < 0 && ImageBinary != null));
    }
}