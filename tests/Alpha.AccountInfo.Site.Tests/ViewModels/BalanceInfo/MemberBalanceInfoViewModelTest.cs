namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo-ViewModel")]
    public class MemberBalanceInfoViewModelTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(2);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo();

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, false)]
        [InlineData(2, true)]
        public void ShowTransferLink_ActorLevel_ReturnCorrectValue(byte actorLevel, bool expectedShowTransferLink)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.ShowTransferLink, expectedShowTransferLink);
        }

        [Fact]
        public void TransferLinkOnYesterdayBalance_YesterdayBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_False_ReturnsCorrectLink()
        {
            var messages = new CustomerInfoQuery(viewer);
            viewer.AllowedTotalBalanceTransfer.Returns(false);
            var expectedTransferLink = "MicroSiteUrl/_Transfer/Agent/MemberTransfer.aspx?custid=1&CallBack=false";

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.YesterdayBalanceTransferLink, expectedTransferLink);
        }

        [Fact]
        public void TransferLinkOnTotalBalance_TotaBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_True_ReturnsCorrectLink()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedTransferLink = "MicroSiteUrl/_Transfer/Agent/MemberTransfer.aspx?custid=1&CallBack=false";

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.TotalBalanceTransferLink, expectedTransferLink);
        }

        [Fact]
        public void BetCredit_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedBetCredit = 29;

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.BetCredit, expectedBetCredit);
        }
    }
}
