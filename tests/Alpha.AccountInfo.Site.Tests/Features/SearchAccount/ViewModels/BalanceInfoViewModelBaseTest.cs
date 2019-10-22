namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Alpha.Products.Core.Shared;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class BalanceInfoViewModelBaseTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo();
        private readonly CreditInfo creditInfo = AccountInfoHelper.CreditInfo;

        [Fact]
        public void ShowTransferLink_Allow_AllDay_Balance_Transfer_ReturnFalse()
        {
            viewer.AllowedAllDayBalTransfer.Returns(true);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.False(viewModel.ShowTransferLink);
        }

        [Fact]
        public void ShowTransferLink_Does_Not_Allow_Transfer_ReturnFalse()
        {
            viewer.AllowTransfer.Returns(false);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.False(viewModel.ShowTransferLink);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void ShowTotalBalanceTransferLink_AllowedTotalBalanceTransfer_ReturnCorrectValue(
            bool allowedTotalBalanceTransfer,
            bool expectedValue)
        {
            viewer.AllowedTotalBalanceTransfer.Returns(allowedTotalBalanceTransfer);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.Equal(viewModel.ShowTotalBalanceTransferLink, expectedValue);
        }

        [Theory]
        [InlineData(20, true)]
        [InlineData(-16, false)]
        [InlineData(-20, true)]
        public void ShowTotalBalanceTransferLink_TotalBalance_ReturnCorrectValue(
         decimal balance,
         bool expectedValue)
        {
            var memberBalanceInfoDTO = new MemberBalanceInfoDTO(1, 12, 15, 20);
            var balanceInfo = new MemberBalanceInfoModel(memberBalanceInfoDTO, balance, 16, 19);

            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, creditInfo, balanceInfo);

            Assert.Equal(viewModel.ShowTotalBalanceTransferLink, expectedValue);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void ShowYesterdayBalanceTransferLink_AllowedTotalBalanceTransfer_ReturnCorrectValue(
         bool allowedTotalBalanceTransfer,
         bool expectedValue)
        {
            viewer.AllowedTotalBalanceTransfer.Returns(allowedTotalBalanceTransfer);
            var messages = new CustomerInfoQuery(viewer);

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo, creditInfo);

            Assert.Equal(viewModel.ShowYesterdayBalanceTransferLink, expectedValue);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(0, false)]
        [InlineData(-10, true)]
        public void ShowYesterdayBalanceTransferLink_YesterdayBalance_ReturnCorrectValue(
         decimal yesterday_CustomerCash,
         bool expectedValue)
        {
            viewer.AllowedTotalBalanceTransfer.Returns(false);
            var messages = new CustomerInfoQuery(viewer);

            var memberBalanceInfoDTO = new MemberBalanceInfoDTO(1, 12, yesterday_CustomerCash, 20);
            var balanceInfo = new MemberBalanceInfoModel(memberBalanceInfoDTO, 20, 16, 19);

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, creditInfo, balanceInfo);

            Assert.Equal(viewModel.ShowYesterdayBalanceTransferLink, expectedValue);
        }

        [Theory]
        [InlineData(4, 3, "Master Outstanding", "MicroSiteUrl/outstanding/Master?CustId=1&CustName=UserTest&CustLevelId=3&BreadcrumbLevelJson=[{'CustId':14,'CustName':'SuperTest','CustLevelId':4}]")]
        [InlineData(4, 2, "Agent Outstanding", "MicroSiteUrl/outstanding/Master?CustId=1&CustName=UserTest&CustLevelId=2&BreadcrumbLevelJson=[{'CustId':14,'CustName':'SuperTest','CustLevelId':4},{'CustId':13,'CustName':'MasterTest','CustLevelId':3}]")]
        [InlineData(4, 1, "Outstanding", "MicroSiteUrl/RunningBetList/Index?CustID=1&amp;CustName=UserTest&amp;GetAll=True")]
        [InlineData(3, 2, "Agent Outstanding", "MicroSiteUrl/outstanding/Agent?CustId=1&CustName=UserTest&CustLevelId=2&BreadcrumbLevelJson=[{'CustId':13,'CustName':'MasterTest','CustLevelId':3}]")]
        [InlineData(3, 1, "Outstanding", "MicroSiteUrl/RunningBetList/Index?CustID=1&amp;CustName=UserTest&amp;GetAll=True")]
        [InlineData(2, 1, "Outstanding", "MicroSiteUrl/RunningBetList/Index?CustID=1&amp;CustName=UserTest&amp;GetAll=True")]
        public void ReportLink_Always_ReturnsCorrectLink(
            byte actorRoleId,
            byte searchAccountRoleId,
            string expectedText,
            string expectedLink)
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(actorRoleId));
            var customer = AccountInfoHelper.BuildCustomerInfo(searchAccountRoleId);
            var viewModel = AccountInfoHelper.BalanceInfoViewModels[Enumeration.FromValue<UserLevel>(searchAccountRoleId)](messages, customer, AccountInfoHelper.CreditInfo);

            Assert.True(
                viewModel.ReportLink.Url.Equals(expectedLink) &&
                viewModel.ReportLink.Text.Equals(expectedText));
        }

        [Theory]
        [InlineData(4, 3, "MicroSiteUrl/_Transfer/Super/MasterTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=30")]
        [InlineData(3, 2, "MicroSiteUrl/_Transfer/Master/AgentTransfer.aspx?custid=1&status=1&CallBack=false&username=UserTest&isyesterday=1&amt=30")]
        [InlineData(2, 1, "MicroSiteUrl/_Transfer/Agent/MemberTransfer.aspx?custid=1&CallBack=false")]
        public void BuildTransferLink_Always_ReturnsCorrectStatementLink(
           byte actorRoleId,
           byte searchAccountRoleId,
           string expectedStatementLink)
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(actorRoleId));
            var customer = AccountInfoHelper.BuildCustomerInfo(searchAccountRoleId);
            var transferValue = 30;

            var viewModel = AccountInfoHelper.BalanceInfoViewModels[Enumeration.FromValue<UserLevel>(searchAccountRoleId)](messages, customer, AccountInfoHelper.CreditInfo);
            var actualLink = viewModel.BuildTransferLink(transferValue);

            Assert.Equal(actualLink, expectedStatementLink);
        }
    }
}