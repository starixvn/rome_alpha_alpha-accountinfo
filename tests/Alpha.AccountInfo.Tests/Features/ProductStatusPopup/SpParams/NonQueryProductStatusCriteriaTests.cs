namespace Alpha.AccountInfo.Tests.Features.ProductStatusPopup.SpParams
{
    using System;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class NonQueryProductStatusCriteriaTests
    {
        private const int CustIdTest = 1176083;
        private const int SubAccIdTest = 536751;
        private const int UserIdTest = 1176070;
        private const string IPTest = "127.0.0.1";

        [Fact]
        public void GetSettingKey_Always_ThrowsNotImplementedException()
        {
            // Arrange
            var nonQueryCriteria = new NonQueryProductStatusCriteriaForTesting(CustIdTest, SubAccIdTest, UserIdTest, IPTest);

            // Act& Assert
            Assert.Throws<NotImplementedException>(() => nonQueryCriteria.GetSettingKey());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var expectedResult = true;
            var nonQueryCriteria = new NonQueryProductStatusCriteriaForTesting(CustIdTest, SubAccIdTest, UserIdTest, IPTest);

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(CustIdTest, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new NonQueryProductStatusCriteriaForTesting(custId, SubAccIdTest, UserIdTest, IPTest);

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(SubAccIdTest, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_SubAccId_ReturnsExpectedResult(int subAccId, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new NonQueryProductStatusCriteriaForTesting(CustIdTest, subAccId, UserIdTest, IPTest);

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(UserIdTest, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_UserId_ReturnsExpectedResult(int userId, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new NonQueryProductStatusCriteriaForTesting(CustIdTest, SubAccIdTest, userId, IPTest);

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
