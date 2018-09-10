namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using NSubstitute;
    using System.Resources;
    using Xunit;

    [Trait("Category", "AccountInfo-ViewModel")]
    public class MasterBalanceInfoViewModelTest
    {
        private readonly IUser viewer = StubAccountInfo.GetViewer(4);
        private readonly CustomerInfoModel customerInfo = StubAccountInfo.CustomerInfo;

        private IBalanceInfoModel masterBalanceInfo 
            => new MasterBalanceInfoModel(
              custId: 1,
              credit: 12,
              masterCash: 13,
              yesterday_MasterCash: 14,
              masterPositionTaking: 15,
              agentCash: 16,
              yesterday_AgentCash: 17,
              customerCash: 18,
              yesterday_CustomerCash: 19,
              customerOutstanding: 20,
              casinoOutstanding: 21,
              casinoTransfer: 22,
              masterUsedCredit: 23,
              masterCredit_Min: 24,
              masterCredit_Max: 85);

        private string GetOldMainSiteUrl(string microSiteName, string virtualPath) => "OldMain/";

        private string GetReportSiteUrl(string microSiteName, string virtualPath) => "Report/";

        private string ConvertResourcesJson(string language, ResourceManager resources) => "{}";

        [Fact]
        public void TotalBalance_Always_ReturnCorrectValue()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedTotalBalance = 47;

            // Act
            var viewModel = new MasterBalanceInfoViewModel(messages, masterBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TotalBalance, expectedTotalBalance);
        }

        [Fact]
        public void YesterdayBalance_Always_ReturnCorrectValue()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedYesterdayBalance = 50;

            // Act
            var viewModel = new MasterBalanceInfoViewModel(messages, masterBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.YesterdayBalance, expectedYesterdayBalance);
        }

        [Fact]
        public void GivenCreditLimit_Always_ReturnCorrectValue()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedGivenCreditLimit = ">=24 and <=85";

            // Act
            var viewModel = new MasterBalanceInfoViewModel(messages, masterBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.GivenCreditLimit, expectedGivenCreditLimit);
        }

        [Fact]
        public void TransferLinkOnYesterdayBalance_YesterdayBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_False_ReturnsCorrectLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            messages.UserName = "UserTest";
            viewer.AllowedTotalBalanceTransfer.Returns(false);
            var expectedTransferLink = "OldMain/_Transfer/Super/MasterTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=50";

            // Act
            var viewModel = new MasterBalanceInfoViewModel(messages, masterBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TransferLinkOnYesterdayBalance, expectedTransferLink);
        }

        [Fact]
        public void TransferLinkOnTotalBalance_TotaBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_True_ReturnsCorrectLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            messages.UserName = "UserTest";
            var expectedTransferLink = "OldMain/_Transfer/Super/MasterTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=47";

            // Act
            var viewModel = new MasterBalanceInfoViewModel(messages, masterBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TransferLinkOnTotalBalance, expectedTransferLink);
        }

        [Fact]
        public void OutstandingLink_CanViewReport_Is_True_And_Outstanding_Greater_Than_Zero_ReturnsCorrectOutstandingLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedOutstandingLink = "OldMain/outstanding/Master?CustId=1&CustName=&CustLevelId=3&BreadcrumbLevelJson=[{'CustId':14,'CustName':'Super Name','CustLevelId':4},{'CustId':13,'CustName':'Master Name','CustLevelId':3},{'CustId':12,'CustName':'Agent Name','CustLevelId':2}]";

            // Act
            var viewModel = new MasterBalanceInfoViewModel(messages, masterBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.OutstandingLink, expectedOutstandingLink);
        }
    }
}
