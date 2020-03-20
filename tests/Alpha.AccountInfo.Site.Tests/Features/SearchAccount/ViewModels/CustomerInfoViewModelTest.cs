namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class CustomerInfoViewModelTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);

        [Theory]
        [InlineData(4, 3, "MicroSiteUrl/Statement/SMAStatement?CustID=1&CustName=UserTest&CustLevelId=3")]
        [InlineData(4, 2, "MicroSiteUrl/Statement/SMAStatement?CustID=1&CustName=UserTest&CustLevelId=2")]
        [InlineData(4, 1, "MicroSiteUrl/Statement/MemberStatement?CustID=1&CustName=UserTest&CustLevelId=1")]
        [InlineData(3, 2, "MicroSiteUrl/Statement/SMAStatement?CustID=1&CustName=UserTest&CustLevelId=2")]
        [InlineData(3, 1, "MicroSiteUrl/Statement/MemberStatement?CustID=1&CustName=UserTest&CustLevelId=1")]
        [InlineData(2, 1, "MicroSiteUrl/Statement/MemberStatement?CustID=1&CustName=UserTest&CustLevelId=1")]
        public void StatemenLink_Always_ReturnsCorrectStatementLink(
            byte actorRoleId,
            byte searchAccountRoleId,
            string expectedStatementLink)
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(actorRoleId));
            var viewModel = BuildCustomerInfoViewModel(messages, searchAccountRoleId);

            Assert.Equal(viewModel.StatemenLink.Url, expectedStatementLink);
        }

        ////[Theory]
        ////[InlineData(4, 3, "MicroSiteUrl/_MemberInfo/CustomerList/Super/AgentList.aspx?custid=1")]
        ////[InlineData(4, 2, "MicroSiteUrl/_MemberInfo/CustomerList/Super/MemberList.aspx?custid=1")]
        ////[InlineData(3, 2, "MicroSiteUrl/_MemberInfo/CustomerList/Master/MemberList.aspx?custid=1")]
        //////[InlineData(4, 1, "MicroSiteUrl/MemberBetList/Index?CustID=1&CustName=UserTest")]
        //////[InlineData(3, 1, "MicroSiteUrl/MemberBetList/Index?CustID=1&CustName=UserTest")]
        //////[InlineData(2, 1, "MicroSiteUrl/MemberBetList/Index?CustID=1&CustName=UserTest")]
        ////public void CustomerListOrBetListLink_Always_ReturnsCorrectStatementLink(
        ////   byte actorRoleId,
        ////   byte searchAccountRoleId,
        ////   string expectedStatementLink)
        ////{
        ////    var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(actorRoleId));
        ////    var viewModel = BuildCustomerInfoViewModel(messages, searchAccountRoleId);

        ////    Assert.Equal(viewModel.CustomerListOrBetListLink.Url, expectedStatementLink);
        ////}

        [Fact]
        public void ViewLogLink_Always_ReturnsCorrectLink()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedViewLogLink = "MicroSiteUrl/setting/index?username=UserTest";

            var viewModel = BuildCustomerInfoViewModel(messages);

            Assert.Equal(viewModel.ViewLogLink, expectedViewLogLink);
        }

        [Fact]
        public void ViewLoginIPLink_Always_ReturnsCorrectLink()
        {
            var messages = new CustomerInfoQuery(viewer);
            var expectedViewLoginIPLink = "MicroSiteUrl/_IPInfo/IpInfo.aspx";

            var viewModel = BuildCustomerInfoViewModel(messages);

            Assert.Equal(viewModel.ViewLoginIPLink, expectedViewLoginIPLink);
        }

        [Theory]
        [InlineData(4, 3, false)]
        [InlineData(4, 2, false)]
        [InlineData(3, 2, false)]
        [InlineData(4, 1, true)]
        [InlineData(3, 1, true)]
        [InlineData(2, 1, true)]
        public void ShowMember_Always_ReturnsCorrectValue(
           byte actorRoleId,
           byte searchAccountRoleId,
           bool expectedShowMember)
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(actorRoleId));
            var viewModel = BuildCustomerInfoViewModel(messages, searchAccountRoleId);

            Assert.Equal(viewModel.ShowMember, expectedShowMember);
        }

        [Theory]
        [InlineData(4, 3, false)]
        [InlineData(4, 2, true)]
        [InlineData(3, 2, true)]
        [InlineData(4, 1, true)]
        [InlineData(3, 1, true)]
        [InlineData(2, 1, false)]
        public void ShowAgent_Always_ReturnsCorrectValue(
            byte actorRoleId,
            byte searchAccountRoleId,
            bool expectedShowAgent)
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(actorRoleId));
            var viewModel = BuildCustomerInfoViewModel(messages, searchAccountRoleId);

            Assert.Equal(viewModel.ShowAgent, expectedShowAgent);
        }

        [Theory]
        [InlineData(4, 3, true)]
        [InlineData(4, 2, true)]
        [InlineData(4, 1, true)]
        [InlineData(3, 2, false)]
        [InlineData(3, 1, false)]
        [InlineData(2, 1, false)]
        public void ShowMaster_Always_ReturnsCorrectValue(
            byte actorRoleId,
            byte searchAccountRoleId,
            bool expectedShowMaster)
        {
            var messages = new CustomerInfoQuery(AccountInfoHelper.ViewerFixture(actorRoleId));
            var viewModel = BuildCustomerInfoViewModel(messages, searchAccountRoleId);

            Assert.Equal(viewModel.ShowMaster, expectedShowMaster);
        }

        private static CustomerInfoViewModel BuildCustomerInfoViewModel(CustomerInfoQuery messages, byte searchAccountRoleId = 1)
            => new CustomerInfoViewModel(
                messages,
                AccountInfoHelper.BuildCustomerInfo(searchAccountRoleId),
                AccountInfoHelper.BuildMemberBalanceInfoViewModel(messages, AccountInfoHelper.BuildCustomerInfo(), AccountInfoHelper.CreditInfo),
                true,
                AccountInfoHelper.GetMicroSiteUrl);
    }
}