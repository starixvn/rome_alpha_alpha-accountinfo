namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class AccountInfoCriteriaBaseTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void AccountInfoCriteriaBase_Should_ReturnsCorrectValue(int custId, bool expectedValue)
        {
            var criteria = new AccountInfoCriteriaBase(custId);

            Assert.Equal(string.Empty, criteria.GetSettingKey());
            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}