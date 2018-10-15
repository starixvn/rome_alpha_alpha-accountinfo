namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmDoubleCommViewModel")]
    public class ConfirmDoubleCommViewModelTests
    {
        [Fact]
        public void Disabled_Is_True_When_Actor_Does_Not_HasUpdateAccountPermission()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", false);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Fact]
        public void Disabled_Is_True_When_SearchAccount_Upline_Does_Not_Allow_DoubleComm()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", false);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Disabled_Is_True_When_SearchAccount_Is_Not_DirectDownline_Of_Actor(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Theory]
        [ClassData(typeof(ActorAndDirectDownlineSearchAccountLevelTestData))]
        public void Disabled_Is_False_When_Actor_HasUpdateAccountPermission_And_Edit_DirectDownline_And_SearchAccount_Upline_Allow_DoubleComm(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.False(viewModel.Disabled);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Checked_By_SearchAccount_IsAllowedDoubleComm(bool isAllowedDoubleComm, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedDoubleComm", isAllowedDoubleComm);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.Equal(expectedValue, viewModel.Checked);
        }
    }
}