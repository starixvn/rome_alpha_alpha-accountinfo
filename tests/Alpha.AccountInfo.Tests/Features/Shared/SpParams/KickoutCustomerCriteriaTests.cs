namespace Alpha.AccountInfo.Tests.Features.Shared.SpParams
{
    using Alpha.AccountInfo.Features.Shared.Params.SPs;
    using Xunit;

    public class KickoutCustomerCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_Return_CorrectSPName()
        {
            // Arrange
            var criteria = new KickoutCustomerCriteria(
                1, 3, 90, "::1", 0, "ChangeProtectionMode");

            // Act
            var settingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal("Age_OnUser_ForceKickedout", settingKey);
        }

        [Fact]
        public void Constructor_Always_SetCorrectProperties()
        {
            // Arrange
            var criteria = new KickoutCustomerCriteria(
                1, 3, 90, "::1", 0, "ChangeProtectionMode");

            // Act & Assert
            Assert.True(
                criteria.CustId == 1
                && criteria.RoleId == 3
                && criteria.ServerId == 90
                && criteria.IP == "::1"
                && criteria.SubAccId == 0
                && criteria.Mode == "ChangeProtectionMode");
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(8, true)]
        public void IsValid_CustId_ReturnCorrectValue(int custId, bool expected)
        {
            // Arrange
            var criteria = new KickoutCustomerCriteria(
                custId, 3, 90, "::1", 0, "ChangeProtectionMode");

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(90, true)]
        public void IsValid_ServerId_ReturnCorrectValue(int serverId, bool expected)
        {
            // Arrange
            var criteria = new KickoutCustomerCriteria(
                1, 3, serverId, "::1", 0, "ChangeProtectionMode");

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
