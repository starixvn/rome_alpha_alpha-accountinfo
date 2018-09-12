
namespace Alpha.AccountInfo.Site.Tests.Handlers
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Handlers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.Extensions;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using Alpha.Products.Core.Services;
    using NSubstitute;
    using System;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "AccountInfoHandler")]
    public class AccountInfoHandlerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly AccountInfoService accountInfoService;
        private readonly AccountInfoHandler accountInfoHandler;

        public AccountInfoHandlerTest()
        {
            accountInfoService = Substitute.For<AccountInfoService>();
            accountInfoHandler = new AccountInfoHandler(accountInfoService);

            SimpleServiceLocator
             .Instance
             .Register<Func<string, string, string>>((microSiteName, virtualPath) => "MicroSiteUrl/")
             .Register<Func<string, ResourceManager, string>>((language, resources) => "{}");
        }

        [Fact]
        public async Task HandleAccountInfoQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new AccountInfoQuery(viewer);
            var expectedViewModel = new AccountInfoViewModel(message);

            var actualViewModel = await accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleUserNameSuggestionQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new UserNameSuggestionQuery(viewer)
            {
                SuggestionQuery = "test"
            };

            var expectedViewModel = Enumerable.Empty<UsernameSuggestion>();
            accountInfoService.GetUsernamesSuggestion(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(expectedViewModel);

            var actualViewModel = accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        //[Fact]
        //public void HandleDownlineListQuery_Always_ReturnsCorrectViewModel()
        //{
        //    var message = new DownlineListQuery(viewer)
        //    {
        //        SearchAccountId = 1
        //    };

        //    var expectedViewModel = Enumerable.Empty<UsernameSuggestion>();
        //    accountInfoService.GetUsernamesSuggestion(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(expectedViewModel);

        //    var actualViewModel = accountInfoHandler.Handle(message);

        //    AssertExtension.Equal(actualViewModel, expectedViewModel);
        //}
    }
}
