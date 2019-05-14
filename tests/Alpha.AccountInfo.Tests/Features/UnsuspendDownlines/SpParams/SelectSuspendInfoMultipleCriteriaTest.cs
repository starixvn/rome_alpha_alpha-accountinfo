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
    }
}