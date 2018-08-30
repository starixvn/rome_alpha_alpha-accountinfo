
namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateCustomerStatusCriteriaTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_Should_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            var criteria = new UpdateCustomerStatusCriteria( searchAccountId, 1462, 0, "E", "192.168.1.1");

            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}
