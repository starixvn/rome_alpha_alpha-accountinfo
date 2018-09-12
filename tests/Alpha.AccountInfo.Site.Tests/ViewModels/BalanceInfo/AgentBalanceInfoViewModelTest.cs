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
        private readonly IUser viewer = BalanceInfoHelper.ViewerFixture(UserLevel.Master.Value);
        private readonly CustomerInfoModel customerInfo = BalanceInfoHelper.BuildCustomerInfo(UserLevel.Agent.Value);

        [Fact]
        public void TotalBalance_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedTotalBalance = 28;

            var viewModel = BalanceInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.TotalBalance, expectedTotalBalance);
        }

        [Fact]
        public void YesterdayBalance_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedYesterdayBalance = 30;

            var viewModel = BalanceInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.YesterdayBalance, expectedYesterdayBalance);
        }

        [Fact]
        public void GivenCreditLimit_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedGivenCreditLimit = ">=23 and <=85";

            var viewModel = BalanceInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.GivenCreditLimit, expectedGivenCreditLimit);
        }

        [Fact]
        public void MemberMaxCreditLimit_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedMemberMaxCreditLimit = ">=25 and <=120";

            var viewModel = BalanceInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.MemberMaxCreditLimit, expectedMemberMaxCreditLimit);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        public void ShowMemberMaxCreditLimit_Always_ReturnCorrectValue(byte actorLevel, bool expectedValue)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = BalanceInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.ShowMemberMaxCreditLimit, expectedValue);
        }


        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        public void ShowTransferLink_ActorLevel_ReturnCorrectValue(byte actorLevel, bool expectedShowTransferLink)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = BalanceInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.ShowTransferLink, expectedShowTransferLink);
        }
    }
}
