namespace Alpha.AccountInfo.Tests.Features.ProfilePicture.SpParams.Criteria
{
    using Alpha.AccountInfo.ProfilePicture.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class SelectProfilePictureLibraryCriteriaTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_Should_ReturnsCorrectValue(int systemMode, bool expectedValue)
        {
            var criteria = new SelectProfilePictureLibraryCriteria(systemMode);

            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectProfilePictureLibraryCriteria(1);
            var expectedSettingKey = "Age_CustomerProfilePictureLibrarySel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}