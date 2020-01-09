namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectCreditInfoCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectCreditInfoCriteria(1);
            var expectedSettingKey = "Age_Customer_CreditInfo_Sel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-3, false)]
        public void IsValid_Always_ReturnsCorrectResult(int custId, bool expectedResult)
        {
            var criteria = new SelectCreditInfoCriteria(custId);

            var actualResutl = criteria.IsValid();

            Assert.Equal(expectedResult, actualResutl);
        }
    }
}