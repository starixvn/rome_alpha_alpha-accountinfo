namespace Alpha.AccountInfo.Tests.Features.ProfilePicture.SpParams.Commands
{
    using Alpha.AccountInfo.ProfilePicture.Params.SPs.Commands;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class UpdateActorProfilePictureCommandBaseTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_Should_ReturnsCorrectValue(int actorId, bool expectedValue)
        {
            var criteria = new UpdateActorProfilePictureCommandBase(actorId, 1, null, 1, string.Empty, true);

            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Fact]
        public void ImageId_Less_Than_Zero_Should_Pass_ImageID_Null_To_SP()
        {
            var criteria = new UpdateActorProfilePictureCommandBase(1, -100, new byte[5], 1, string.Empty, true);

            Assert.Null(criteria.ImageId);
        }

        [Fact]
        public void ImageId_Greater_Than_Zero_Should_Pass_Image_Null_To_SP()
        {
            var criteria = new UpdateActorProfilePictureCommandBase(1, 100, new byte[5], 1, string.Empty, true);

            Assert.Null(criteria.Image);
        }
    }
}