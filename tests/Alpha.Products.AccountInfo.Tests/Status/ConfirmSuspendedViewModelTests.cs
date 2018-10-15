namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmSuspendedViewModel")]
    public class ConfirmSuspendedViewModelTests
    {

        [Fact]
        public void Disabled_Is_True_When_Actor_Does_Not_HasUpdateAccountPermission()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", false);

            var viewModel = new ConfirmSuspendedViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Fact]
        public void Disabled_Is_True_When_SearchAccount_Upline_Is_Suspended()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsSuspended", true);

            var viewModel = new ConfirmSuspendedViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Checked_By_SearchAccount_IsSuspended(bool isSuspended, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsSuspended", false);
            request.AddData("SearchAccount.IsSuspended", isSuspended);

            var viewModel = new ConfirmSuspendedViewModel(request);

            Assert.Equal(expectedValue, viewModel.Checked);
        }
    }
}