namespace Alpha.AccountInfo.Tests.Features.UnsuspendDownlines.SpParams
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class SelectSuspendInfoMultipleCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectSuspendInfoMultipleCriteria("1, 2", 1);
            var expectedSettingKey = "Acc_CustomerList_SuspendInfoSel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("1,2", true)]
        public void IsValid_Always_ReturnsCorrectValues(string custIds, bool expectedResult)
        {
            var criteria = new SelectSuspendInfoMultipleCriteria(custIds, 1);
            Assert.Equal(expectedResult, criteria.IsValid());
        }
    }
}