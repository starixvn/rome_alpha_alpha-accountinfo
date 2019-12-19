namespace Alpha.AccountInfo.ProfilePicture.Params
{
    public class UpdateActorProfilePictureParam
    {
        public UpdateActorProfilePictureParam(
            int actorId,
            int imageId,
            byte[] image,
            int mode,
            string ip)
        {
            ActorId = actorId;
            ImageId = imageId;
            Image = image;
            Mode = mode;
            IP = ip;
        }
        public int ActorId { get; }

        public int ImageId { get; }

        public byte[] Image { get; }

        public int Mode { get; }

        public string IP { get; }
    }
}