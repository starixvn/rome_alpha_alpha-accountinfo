namespace Alpha.AccountInfo.Tests.Features.UnsuspendDownlines.SpParams
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class SelectSuspendInfoDetailCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectSuspendInfoDetailCriteria(1, 1, true, 1, 1);
            var expectedSettingKey = "Acc_Customer_SuspendInfoDetailSel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}