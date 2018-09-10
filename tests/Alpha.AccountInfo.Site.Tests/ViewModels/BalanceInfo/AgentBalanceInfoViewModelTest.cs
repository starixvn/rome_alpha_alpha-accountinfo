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
    public class AgentBalanceInfoViewModelTest
    {
        private readonly IUser viewer = StubAccountInfo.GetViewer(3);
        private readonly CustomerInfoModel customerInfo = StubAccountInfo.CustomerInfo;

        private IBalanceInfoModel agentBalanceInfo
            => new AgentBalanceInfoModel(
              custId: 1,
              credit: 120,
              agentCash: 13,
              yesterday_AgentCash: 14,
              customerCash: 15,
              yesterday_CustomerCash: 16,
              customerOutstanding: 17,
              casinoOutstanding: 18,
              agentPositionTaking: 19,
              casinoTransfer: 20,
              masterUsedCredit: 21,
              agentUsedCredit: 22,
              agentCredit_Min: 23,
              agentCredit_Max: 85,
              memMaxCredit_Min: 25,
              memberMaxCredit: 26);

        private string GetOldMainSiteUrl(string microSiteName, string virtualPath) => "OldMain/";

        private string GetReportSiteUrl(string microSiteName, string virtualPath) => "Report/";

        private string ConvertResourcesJson(string language, ResourceManager resources) => "{}";

        [Fact]
        public void TotalBalance_Always_ReturnCorrectValue()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedTotalBalance = 28;

            // Act
            var viewModel = new AgentBalanceInfoViewModel(messages, agentBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TotalBalance, expectedTotalBalance);
        }

        [Fact]
        public void YesterdayBalance_Always_ReturnCorrectValue()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedYesterdayBalance = 30;

            // Act
            var viewModel = new AgentBalanceInfoViewModel(messages, agentBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.YesterdayBalance, expectedYesterdayBalance);
        }

        [Fact]
        public void GivenCreditLimit_Always_ReturnCorrectValue()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedGivenCreditLimit = ">=23 and <=85";

            // Act
            var viewModel = new AgentBalanceInfoViewModel(messages, agentBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.GivenCreditLimit, expectedGivenCreditLimit);
        }

        [Theory]
        [InlineData(4, "")]
        [InlineData(3, ">=25 and <=120")]
        public void MemberMaxCreditLimit_Always_ReturnCorrectValue(byte actorLevel, string expectedMemberMaxCreditLimit)
        {
            // Arrange
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel = new AgentBalanceInfoViewModel(messages, agentBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.MemberMaxCreditLimit, expectedMemberMaxCreditLimit);
        }

        [Fact]
        public void OutstandingLink_CanViewReport_Is_True_And_Outstanding_Greater_Than_Zero_ReturnsCorrectOutstandingLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedOutstandingLink = "Report/outstanding/Agent?CustId=1&CustName=&CustLevelId=2&BreadcrumbLevelJson=[{'CustId':13,'CustName':'Master Name','CustLevelId':3},{'CustId':12,'CustName':'Agent Name','CustLevelId':2}]";

            // Act
            var viewModel =
                new AgentBalanceInfoViewModel(messages, agentBalanceInfo, customerInfo, GetReportSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.OutstandingLink, expectedOutstandingLink);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        public void ShowTransferLink_ActorLevel_ReturnCorrectValue(byte actorLevel, bool expectedShowTransferLink)
        {
            // Arrange
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel = new AgentBalanceInfoViewModel(messages, agentBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.ShowTransferLink, expectedShowTransferLink);
        }

        [Fact]
        public void TransferLinkOnYesterdayBalance_YesterdayBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_False_ReturnsCorrectLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            messages.UserName = "UserTest";
            viewer.AllowedTotalBalanceTransfer.Returns(false);
            var expectedTransferLink = "OldMain/_Transfer/Master/AgentTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=30";

            // Act
            var viewModel =
                new AgentBalanceInfoViewModel(messages, agentBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TransferLinkOnYesterdayBalance, expectedTransferLink);
        }

        [Fact]
        public void TransferLinkOnTotalBalance_TotaBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_True_ReturnsCorrectLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            messages.UserName = "UserTest";
            var expectedTransferLink = "OldMain/_Transfer/Master/AgentTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=28";

            // Act
            var viewModel =
                new AgentBalanceInfoViewModel(messages, agentBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TransferLinkOnTotalBalance, expectedTransferLink);
        }
    }
}
