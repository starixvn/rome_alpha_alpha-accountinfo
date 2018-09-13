namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class MasterBalanceInfoViewModelTest
    {
        [Fact]
        public void MasterBalanceInfoViewModel_Always_ReturnCorrectValue()
        {
            var viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            var messages = new CustomerInfoQuery(viewer);
            var customerInfo = AccountInfoHelper.BuildCustomerInfo(UserLevel.Master.Value);
            var viewModel = AccountInfoHelper.BuildMasterBalanceInfoViewModel(messages, customerInfo);

            Assert.True(
                viewModel.TotalCash.Equals(13) &&
                viewModel.YesterdayCash.Equals(14) &&
                viewModel.TotalBalance.Equals(47) &&
                viewModel.YesterdayBalance.Equals(50) &&
                viewModel.Outstanding.Equals(15) &&
                viewModel.UsedCredit.Equals(23) &&
                viewModel.GivenCreditLimit.Equals(">=24 and <=85") &&
                viewModel.GivenCredit.Equals(12));
        }
    }
}
