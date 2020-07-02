namespace Alpha.AccountInfo.Site.Tests.Features.ProductStatusPopup.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Models.ProductStatus;
    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.ViewModels;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusViewModelTests
    {
        [Fact]
        public void IsDisabledEditCustStatus_IsIBCIndo_IsDirectDownline_ReturnFalse()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SiteGroup"] = "IBCIndo"
            };
            var viewModel = new ProductStatusViewModel(
                54,
                123,
                "razor",
                UserLevel.Master,
                new List<ProductStatusModel>(),
                UserLevel.Super,
                false);

            // Act
            var act = viewModel.IsDisabledEditCustStatus(new ProductStatusModel());

            // Assert
            Assert.False(act);
        }

        [Fact]
        public void IsDisabledEditCustStatus_IsIBCIndo_IsNotDirectDownline_ReturnTrue()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SiteGroup"] = "IBCIndo"
            };
            var viewModel = new ProductStatusViewModel(
                54,
                123,
                "razor",
                UserLevel.Agent,
                new List<ProductStatusModel>(),
                UserLevel.Super,
                false);

            // Act
            var act = viewModel.IsDisabledEditCustStatus(new ProductStatusModel());

            // Assert
            Assert.True(act);
        }

        [Fact]
        public void IsEnabledSubmitButton_IsIBCIndo_IsDirectDownline_ReturnTrue()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SiteGroup"] = "IBCIndo"
            };
            var viewModel = new ProductStatusViewModel(
                54,
                123,
                "razor",
                UserLevel.Master,
                new List<ProductStatusModel>(),
                UserLevel.Super,
                false);

            // Act
            var act = viewModel.IsEnabledSubmitButton(new ProductStatusModel());

            // Assert
            Assert.True(act);
        }

        [Fact]
        public void IsEnabledSubmitButton_IsIBCIndo_IsNotDirectDownline_ReturnTrue()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SiteGroup"] = "IBCIndo"
            };
            var viewModel = new ProductStatusViewModel(
                54,
                123,
                "razor",
                UserLevel.Agent,
                new List<ProductStatusModel>(),
                UserLevel.Super,
                false);

            // Act
            var act = viewModel.IsEnabledSubmitButton(new ProductStatusModel());

            // Assert
            Assert.False(act);
        }

        [Fact]
        public void IsShowMessageWarnning_IsIBCIndo_ReturnFalse()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SiteGroup"] = "IBCIndo"
            };

            var viewModel = new ProductStatusViewModel(
                54,
                123,
                "razor",
                UserLevel.Agent,
                new List<ProductStatusModel>(),
                UserLevel.Super,
                false);

            // Act
            var act = viewModel.IsShowMessageWarnning;

            // Assert
            Assert.False(act);
        }

        [Fact]
        public void IsShowMessageWarnning_IsMBC_AgentUpdateMember_ReturnFalse()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SiteGroup"] = "IBCBET"
            };

            var viewModel = new ProductStatusViewModel(
                54,
                123,
                "razor",
                UserLevel.Member,
                new List<ProductStatusModel>(),
                UserLevel.Agent,
                false);

            // Act
            var act = viewModel.IsShowMessageWarnning;

            // Assert
            Assert.False(act);
        }

        [Fact]
        public void IsShowMessageWarnning_IsMBC_MasterUpdateMember_ReturnTrue()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SiteGroup"] = "IBCBET"
            };

            var viewModel = new ProductStatusViewModel(
                1,
                123,
                "razor",
                UserLevel.Member,
                new List<ProductStatusModel>(),
                UserLevel.Master,
                false);

            // Act
            var act = viewModel.IsShowMessageWarnning;

            // Assert
            Assert.True(act);
        }
    }
}
