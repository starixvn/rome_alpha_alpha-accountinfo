namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class CheckMultipleCustomesBelongingCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new CheckMultipleCustomesBelongingCriteria(1, 0, "33,22");
            var expectedSettingKey = "Age_CustomerSelCheckBelonging_MultiCust";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("33,22", true)]
        public void IsValid_Always_ReturnsCorrectResult(string custIds, bool expectedIsValid)
        {
            var criteria = new CheckMultipleCustomesBelongingCriteria(1, 0, custIds);

            var actualResult = criteria.IsValid();

            Assert.Equal(expectedIsValid, actualResult);
        }

        [Fact]
        public void Returns_CorrectProperties()
        {
            var criteria = new CheckMultipleCustomesBelongingCriteria(1, 0, "33,22");

            Assert.Equal(0, criteria.SubAccountId);
            Assert.Equal(1, criteria.ActorId);
            Assert.Equal("33,22", criteria.CustIds);
        }
    }
}