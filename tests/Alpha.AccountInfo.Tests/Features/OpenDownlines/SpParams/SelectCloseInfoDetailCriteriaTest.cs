namespace Alpha.AccountInfo.Tests.Features.OpenDownlines.SpParams
{
    using Alpha.AccountInfo.Features.OpenDownlines.Params;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    public class SelectCloseInfoDetailCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectCloseInfoDetailCriteria(1, 1, true, 1, 1);
            var expectedSettingKey = "Acc_Customer_CloseInfoDetailSel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}