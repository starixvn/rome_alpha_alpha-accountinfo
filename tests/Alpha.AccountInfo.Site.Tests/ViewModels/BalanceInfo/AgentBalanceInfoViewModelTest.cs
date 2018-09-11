namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo-ViewModel")]
    public class AgentBalanceInfoViewModelTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(3);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo(2);

        [Fact]
        public void TotalBalance_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedTotalBalance = 28;

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.TotalBalance, expectedTotalBalance);
        }

        [Fact]
        public void YesterdayBalance_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedYesterdayBalance = 30;

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.YesterdayBalance, expectedYesterdayBalance);
        }

        [Fact]
        public void GivenCreditLimit_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedGivenCreditLimit = ">=23 and <=85";

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.GivenCreditLimit, expectedGivenCreditLimit);
        }

        [Theory]
        [InlineData(4, "")]
        [InlineData(3, ">=25 and <=120")]
        public void MemberMaxCreditLimit_Always_ReturnCorrectValue(byte actorLevel, string expectedMemberMaxCreditLimit)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.MemberMaxCreditLimit, expectedMemberMaxCreditLimit);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        public void ShowTransferLink_ActorLevel_ReturnCorrectValue(byte actorLevel, bool expectedShowTransferLink)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.ShowTransferLink, expectedShowTransferLink);
        }

        [Fact]
        public void TransferLinkOnYesterdayBalance_YesterdayBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_False_ReturnsCorrectLink()
        {
            viewer.AllowedTotalBalanceTransfer.Returns(false);
            var messages = new CustomerInfoQuery(viewer);

            var expectedTransferLink = "MicroSiteUrl/_Transfer/Master/AgentTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=30";

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.YesterdayBalanceTransferLink, expectedTransferLink);
        }

        [Fact]
        public void TransferLinkOnTotalBalance_TotaBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_True_ReturnsCorrectLink()
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(3));
            var expectedTransferLink = "MicroSiteUrl/_Transfer/Master/AgentTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=28";

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.TotalBalanceTransferLink, expectedTransferLink);
        }
    }
}
