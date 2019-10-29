namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using MediatR;

    public class UpdateActorProfilePictureQuery : BaseRequest, IRequest<Result>
    {
        public UpdateActorProfilePictureQuery(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
            IP = viewer.IP;
            IsDefault = false;
        }

        public int ActorId { get;}

        public int ImageId { get; set; }

        public string Image { get; set; }

        public byte[] ImageBinary 
            => string.IsNullOrEmpty(Image) ? null : ImageConverter.ToLimitedImageSize(Image);

        public bool IsDefault { get; }

        public string IP { get; }

        public override bool IsValid() => ActorId > 0 && (ImageId > 0 || (ImageId < 0 && ImageBinary != null));
    }
}