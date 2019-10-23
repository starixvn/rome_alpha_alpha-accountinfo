namespace Alpha.AccountInfo.Features.ProfilePicture.Models
{
    public class ActorProfilePicture
    {
        public ActorProfilePicture()
        {
        }

        public ActorProfilePicture(int custId, byte[] image)
        {
            CustId = custId;
            Image = image;
        }

        public int CustId { get; private set; }

        public byte[] Image{ get; private set; }
    }
}
