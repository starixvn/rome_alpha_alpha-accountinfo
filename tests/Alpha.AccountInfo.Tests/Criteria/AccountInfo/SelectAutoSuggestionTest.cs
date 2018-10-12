namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectAutoSuggestionTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_Should_ReturnsCorrectValue(int custId, bool expectedValue)
        {
            var criteria = new SelectAutoSuggestionCriteria(custId, "AC", false);

            Assert.Equal(expectedValue, criteria.IsValid());
        }

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
    }
}