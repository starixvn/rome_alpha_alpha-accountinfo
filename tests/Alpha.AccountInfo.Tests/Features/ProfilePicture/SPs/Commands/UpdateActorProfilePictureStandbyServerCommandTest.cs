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
            var ip = "192.168.1.1";
            var command = new UpdateActorProfilePictureStandbyServerCommand(1, 1, null, 1, ip);
            var expectedSettingKey = "Age_CustomerUpdProfilePicture_Standby";

            var actualSettingKey = command.GetSettingKey();

            Assert.Equal(1, command.ActorId);
            Assert.False(command.IsDefault);
            Assert.False(command.IsMainServer);
            Assert.Equal(1, command.Mode);
            Assert.Equal(command.IP, ip);
            Assert.Equal(1, command.ImageId);
            Assert.Null(command.Image);
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}