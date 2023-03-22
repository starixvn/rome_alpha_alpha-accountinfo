namespace Alpha.AccountInfo.Site.Tests.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Models;
    using Alpha.AccountInfo.ProfilePicture.Params;
    using Alpha.AccountInfo.ProfilePicture.Services;
    using Alpha.AccountInfo.Site.Features.ProfilePicture;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.AccountInfo.Site.Tests.Extensions;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Alpha.Products.Core.Services;
    using Alpha.SecurityCode.Services;
    using AutoFixture;
    using JW;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class ProfilePictureHandlerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly ProfilePictureServices profilePictureServices;
        private readonly ProfilePictureHandler handler;
        private readonly IEnumerable<ProfilePictureModel> model;
        private readonly SecurityCodeChecker securityCodeChecker;
        public ProfilePictureHandlerTest()
        {
            SimpleServiceLocator
                .Instance
                .Register<Func<int>>(() => 1)
                .Register<Func<string, ResourceManager, string>>((language, resources) => "{}")
                .Register<Func<string, string, string>>((microSiteName, virtualPath) => $"microSiteName/");

            profilePictureServices = Substitute.For<ProfilePictureServices>();
            securityCodeChecker= Substitute.For<SecurityCodeChecker>();
            handler = new ProfilePictureHandler(securityCodeChecker,profilePictureServices);

            model = new List<ProfilePictureModel>()
            {
                new ProfilePictureModel(1, null),
                new ProfilePictureModel(2, null),
                new ProfilePictureModel(3, null),
                new ProfilePictureModel(4, null),
                new ProfilePictureModel(5, null),
                new ProfilePictureModel(6, null),
                new ProfilePictureModel(7, null)
            };

            profilePictureServices
             .FectchProfilePictureLibraryAsync(Arg.Any<int>())
             .Returns(Task.FromResult(model));
        }

        [Fact]
        public void HandleProfilePictureQuery_Always_ReturnsCorrectViewModel()
        {
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SiteGroup"] = "MBCBet"
            };
            var message = new ProfilePictureQuery(viewer);

            var expectedViewModel = new ProfielPictureViewModel(
                model,
                message.ActorRoleId, 
                message.Language,
                message.AllowUpdate,
                securityCodeChecker.IsEnableSecurityCode(),
                message.IsOfferProtectedMode,
                "en-US",
                Enumerable.Empty<KeyValuePair<string, string>>(),
                allowUpdateDisplayLanguage: true);

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(actualViewModel.Images.OrderBy(image => image.ImageId), expectedViewModel.Images);
            AssertExtension.Equal(actualViewModel.ActorLevel, expectedViewModel.ActorLevel);
            AssertExtension.Equal(actualViewModel.AllowUpdate, expectedViewModel.AllowUpdate);
            AssertExtension.Equal(actualViewModel.Resources, expectedViewModel.Resources);
            AssertExtension.Equal(actualViewModel.SetUpOTPLink, expectedViewModel.SetUpOTPLink);
            AssertExtension.Equal(actualViewModel.DisplayLanguage, expectedViewModel.DisplayLanguage);
        }

        [Fact]
        public void HandleProfilePictureLibraryQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new ProfilePictureLibraryQuery(viewer)
            {
                CurrentPage = 1
            };

            var pager = new Pager(totalItems: model.Count(), currentPage: message.CurrentPage);

            var expectedViewModel = new ProfielPictureLibraryViewModel(model, pager);

            var actualViewModel = 
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleUpdateActorProfilePictureRequest_Always_ReturnsCorrectViewModel()
        {
            var message = new UpdateActorProfilePictureRequest(viewer)
            {
                ImageId = 10
            };

            var result = Result.CreateSuccessResult();

            var expectedViewModel = result;

            profilePictureServices
             .UpdateActorProfilePictureMainServerAsync(Arg.Any<UpdateActorProfilePictureParam>())
             .Returns(result);

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }
    }
}