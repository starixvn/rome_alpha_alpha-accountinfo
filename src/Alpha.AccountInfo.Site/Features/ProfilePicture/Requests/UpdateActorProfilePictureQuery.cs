namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using MediatR;

    public class UpdateActorProfilePictureQuery : BaseRequest, IRequest<ProfilePictureResult>
    {
        public UpdateActorProfilePictureQuery(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
            IP = viewer.IP;
            AllowUpdate = viewer.CanUpdateAccount && !viewer.IsSubAcc;
        }

        public int ActorId { get;}

        public int ImageId { get; set; }

        public string Image { get; set; }

        public byte[] ImageBinary 
            => string.IsNullOrEmpty(Image) ? null : ImageConverter.ToLimitedImageSize(Image);

        public string IP { get; }

        public bool AllowUpdate { get; private set; }

        public override bool IsValid()
        {
            var IsValidActorPermission = AllowUpdate && ActorId > 0;
            var IsValidImage = ImageId > 0 || (ImageId < 0 && ImageBinary != null);
            return IsValidActorPermission && IsValidImage;
        }
    }
}