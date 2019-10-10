namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class AgentBalanceInfoViewModelTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Master.Value);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo(UserLevel.Agent.Value);
        private readonly CreditInfo creditInfo = AccountInfoHelper.CreditInfo;

        [Fact]
        public void MasterBalanceInfoViewModel_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.True(
                viewModel.TotalCash.Equals(13) &&
                viewModel.YesterdayCash.Equals(14) &&
                viewModel.TotalBalance.Equals(28) &&
                viewModel.YesterdayBalance.Equals(30) &&
                viewModel.Outstanding.Equals(19) &&
                viewModel.MemberMaxCredit.Equals(8) &&
                viewModel.UsedCredit.Equals(7) &&
                viewModel.GivenCredit.Equals(1) &&
                viewModel.MemberMaxCreditLimit.Equals(">=9 and <=1"));
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        public void ShowMemberMaxCreditLimit_Always_ReturnCorrectValue(byte actorLevel, bool expectedValue)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.Equal(viewModel.ShowMemberMaxCreditLimit, expectedValue);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        public void ShowTransferLink_ActorLevel_ReturnCorrectValue(byte actorLevel, bool expectedShowTransferLink)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildAgentBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.Equal(viewModel.ShowTransferLink, expectedShowTransferLink);
        }
    }
}