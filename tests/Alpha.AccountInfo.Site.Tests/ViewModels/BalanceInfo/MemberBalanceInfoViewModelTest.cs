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
    public class MemberBalanceInfoViewModelTest
    {
        private readonly IUser viewer = StubAccountInfo.GetViewer(2);
        private readonly CustomerInfoModel customerInfo = StubAccountInfo.CustomerInfo;

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

        private string GetOldMainSiteUrl(string microSiteName, string virtualPath) => "OldMain/";

        private string GetBetListSiteUrl(string microSiteName, string virtualPath) => "BetList/";

        private string ConvertResourcesJson(string language, ResourceManager resources) => "{}";

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, false)]
        [InlineData(2, true)]
        public void ShowTransferLink_ActorLevel_ReturnCorrectValue(byte actorLevel, bool expectedShowTransferLink)
        {
            // Arrange
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            // Act
            var viewModel = new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetBetListSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.ShowTransferLink, expectedShowTransferLink);
        }

        [Fact]
        public void TransferLinkOnYesterdayBalance_YesterdayBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_False_ReturnsCorrectLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            viewer.AllowedTotalBalanceTransfer.Returns(false);
            var expectedTransferLink = "OldMain/_Transfer/Agent/MemberTransfer.aspx?custid=1&CallBack=false";

            // Act
            var viewModel =
                new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);
            
            // Assert
            Assert.Equal(viewModel.TransferLinkOnYesterdayBalance, expectedTransferLink);
        }

        [Fact]
        public void TransferLinkOnTotalBalance_TotaBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_True_ReturnsCorrectLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedTransferLink = "OldMain/_Transfer/Agent/MemberTransfer.aspx?custid=1&CallBack=false";

            // Act
            var viewModel =
                new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetOldMainSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.TransferLinkOnTotalBalance, expectedTransferLink);
        }

        [Fact]
        public void BetListLink_CanViewReport_Is_True_And_Outstanding_Greater_Than_Zero_ReturnsCorrectBelistLink()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedBetListLink = "BetList/RunningBetList/Index?CustID=1&amp;CustName=ACGUUS3838033&amp;GetAll=True";

            // Act
            var viewModel =
                new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetBetListSiteUrl, ConvertResourcesJson);
            
            // Assert
            Assert.Equal(viewModel.BetListLink, expectedBetListLink);
        }

        [Fact]
        public void BetCredit_Always_ReturnCorrectValue()
        {
            // Arrange
            var messages = new CustomerInfoQuery(viewer);
            var expectedBetCredit = 29;

            // Act
            var viewModel = new MemberBalanceInfoViewModel(messages, memberBalanceInfo, customerInfo, GetBetListSiteUrl, ConvertResourcesJson);

            // Assert
            Assert.Equal(viewModel.BetCredit, expectedBetCredit);
        }
    }
}
