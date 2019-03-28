namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using AccountInfo.Setting.ViewModels.Products;
    using Core.Models;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.ColossusBetsViewModel")]
    public class ColossusBetsViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/customersetting/ColossusBetSetting/ColossusBetSetting/EditSetting";

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"?custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_UserId_Returns_CustId_Of_Actor()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&userid={request.Actor.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_SubAccId_Returns_SubAccId_Of_Actor()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&subAccId={request.Actor.SubAccountId}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Contains_IsMultipleEdit_Paramater()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&isMultipleEdit=", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Return_recommend_Paramater_When_Actor_Is_Super()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&srecommend={request.SearchAccount.Upline.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Return_mrecommend_Paramater_When_Actor_Is_Master()
        {
            var request = RequestHelper.CreateRequest(3, 2);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"mrecommend={request.SearchAccount.Upline.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Return_recommend_Paramater_When_Actor_Is_Agent()
        {
            var request = RequestHelper.CreateRequest(2, 1);
            var viewModel = new ColossusBetsViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"recommend={request.SearchAccount.Upline.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_Visibleness_By_Actor_Product_Hidden_Property(bool hidden, bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(15, hidden: hidden)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new ColossusBetsViewModel(request);

            // Assert
            Assert.Equal(expectedValue, viewModel.Visible);
        }
    }
}