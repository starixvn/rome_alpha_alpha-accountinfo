namespace Alpha.AccountInfo.Features.ProfilePicture.Models
{
    public class ActorProfilePictureModel
    {
        public ActorProfilePictureModel()
        {
        }

        public ActorProfilePictureModel(int custId, byte[] image)
        {
            CustId = custId;
            Image = image;
        }

        public int CustId { get; private set; }

        public byte[] Image{ get; private set; }
    }
}
