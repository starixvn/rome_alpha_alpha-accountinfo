using System;

namespace Alpha.AccountInfo.Features.ProfilePicture.Models
{
    public class ProfilePictureModel
    {
        public ProfilePictureModel()
        {
        }

        public ProfilePictureModel(byte[] image)
        {
            ImageId = 0;
            Image = BuildImageBase64String(image);
        }

        public ProfilePictureModel(ProfilePictureDTO profilePictureDTO)
        {
            ImageId = profilePictureDTO.ImageId;
            Image = BuildImageBase64String(profilePictureDTO.Image);
        }

        public int ImageId { get; private set; }

        public string Image { get; private set; }

        private string BuildImageBase64String(byte[] image) => $"data:image/jpeg;base64,{Convert.ToBase64String(image)}";
    }

    public class ProfilePictureDTO
    {
        public ProfilePictureDTO()
        {
        }

        public ProfilePictureDTO(int imageId, byte[] image)
        {
            ImageId = imageId;
            Image = image;
        }

        public int ImageId { get; private set; }

        public byte[] Image{ get; private set; }
    }
}
