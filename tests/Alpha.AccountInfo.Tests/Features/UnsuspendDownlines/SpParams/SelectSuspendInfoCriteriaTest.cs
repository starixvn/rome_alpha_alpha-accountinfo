namespace Alpha.AccountInfo.Tests.Features.UnsuspendDownlines.SpParams
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class SelectDownlineListCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectSuspendInfoCriteria(1);
            var expectedSettingKey = "Acc_Customer_SuspendInfoSel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}