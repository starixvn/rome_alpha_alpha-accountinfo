namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    using Fanex.Data.Repository;

    public class SelectProfilePictureLibraryCriteria : CriteriaBase
    {
        public SelectProfilePictureLibraryCriteria()
        {
        }

        public override string GetSettingKey() => "Age_CustomerProfilePictureLibrarySel";

        public override bool IsValid() => true;
    }
}
