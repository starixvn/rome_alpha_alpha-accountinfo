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
        private readonly IUser viewer = BalanceInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly CustomerInfoModel customerInfo = BalanceInfoHelper.BuildCustomerInfo();

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, false)]
        [InlineData(2, true)]
        public void ShowTransferLink_ActorLevel_ReturnCorrectValue(byte actorLevel, bool expectedShowTransferLink)
        {
            viewer.LevelId.Returns(actorLevel);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = BalanceInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.ShowTransferLink, expectedShowTransferLink);
        }

        [Fact]
        public void BetCredit_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedBetCredit = 29;

            var viewModel = BalanceInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.BetCredit, expectedBetCredit);
        }
    }
}
