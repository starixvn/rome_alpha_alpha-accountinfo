namespace Alpha.AccountInfo.Features.ProfilePicture.Models
{
    public class DefaultProfilePicture
    {
        public DefaultProfilePicture()
        {
        }

        public DefaultProfilePicture(int imageId, byte[] image)
        {
            ImageId = imageId;
            Image = image;
        }

        public int ImageId { get; private set; }

        public byte[] Image{ get; private set; }
    }
}
