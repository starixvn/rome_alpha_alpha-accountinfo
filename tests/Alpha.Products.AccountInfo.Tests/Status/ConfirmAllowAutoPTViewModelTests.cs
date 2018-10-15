namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System;
    using Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmAllowAutoPTViewModel")]
    public class ConfirmAllowAutoPTViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/AccountInfo/UpdateCustomerStatus";

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Visible_Is_True_When_Actor_Is_Super_And_SearchAccount_Is_Master()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.True(viewModel.Visible);
        }

        [Theory]
        [InlineData(4, 2)]
        [InlineData(4, 1)]
        [InlineData(3, 2)]
        [InlineData(3, 1)]
        [InlineData(2, 1)]
        public void Visible_Is_False_When_Actor_Is_Not_Super_Or_SearchAccount_Is_Not_Master(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.False(viewModel.Visible);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_Disabled_By_Actor_HasUpdateAccountPermission(bool hasUpdateAccountPermission, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", hasUpdateAccountPermission);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal(expectedValue, viewModel.Disabled);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Checked_By_SearchAccount_IsAllowedMasterAutoPT(bool isAllowedMasterAutoPT, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedMasterAutoPT", isAllowedMasterAutoPT);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal(expectedValue, viewModel.Checked);
        }
    }
}