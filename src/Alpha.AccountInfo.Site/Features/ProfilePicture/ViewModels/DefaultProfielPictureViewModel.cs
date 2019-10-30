namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    public class DefaultProfielPictureViewModel
    {
        public DefaultProfielPictureViewModel(int imageId, string image)
        {
            ImageId = imageId;
            Image = image;
        }

        public int ImageId { get; private set; }

        public string Image { get; private set; }
    }
}