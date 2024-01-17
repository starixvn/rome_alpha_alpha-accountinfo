namespace Alpha.AccountInfo.Tests.Features.ProtectedStatus.SpParams
{
    using Alpha.AccountInfo.Features.ProtectedStatus.Params.SPs;
    using Xunit;

    [Trait("Category", "ProtectedStatus")]
    public class GetProtectedStatusCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new GetProtectedStatusCriteria(1, 4, "2,3,4,5,6,7");
            var expectedSettingKey = "Age_CustomerSelProtectionModeList";

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(22, true)]
        [InlineData(0, false)]
        [InlineData(-22, false)]
        public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
        {
            // Arrange
            var criteria = new GetProtectedStatusCriteria(custId, 4, "2,3,4,5,6,7");

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actualIsValid);
        }

        [Theory]
        [InlineData(4, true)]
        [InlineData(3, true)]
        [InlineData(2, false)]
        [InlineData(1, false)]
        public void IsValid_RoleId_ReturnsExpectedResult(byte roleId, bool expected)
        {
            // Arrange
            var criteria = new GetProtectedStatusCriteria(1, roleId, "2,3,4,5,6,7");

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actualIsValid);
        }

        [Theory]
        [InlineData("2,3,4,5,6,7", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("          ", false)]
        public void IsValid_CustList_ReturnsExpectedResult(string custList, bool expected)
        {
            // Arrange
            var criteria = new GetProtectedStatusCriteria(1, 4, custList);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actualIsValid);
        }
    }
}
