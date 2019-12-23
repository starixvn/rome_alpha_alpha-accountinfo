namespace Alpha.AccountInfo.Tests.Features.ProfilePicture.SpParams.Commands
{
    using Alpha.AccountInfo.ProfilePicture.Params.SPs.Commands;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class UpdateActorProfilePictureStandbyServerCommandTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new UpdateActorProfilePictureStandbyServerCommand(1, 1, null, 1, string.Empty);
            var expectedSettingKey = "Age_CustomerUpdProfilePicture_Standby";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}