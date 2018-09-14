namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class AccountInfoCriteriaTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_Should_ReturnsCorrectValue(int custId, bool expectedValue)
        {
            var criteria = new AccountInfoCriteriaBase(custId);

            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}
