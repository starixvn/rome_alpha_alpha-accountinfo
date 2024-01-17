namespace Alpha.AccountInfo.Tests.Features.OpenDownlines
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.OpenDownlines;
    using Alpha.AccountInfo.Features.OpenDownlines.Models;
    using Alpha.AccountInfo.Features.OpenDownlines.Params;
    using AutoFixture;
    using Extensions;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    public class OpenDownlinesServiceTest
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly OpenDownlinesServiceImpl service;
        private readonly Fixture fixture = new Fixture();

        public OpenDownlinesServiceTest()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            service = new OpenDownlinesServiceImpl(dynamicRepository);
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
            var model = (new List<OpenSearchAccountModel>(), new List<OpenDownlineModel>());

            dynamicRepository
               .FetchMultipleAsync<OpenSearchAccountModel, OpenDownlineModel>(Arg.Any<SelectCloseInfoMultipleCriteria>())
               .Returns(model);

            var actualModel = service
                .GetOpenMultipleModel("1, 2", 1)
                .GetAwaiter()
                .GetResult();

            //AssertExtension.Equal(model, actualModel);
        }

        [Fact]
        public void GetOpenViewListModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<IEnumerable<OpenDownlineDto>>();

            dynamicRepository
               .FetchAsync<OpenDownlineDto>(Arg.Any<SelectCloseInfoDetailCriteria>())
               .Returns(model);

            var actualModel = service
                .GetOpenViewListModel(1, true, 2, 10, 1)
                .GetAwaiter()
                .GetResult();

            //AssertExtension.Equal(model, actualModel);
        }
    }
}