namespace Alpha.AccountInfo.Tests.Features.OpenDownlines
{
    using Alpha.AccountInfo.Features.OpenDownlines;
    using Alpha.AccountInfo.Features.OpenDownlines.Models;
    using Alpha.AccountInfo.Features.OpenDownlines.Params;
    using AutoFixture;
    using Extensions;
    using Fanex.Data.Repository;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    public class OpenDownlinesServiceTest
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly OpenDownlinesDataAccess access;
        private readonly OpenDownlinesServiceImpl service;
        private readonly Fixture fixture = new Fixture();

        public OpenDownlinesServiceTest()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            access = Substitute.For<OpenDownlinesDataAccess>();
            service = new OpenDownlinesServiceImpl(dynamicRepository, access);
        }

        [Fact]
        public void GetOpenInfoModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<IEnumerable<OpenSingleModel>>();

            dynamicRepository
               .FetchAsync<OpenSingleModel>(Arg.Any<SelectCloseInfoCriteria>())
               .Returns(Task.FromResult(model));

            var actualModel = service
                .GetOpenInfoModel(1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }

        [Fact]
        public void GetOpenMultipleModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<IEnumerable<OpenMultipleModel>>();

            access
               .GetOpenMultipleModel(Arg.Any<SelectCloseInfoMultipleCriteria>())
               .Returns(Task.FromResult(model));

            var actualModel = service
                .GetOpenMultipleModel("1, 2", 1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }

        [Fact]
        public void GetOpenViewListModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<OpenViewListModel>();

            access
               .GetDownlinesInfoStatus(Arg.Any<SelectCloseInfoDetailCriteria>())
               .Returns(Task.FromResult(model));

            var actualModel = service
                .GetOpenViewListModel(1, true, 2, 10, 1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }
    }
}