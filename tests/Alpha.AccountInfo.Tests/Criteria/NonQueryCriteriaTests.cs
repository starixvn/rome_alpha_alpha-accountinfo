namespace Alpha.AccountInfo.Tests.Criteria
{
    using System;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class NonQueryCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ThrowsNotImplementedException()
        {
            // Arrange
            var nonQueryCriteria = new NonQueryCriteriaForTesting(536751, 1176070, "E", "::1");

            // Act& Assert
            Assert.Throws<NotImplementedException>(() => nonQueryCriteria.GetSettingKey());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var expectedResult = true;
            var nonQueryCriteria = new NonQueryCriteriaForTesting(536751, 1176070, "E", "::1");

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_SubAccId_ReturnsExpectedResult(int subAccId, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new NonQueryCriteriaForTesting(subAccId, 1176070, "E", "::1");

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new NonQueryCriteriaForTesting(536751, custId, "E", "::1");

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("E", true)]
        public void IsValid_Language_ReturnsExpectedResult(string language, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new NonQueryCriteriaForTesting(536751, 1176070, language, "::1");

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("127.0.0.1", true)]
        public void IsValid_IP_ReturnsExpectedResult(string ip, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new NonQueryCriteriaForTesting(536751, 1176070, "E", ip);

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
