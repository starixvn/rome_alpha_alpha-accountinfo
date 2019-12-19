namespace Alpha.AccountInfo.Tests.Features.UnsuspendDownlines.SpParams
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
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
    }
}