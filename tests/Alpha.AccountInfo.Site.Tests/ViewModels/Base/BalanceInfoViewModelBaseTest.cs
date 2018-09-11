namespace Alpha.AccountInfo.Site.Tests.ViewModels.Base
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Alpha.Products.Core.Shared;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo-ViewModel")]
    public class BalanceInfoViewModelBaseTest
    {
        [Fact]
        public void ShowTransferLink_Allow_AllDay_Balance_Transfer_ReturnFalse()
        {
            var viewer = AccountInfoHelper.ViewerFixture(2);
            viewer.AllowedAllDayBalTransfer.Returns(true);
            var messages = new CustomerInfoQuery(viewer);

            var customerInfo = AccountInfoHelper.BuildCustomerInfo();

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo);

            Assert.False(viewModel.ShowTransferLink);
        }

        [Fact]
        public void ShowTransferLink_Does_Not_Allow_Transfer_ReturnFalse()
        {
            var viewer = AccountInfoHelper.ViewerFixture(2);
            viewer.AllowTransfer.Returns(false);
            var messages = new CustomerInfoQuery(viewer);

            var customerInfo = AccountInfoHelper.BuildCustomerInfo();

            var viewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, customerInfo);

            Assert.False(viewModel.ShowTransferLink);
        }

        [Theory]
        [InlineData(4, 3, "Master Outstanding", "MicroSiteUrl/outstanding/Master?CustId=1&CustName=UserTest&CustLevelId=3&BreadcrumbLevelJson=[{'CustId':14,'CustName':'SuperTest','CustLevelId':4}]")]
        [InlineData(4, 2, "Agent Outstanding", "MicroSiteUrl/outstanding/Master?CustId=1&CustName=UserTest&CustLevelId=2&BreadcrumbLevelJson=[{'CustId':14,'CustName':'SuperTest','CustLevelId':4},{'CustId':13,'CustName':'MasterTest','CustLevelId':3}]")]
        [InlineData(4, 1, "Outstanding", "MicroSiteUrl/RunningBetList/Index?CustID=1&amp;CustName=ACGUUS3838033&amp;GetAll=True")]
        [InlineData(3, 2, "Agent Outstanding", "MicroSiteUrl/outstanding/Agent?CustId=1&CustName=UserTest&CustLevelId=2&BreadcrumbLevelJson=[{'CustId':13,'CustName':'MasterTest','CustLevelId':3}]")]
        [InlineData(3, 1, "Outstanding", "MicroSiteUrl/RunningBetList/Index?CustID=1&amp;CustName=ACGUUS3838033&amp;GetAll=True")]
        [InlineData(2, 1, "Outstanding", "MicroSiteUrl/RunningBetList/Index?CustID=1&amp;CustName=ACGUUS3838033&amp;GetAll=True")]
        public void ReportLink_Always_ReturnsCorrectLink(
            byte actorRoleId,
            byte searchAccountRoleId,
            string expectedText,
            string expectedLink)
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(actorRoleId));
            var customerInfo = AccountInfoHelper.BuildCustomerInfo(searchAccountRoleId);
            var viewModel = AccountInfoHelper.BalanceInfoViewModels[Enumeration.FromValue<UserLevel>(searchAccountRoleId)](messages, customerInfo);

            Assert.Equal(viewModel.ReportLink.Text, expectedText);
            Assert.Equal(viewModel.ReportLink.Url, expectedLink);
        }

        //[Theory]
        //[InlineData(4, 3, "MicroSiteUrl/_MemberInfo/CustomerList/Super/AgentList.aspx")]
        //[InlineData(4, 2, "MicroSiteUrl/_MemberInfo/CustomerList/Super/MemberList.aspx")]
        //[InlineData(3, 2, "MicroSiteUrl/_MemberInfo/CustomerList/Master/MemberList.aspx")]
        //[InlineData(4, 1, "MicroSiteUrl/MemberBetList/Index?CustID=1&CustName=UserTest")]
        //[InlineData(3, 1, "MicroSiteUrl/MemberBetList/Index?CustID=1&CustName=UserTest")]
        //[InlineData(2, 1, "MicroSiteUrl/MemberBetList/Index?CustID=1&CustName=UserTest")]
        //public void TransferLink_Always_ReturnsCorrectStatementLink(
        //   byte actorRoleId,
        //   byte searchAccountRoleId,
        //   string expectedStatementLink)
        //{
        //    var messages = new CustomerInfoQuery(StubAccountInfo.ViewerFixture(actorRoleId));
        //    var viewModel = BuildCustomerInfoViewModel(messages, searchAccountRoleId);

        //    Assert.Equal(viewModel.CustomerListOrBetListLink.Url, expectedStatementLink);
        //}
    }
}
