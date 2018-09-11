namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo-ViewModel")]
    public class MasterBalanceInfoViewModelTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(4);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo(3);

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

        [Fact]
        public void TransferLinkOnYesterdayBalance_YesterdayBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_False_ReturnsCorrectLink()
        {
            var viewer = AccountInfoHelper.ViewerFixture(4);
            viewer.AllowedTotalBalanceTransfer.Returns(false);
            var messages = new CustomerInfoQuery(viewer);
            
            var expectedTransferLink = "MicroSiteUrl/_Transfer/Super/MasterTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=50";

            var viewModel = AccountInfoHelper.BuildMasterBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.YesterdayBalanceTransferLink, expectedTransferLink);
        }

        [Fact]
        public void TransferLinkOnTotalBalance_TotaBalanceGreaterThanZero_And_ShowTransferLink_Is_True_And_AllowedTotalBalanceTransfer_Is_True_ReturnsCorrectLink()
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(4));
            var expectedTransferLink = "MicroSiteUrl/_Transfer/Super/MasterTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=47";

            var viewModel = AccountInfoHelper.BuildMasterBalanceInfoViewModel(messages, customerInfo);

            Assert.Equal(viewModel.TotalBalanceTransferLink, expectedTransferLink);
        }
    }
}
