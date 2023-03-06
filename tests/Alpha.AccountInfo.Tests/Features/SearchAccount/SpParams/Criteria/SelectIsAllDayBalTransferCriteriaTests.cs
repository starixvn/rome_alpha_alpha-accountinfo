namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams.Criteria
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectIsAllDayBalTransferCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new SelectIsAllDayBalTransferCriteria(1176086);
            var expectedSettingKey = "Age_GetIsAllDayBalTransfer";

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1176086, true)]
        [InlineData(0, false)]
        [InlineData(-1176086, false)]
        public void IsValid_Always_ReturnsCorrectValue(int custId, bool expectedResult)
        {
            // Arrange
            var criteria = new SelectIsAllDayBalTransferCriteria(custId);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
