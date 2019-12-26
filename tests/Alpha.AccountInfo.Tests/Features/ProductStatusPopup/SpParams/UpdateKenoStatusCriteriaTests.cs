namespace Alpha.AccountInfo.Tests.Features.ProductStatusPopup.SpParams
{
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class UpdateKenoStatusCriteriaTests
    {
        private const int CustIdTest = 1176083;
        private const int SubAccIdTest = 536751;
        private const int UserIdTest = 1176070;
        private const string IPTest = "127.0.0.1";
        private const string LanguageTest = "E";
        private const int BetTypeTest = 1501;
        private const bool DisableTest = false;
        private const int UplineIdTest = 1176070;

        [Fact]
        public void UpdateKenoStatusCriteria_Always_ReturnsCorrectProperties()
        {
            // Arrange
            var criteria = new UpdateKenoStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, BetTypeTest, DisableTest, UplineIdTest);
            var expectedSettingKey = "Acc_CustomerUpdDisableKeno";

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(BetTypeTest, criteria.BetType);
            Assert.Equal(DisableTest, criteria.Disable);
            Assert.Equal(UplineIdTest, criteria.UplineId);
            Assert.Equal(LanguageTest, criteria.Language);
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var expectedResult = true;
            var criteria = new UpdateKenoStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, BetTypeTest, DisableTest, UplineIdTest);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(0, false)]
        [InlineData(BetTypeTest, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_BetType_ReturnsExpectedResult(int betType, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new UpdateKenoStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, betType, DisableTest, UplineIdTest);

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(0, false)]
        [InlineData(UplineIdTest, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_UplineId_ReturnsExpectedResult(int uplineId, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new UpdateKenoStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, BetTypeTest, DisableTest, uplineId);

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
