namespace Alpha.AccountInfo.Tests.Configurations
{
    using System.Collections.Specialized;
    using System.Configuration;
    using AccountInfo.Configurations;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class AppSettingsTests
    {
        [Fact]
        public void Verify_MissingStoredProcedurePath_ThrowsConfigurationErrorsException()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SystemId"] = "2"
            };

            // Act & Assert
            Assert.Throws<ConfigurationErrorsException>(() => AppSettings.Verify());
        }

        [Fact]
        public void Verify_MissingSystemId_ThrowsConfigurationErrorsException()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["StoredProcedurePath"] = "Local"
            };

            // Act & Assert
            Assert.Throws<ConfigurationErrorsException>(() => AppSettings.Verify());
        }

        [Fact]
        public void Verify_HasEnoughSettings_PassVerify()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["StoredProcedurePath"] = "Local",
                ["SystemId"] = "2"
            };

            // Act
            AppSettings.Verify();

            // Assert
            Assert.True(true);
        }
    }
}
