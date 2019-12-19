namespace Alpha.AccountInfo.ProfilePicture.Params.SPs.Commands
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class UpdateActorProfilePictureCommand : NonQueryCommand
    {
        public UpdateActorProfilePictureCommand(
            int actorId,
            int imageId,
            byte[] image,
            int mode,
            string ip)
        {
            
            if(imageId < 0)
            {
                ImageId = null;
                Image = image;
            }
            else
            {
                ImageId = imageId;
                Image = null;
            }

            ActorId = actorId;
            Mode = mode;
            IP = ip;
            IsDefault = false;
            IsMainServer = true;
        }

        [SpParam(SqlParamName = "CustId")]
        public int ActorId { get; }

        public int? ImageId { get;}

        public byte[] Image { get; }

        public bool IsDefault { get; }

        public bool IsMainServer { get; }

        public int Mode { get; }

        public string IP { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "Age_CustomerUpdProfilePicture";

        public override bool IsValid() => ActorId > 0;
    }
}