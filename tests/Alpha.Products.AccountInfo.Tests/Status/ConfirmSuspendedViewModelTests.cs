namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmSuspendedViewModel")]
    public class ConfirmSuspendedViewModelTests
    {
        [Fact]
        public void Disabled_Is_True_When_SearchAccount_Upline_Is_Suspended()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsSuspended", true);
            request.AddData("SearchAccount.Upline.IsClosed", false);

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

        [Theory]
        [InlineData(true, 1, "InCaseUnsuspendedMemberMessage")]
        [InlineData(false, 1, "InCaseSuspendedMemberMessage")]
        [InlineData(true, 2, "InCaseUnsuspendedMasterOrAgentMessage")]
        [InlineData(false, 2, "InCaseSuspendedMasterOrAgentMessage")]
        [InlineData(true, 3, "InCaseUnsuspendedMasterOrAgentMessage")]
        [InlineData(false, 3, "InCaseSuspendedMasterOrAgentMessage")]
        public void Return_CorrectSuffixMessage_By_Checked(bool isSuspended, byte searchAccountRoleId, string expectedSuffixMessages)
        {
            var request = RequestHelper.CreateRequest(4, searchAccountRoleId);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsSuspended", false);
            request.AddData("SearchAccount.IsSuspended", isSuspended);

            var viewModel = new ConfirmSuspendedViewModel(request);

            Assert.Equal(expectedSuffixMessages, viewModel.SuffixMessage);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_CorrectStatusValue_By_Checked(bool isSuspended, bool expectedStatusValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsSuspended", false);
            request.AddData("SearchAccount.IsSuspended", isSuspended);

            var viewModel = new ConfirmSuspendedViewModel(request);

            Assert.Equal(expectedStatusValue, viewModel.StatusValue);
        }
    }
}