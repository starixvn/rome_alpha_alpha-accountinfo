namespace Alpha.AccountInfo.Tests.Features.ProfilePicture.SpParams.Commands
{
    using Alpha.AccountInfo.ProfilePicture.Params.SPs.Commands;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class UpdateActorProfilePictureCommandTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_Should_ReturnsCorrectValue(int actorId, bool expectedValue)
        {
            var criteria = new UpdateActorProfilePictureCommand(actorId, 1, null, 1, string.Empty);

            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new UpdateActorProfilePictureCommand(1, 1, null, 1, string.Empty);
            var expectedSettingKey = "Age_CustomerUpdProfilePicture";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void ImageId_Less_Than_Zero_Should_Pass_ImageID_Null_To_SP()
        {
            var criteria = new UpdateActorProfilePictureCommand(1, -100, new byte[5], 1, string.Empty);

            Assert.Null(criteria.ImageId);
        }

        [Fact]
        public void ImageId_Greater_Than_Zero_Should_Pass_Image_Null_To_SP()
        {
            var criteria = new UpdateActorProfilePictureCommand(1, 100, new byte[5], 1, string.Empty);

            Assert.Null(criteria.Image);
        }
    }
}