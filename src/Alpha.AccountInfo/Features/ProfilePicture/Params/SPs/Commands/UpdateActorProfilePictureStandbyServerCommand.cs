namespace Alpha.AccountInfo.ProfilePicture.Params.SPs.Commands
{
    public class UpdateActorProfilePictureStandbyServerCommand : UpdateActorProfilePictureCommandBase
    {
        public UpdateActorProfilePictureStandbyServerCommand(
            int actorId,
            int imageId,
            byte[] image,
            int mode,
            string ip) : 
            base(actorId, imageId, image, mode, ip, false)
        {
        }

        public override string GetSettingKey() => "Age_CustomerUpdProfilePicture_Standby";
    }
}