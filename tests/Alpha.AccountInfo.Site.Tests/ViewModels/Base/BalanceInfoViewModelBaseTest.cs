namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using NSubstitute;
    using System;
    using System.Resources;
    using Xunit;

    [Trait("Category", "AccountInfo-ViewModel")]
    public class BalanceInfoViewModelBaseTest
    {
        private readonly IUser viewer;
        private CustomerInfoModel customerInfo =>
            new CustomerInfoModel(
                custId: 1,
                roleId: 1,
                userName: "UserTest",
                userName2: string.Empty,
                firstName: string.Empty,
                lastName: string.Empty,
                phone: string.Empty,
                mobilePhone: string.Empty,
                fax: string.Empty,
                closedReason: 2,
                enabled: true,
                uplineEnabled: true,
                closed: true,
                suspended: true,
                creatDate: DateTime.Now,
                lastLogged: DateTime.Now,
                lastTxn: DateTime.Now,
                lastLoginIP: string.Empty,
                pwdExpiry: DateTime.Now,
                memberId: 11,
                memberName: "Member Name",
                agentId: 12,
                agentName: "Agent Name",
                masterId: 13,
                masterName: "Master Name",
                superId: 14,
                superName: "Super Name");

        private IBalanceInfoModel memberBalanceInfo 
            => new MemberBalanceInfoModel(
              custId: 1,
              credit: 12,
              agentPositionTaking: 13,
              customerCash: 14,
              yesterday_CustomerCash: 15,
              customerOutstanding: 16,
              casinoOutstanding: 17,
              casinoTransfer: 18,
              netPosition: 19,
              memMaxCredit_Max: 20);

        public BalanceInfoViewModelBaseTest()
        {
            viewer = Substitute.For<IUser>();
            viewer.SubAccId.Returns(0);
            viewer.Id.Returns(1);
            viewer.LevelId.Returns((byte)2);
            viewer.CanViewReports.Returns(true);
            viewer.AllowedTotalBalanceTransfer.Returns(true);
            viewer.AllowedAllDayBalTransfer.Returns(false);
            viewer.AllowTransfer.Returns(true);
        }

        private string GetOldMainSiteUrl(string microSiteName, string virtualPath) => "OldMain/";

        private string GetReportSiteUrl(string microSiteName, string virtualPath) => "Report/";

        private string ConvertResourcesJson(string language, ResourceManager resources) => "{}";

        [Fact]
        public void ShowTransferLink_Allow_AllDay_Balance_Transfer_ReturnFalse()
        {
            // Arrange
            viewer.AllowedAllDayBalTransfer.Returns(true);
            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel = new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.False(viewModel.ShowTransferLink);
        }

        [Fact]
        public void ShowTransferLink_Does_Not_Allow_Transfer_ReturnFalse()
        {
            // Arrange
            viewer.AllowTransfer.Returns(false);
            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel = new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.False(viewModel.ShowTransferLink);
        }

        [Fact]
        public void TransferLinkOnTotalBalance_AllowedTotalBalanceTransfer_Is_False_ReturnsEmptyLink()
        {
            // Arrange
            viewer.AllowedTotalBalanceTransfer.Returns(false);
            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel = new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TransferLinkOnTotalBalance, string.Empty);
        }

        [Fact]
        public void TransferLinkOnTotalBalance_TotalBalance_Is_Zero_ReturnsEmptyLink()
        {
            // Arrange
            var balanceInfo = new MemberBalanceInfoModel(
             custId: 1,
             credit: 12,
             agentPositionTaking: 13,
             customerCash: 0, // TotalBalance
             yesterday_CustomerCash: 15,
             customerOutstanding: 16,
             casinoOutstanding: 17,
             casinoTransfer: 18,
             netPosition: 19,
             memMaxCredit_Max: 20);

            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel = new MemberBalanceInfoViewModel(messages, balanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TransferLinkOnTotalBalance, string.Empty);
        }

        [Fact]
        public void TransferLinkOnYesterdayBalance_AllowedTotalBalanceTransfer_Is_True_ReturnsEmptyLink()
        {
            // Arrange
            viewer.AllowedTotalBalanceTransfer.Returns(true);
            var messages = new CustomerInfoQuery(viewer);

            // Act
            var memberBalanceInfoViewModel = new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(memberBalanceInfoViewModel.TransferLinkOnYesterdayBalance, string.Empty);
        }

        [Fact]
        public void TransferLinkOnYesterdayBalance_YesterdayBalance_Is_Zero_ReturnsEmptyLink()
        {
            // Arrange
            var balanceInfo = new MemberBalanceInfoModel(
             custId: 1,
             credit: 12,
             agentPositionTaking: 13,
             customerCash: 14, 
             yesterday_CustomerCash: 0, // YesterdayBalance
             customerOutstanding: 16,
             casinoOutstanding: 17,
             casinoTransfer: 18,
             netPosition: 19,
             memMaxCredit_Max: 20);

            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel = new MemberBalanceInfoViewModel(messages, balanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TransferLinkOnYesterdayBalance, string.Empty);
        }

        [Fact]
        public void ReportLink_CanViewReport_Is_False_ReturnsEmptyLink()
        {
            // Arrange
            viewer.CanViewReports.Returns(false);
            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel =
                new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetReportSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.BetListLink, string.Empty);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0)]
        public void ReportLink_Outstanding_Less_Than_Zero_ReturnsEmptyLink(decimal outstanding)
        {
            // Arrange
            viewer.CanViewReports.Returns(false);
            var messages = new CustomerInfoQuery(viewer);
            var memberBalanceInfo =
                new MemberBalanceInfoModel(
              custId: 1,
              credit: 12,
              agentPositionTaking: 13,
              customerCash: 14,
              yesterday_CustomerCash: 15,
              customerOutstanding: outstanding,
              casinoOutstanding: 17,
              casinoTransfer: 18,
              netPosition: 19,
              memMaxCredit_Max: 20);

            // Act
            var viewModel =
                new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetReportSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.BetListLink, string.Empty);
        }
    }
}
