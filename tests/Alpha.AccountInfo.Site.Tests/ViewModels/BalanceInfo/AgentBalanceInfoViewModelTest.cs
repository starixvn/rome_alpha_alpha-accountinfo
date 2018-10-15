namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Requests.SearchAccount;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class AgentBalanceInfoViewModelTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Master.Value);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo(UserLevel.Agent.Value);

        [Fact]
        public void MasterBalanceInfoViewModel_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.True(
                viewModel.TotalCash.Equals(13) &&
                viewModel.YesterdayCash.Equals(14) &&
                viewModel.TotalBalance.Equals(28) &&
                viewModel.YesterdayBalance.Equals(30) &&
                viewModel.Outstanding.Equals(19) &&
                viewModel.MemberMaxCredit.Equals(26) &&
                viewModel.UsedCredit.Equals(22) &&
                viewModel.GivenCredit.Equals(120) &&
                viewModel.GivenCreditLimit.Equals(">=23 and <=85") &&
                viewModel.MemberMaxCreditLimit.Equals(">=25 and <=120"));
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        public void ShowMemberMaxCreditLimit_Always_ReturnCorrectValue(byte actorLevel, bool expectedValue)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.ShowMemberMaxCreditLimit, expectedValue);
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
    }
}