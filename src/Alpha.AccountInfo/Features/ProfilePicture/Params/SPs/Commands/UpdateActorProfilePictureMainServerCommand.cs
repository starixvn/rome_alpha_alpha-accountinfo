namespace Alpha.AccountInfo.ProfilePicture.Params.SPs.Commands
{
    public class UpdateActorProfilePictureMainServerCommand : UpdateActorProfilePictureCommandBase
    {
        public UpdateActorProfilePictureMainServerCommand(
            int actorId,
            int imageId,
            byte[] image,
            int mode,
            string ip) : 
            base(actorId, imageId, image, mode, ip, true)
        {
        }

        public override string GetSettingKey() => "Age_CustomerUpdProfilePicture";
    }
}