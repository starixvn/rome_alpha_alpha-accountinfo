namespace Alpha.AccountInfo.Tests.Features.OpenDownlines.SpParams
{
    using Alpha.AccountInfo.Features.OpenDownlines.Params;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    public class SelectCloseInfoCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectCloseInfoCriteria(1);
            var expectedSettingKey = "Acc_Customer_CloseInfoSel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}