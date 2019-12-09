namespace Alpha.AccountInfo.ProfilePicture.Params.SPs.Criteria
{
    using Fanex.Data.Repository;

    public class SelectProfilePictureLibraryCriteria : CriteriaBase
    {
        public SelectProfilePictureLibraryCriteria(int systemMode)
        {
            Mode = systemMode;
        }

        public override string GetSettingKey() => "Age_CustomerProfilePictureLibrarySel";

        public int Mode { get; }

        public override bool IsValid() => true;
    }
}
