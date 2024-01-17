namespace Alpha.AccountInfo.Tests.Features.UnsuspendDownlines
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
    using Alpha.AccountInfo.Tests.Extensions;
    using AutoFixture;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class UnsuspendDownlinesServiceTest
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly UnsuspendDownlinesServiceImpl service;
        private readonly Fixture fixture = new Fixture();

        public UnsuspendDownlinesServiceTest()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            service = new UnsuspendDownlinesServiceImpl(dynamicRepository);
        }

        [Fact]
        public void GetUnsuspendInfoModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<IEnumerable<UnsuspendSingleModel>>();

            dynamicRepository
               .FetchAsync<UnsuspendSingleModel>(Arg.Any<SelectSuspendInfoCriteria>())
               .Returns(Task.FromResult(model));

            var actualModel = service
                .GetUnsuspendInfoModel(1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }

        [Fact]
        public void GetUnsuspendMultipleModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<IEnumerable<UnsuspendMultipleModel>>();

            //access
            //   .GetUnsuspendMultipleModel(Arg.Any<SelectSuspendInfoMultipleCriteria>())
            //   .Returns(Task.FromResult(model));

            var actualModel = service
                .GetUnsuspendMultipleModel("1, 2", 1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }

        [Fact]
        public void GetUnsuspendViewListModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<UnsuspendViewListModel>();

            //access
            //   .GetDownlinesInfoStatus(Arg.Any<SelectSuspendInfoDetailCriteria>())
            //   .Returns(Task.FromResult(model));

            var actualModel = service
                .GetUnsuspendViewListModel(1, true, 2, 10, 1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }
    }
}