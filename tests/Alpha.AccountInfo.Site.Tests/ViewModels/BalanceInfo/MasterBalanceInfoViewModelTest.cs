namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class MasterBalanceInfoViewModelTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo(UserLevel.Master.Value);

        [Fact]
        public void TotalBalance_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedTotalBalance = 47;

            var viewModel = AccountInfoHelper.BuildMasterBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.TotalBalance, expectedTotalBalance);
        }

        [Fact]
        public void YesterdayBalance_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedYesterdayBalance = 50;

            var viewModel = AccountInfoHelper.BuildMasterBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.YesterdayBalance, expectedYesterdayBalance);
        }

        [Fact]
        public void GivenCreditLimit_Always_ReturnCorrectValue()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedGivenCreditLimit = ">=24 and <=85";

            var viewModel = AccountInfoHelper.BuildMasterBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.GivenCreditLimit, expectedGivenCreditLimit);
        }
    }
}
