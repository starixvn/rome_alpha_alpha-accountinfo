namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.Shared;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateAllDayBalanceTransferCommandTests
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_SearchAccountId_Should_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            // Arrange
            var criteria = new UpdateAllDayBalanceTransferCommand(searchAccountId, 1462, "E", "192.168.1.1", true);

            // Act
            var act = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, act);
        }
    }
}