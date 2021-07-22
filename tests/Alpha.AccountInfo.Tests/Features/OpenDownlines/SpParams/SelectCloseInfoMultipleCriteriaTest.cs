namespace Alpha.AccountInfo.Tests.Features.OpenDownlines.SpParams
{
    using Alpha.AccountInfo.Features.OpenDownlines.Params;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    public class SelectCloseInfoMultipleCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectCloseInfoMultipleCriteria("1, 2", 1);
            var expectedSettingKey = "Acc_CustomerList_CloseInfoSel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("1,2", true)]
        public void IsValid_Always_ReturnsCorrectValues(string custIds, bool expectedResult)
        {
            var criteria = new SelectCloseInfoMultipleCriteria(custIds, 1);
            Assert.Equal(expectedResult, criteria.IsValid());
        }
    }
}