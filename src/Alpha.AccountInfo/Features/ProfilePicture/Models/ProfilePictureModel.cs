namespace Alpha.AccountInfo.Features.ProfilePicture.Models
{
    public class ProfilePictureModel
    {
        public ProfilePictureModel()
        {
        }

        public ProfilePictureModel(int imageId, byte[] image)
        {
            ImageId = imageId;
            Image = image;
        }

        public int ImageId { get; private set; }

        public byte[] Image{ get; private set; }
    }
}
