namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
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
            var viewModel = AccountInfoHelper.BuildMasterBalanceInfoViewModel(messages, customerInfo, AccountInfoHelper.CreditInfo);

            Assert.True(
                viewModel.TotalCash.Equals(13) &&
                viewModel.YesterdayCash.Equals(14) &&
                viewModel.TotalBalance.Equals(47) &&
                viewModel.YesterdayBalance.Equals(50) &&
                viewModel.Outstanding.Equals(15) &&
                viewModel.UsedCredit.Equals(4) &&
                viewModel.GivenCredit.Equals(1));
        }
    }
}