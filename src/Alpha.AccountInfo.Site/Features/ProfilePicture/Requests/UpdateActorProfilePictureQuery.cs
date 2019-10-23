namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
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

        public byte[] Image { get; set; }

        public bool IsDefault { get; }

        public string IP { get; }

        public override bool IsValid() => ActorId > 0 && Image != null;
    }
}