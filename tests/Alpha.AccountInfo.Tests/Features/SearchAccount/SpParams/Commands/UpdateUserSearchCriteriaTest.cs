namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateUserSearchCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var expectedSettingKey = "Acc_CustomerUpdUserSearch";
            var criteria = new UpdateUserSearchCriteria(
                userId: 1,
                search: "MA",
                firstName: string.Empty,
                lastName: string.Empty,
                nickname: string.Empty);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void IsValid_UserId_Should_ReturnsValidSettingKey(int userId, bool expectedIsValid)
        {
            var criteria = new UpdateUserSearchCriteria(
               userId: userId,
               search: "MA",
               firstName: string.Empty,
               lastName: string.Empty,
               nickname: string.Empty);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }

        [Theory]
        [InlineData("MA", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValid_Search_Should_ReturnsCorrectValue(string search, bool expectedValue)
        {
            var criteria = new UpdateUserSearchCriteria(
               userId: 1,
               search: search,
               firstName: string.Empty,
               lastName: string.Empty,
               nickname: string.Empty);

            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Fact]
        public void Returns_CorrectProperties()
        {
            var criteria = new UpdateUserSearchCriteria(
                 userId: 1,
                 search: "AC",
                 firstName: "Leo",
                 lastName: "Nguyen",
                 nickname: "nickname");

            Assert.Equal("Leo", criteria.FirstName);
            Assert.Equal("Nguyen", criteria.LastName);
            Assert.Equal("nickname", criteria.Nickname);
            Assert.Equal("ACCOUNTING", criteria.System);
        }
    }
}