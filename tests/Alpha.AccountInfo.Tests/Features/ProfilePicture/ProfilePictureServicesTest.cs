namespace Alpha.AccountInfo.Tests.Features.ProfilePicture
{
    using Alpha.AccountInfo.Features.Shared.Helpers;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Models;
    using Alpha.AccountInfo.ProfilePicture.Params;
    using Alpha.AccountInfo.ProfilePicture.Params.SPs.Commands;
    using Alpha.AccountInfo.ProfilePicture.Params.SPs.Criteria;
    using Alpha.AccountInfo.ProfilePicture.Services;
    using Alpha.AccountInfo.Tests.Extensions;
    using AutoFixture;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class ProfilePictureServicesTest
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ExceptionHandlerWrapper exceptionHandlerWrapper;
        private readonly ProfilePictureServicesImpl profilePictureServices;
        private readonly CompareLogic compareLogic = new CompareLogic();
        private readonly Fixture fixture = new Fixture();

        public ProfilePictureServicesTest()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            exceptionHandlerWrapper = Substitute.For<ExceptionHandlerWrapper>();
            profilePictureServices = new ProfilePictureServicesImpl( dynamicRepository, exceptionHandlerWrapper);
        }

        [Fact]
        public void GetProfilePictureLibrary_Always_ReturnsCorrectModel()
        {
            var model = new List<ProfilePictureDTO>
            {
                new ProfilePictureDTO(1, new byte[5]),
                new ProfilePictureDTO(2, new byte[6])
            };

            var result = new List<ProfilePictureModel>
            {
                new ProfilePictureModel(1, new byte[5]),
                new ProfilePictureModel(2, new byte[6])
            };

            dynamicRepository
                .FetchAsync<ProfilePictureDTO>(Arg.Any<SelectProfilePictureLibraryCriteria>())
                .Returns(Task.FromResult(model as IEnumerable<ProfilePictureDTO>));

            var actualResult = profilePictureServices
                .FectchProfilePictureLibraryAsync(1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(actualResult, result);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "DB errore")]
        public void UpdateActorProfilePictureAsync_Always_ReturnsCorrecResult(int code, string message)
        {
            var result = new Result(code, message);
            var param = fixture.Create<UpdateActorProfilePictureParam>();
            dynamicRepository
                .When(repo => repo.ExecuteAsync(Arg.Any<UpdateActorProfilePictureMainServerCommand>()))
               .Do((callInfo) =>
               {
                   var command = callInfo[0] as UpdateActorProfilePictureMainServerCommand;
                   command.ErrCode = result.Code;
                   command.ErrMsg = result.Message;
               });

            var actualResutl = profilePictureServices
                .UpdateActorProfilePictureMainServerAsync(param)
                .GetAwaiter()
                .GetResult();

            Assert.True(compareLogic.Compare(actualResutl, result).AreEqual);
        }
    }
}