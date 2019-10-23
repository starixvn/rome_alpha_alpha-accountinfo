namespace Alpha.AccountInfo.Features.SearchAccount.Params
{
    public class UpdateActorProfilePictureParam 
    {
        public UpdateActorProfilePictureParam(
            int actorId,
            int imageId,
            byte[] image,
            bool isDefault,
            string ip)
        {
            ActorId = actorId;
            ImageId = imageId;
            Image = image;
            IsDefault = isDefault;
            IP = ip;
        }
        public int ActorId { get; }

        public int ImageId { get;}

        public byte[] Image { get; }

        public bool IsDefault { get; }

        public string IP { get; }
    }
}