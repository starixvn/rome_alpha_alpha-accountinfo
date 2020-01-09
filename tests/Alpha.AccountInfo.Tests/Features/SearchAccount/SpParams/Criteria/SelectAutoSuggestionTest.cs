namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectAutoSuggestionTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectAutoSuggestionCriteria(1, "AC", false);
            var expectedSettingKey = "Age_CustomerSelUserSearch";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("UserTest", true)]
        public void IsValid_SuggestionQuery_Should_ReturnsValidSettingKey(string suggestionQuery, bool expectedIsValid)
        {
            var criteria = new SelectAutoSuggestionCriteria(1, suggestionQuery, false);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void IsValid_ActorId_Should_ReturnsValidSettingKey(int actorId, bool expectedIsValid)
        {
            var criteria = new SelectAutoSuggestionCriteria(actorId, "UserTest", false);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }

        [Fact]
        public void SelectAutoSuggestionCriteria_Always_ReturnsCorrectProperties()
        {
            var criteria = new SelectAutoSuggestionCriteria(1, "AC", false);

            Assert.Equal(1, criteria.UserId);
            Assert.Equal("AC", criteria.SuggestionQuery);
            Assert.False(criteria.IsInternal);
            Assert.Equal("Age_CustomerSelUserSearch", criteria.GetSettingKey());
        }
    }
}