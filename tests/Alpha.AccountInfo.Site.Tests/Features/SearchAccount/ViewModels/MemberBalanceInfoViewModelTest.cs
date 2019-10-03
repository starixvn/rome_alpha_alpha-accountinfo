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
    public class MemberBalanceInfoViewModelTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo();
        private readonly CreditInfo creditInfo = AccountInfoHelper.CreditInfo;

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, false)]
        [InlineData(2, true)]
        public void ShowTransferLink_ActorLevel_ReturnCorrectValue(byte actorLevel, bool expectedShowTransferLink)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.Equal(viewModel.ShowTransferLink, expectedShowTransferLink);
        }

        [Fact]
        public void MemberBalanceInfoViewModel_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.True(
                viewModel.TotalBalance.Equals(36) &&
                viewModel.YesterdayBalance.Equals(15) &&
                viewModel.Outstanding.Equals(16) &&
                viewModel.GivenCredit.Equals(1) &&
                viewModel.BetCredit.Equals(21));
        }
    }
}