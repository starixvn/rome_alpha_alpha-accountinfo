namespace Alpha.AccountInfo.Tests.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
    using Alpha.AccountInfo.Tests.Extensions;
    using AutoFixture;
    using Fanex.Data.Repository;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class UnsuspendDownlinesServiceTest
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly UnsuspendDownlinesDataAccess access;
        private readonly UnsuspendDownlinesServiceImpl service;
        private readonly Fixture fixture = new Fixture();

        public UnsuspendDownlinesServiceTest()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            access = Substitute.For<UnsuspendDownlinesDataAccess>();
            service = new UnsuspendDownlinesServiceImpl(dynamicRepository, access);
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

            access
               .GetUnsuspendMultipleModel(Arg.Any<SelectSuspendInfoMultipleCriteria>())
               .Returns(Task.FromResult(model));

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

            access
               .GetDownlinesInfoStatus(Arg.Any<SelectSuspendInfoDetailCriteria>())
               .Returns(Task.FromResult(model));

            var actualModel = service
                .GetUnsuspendViewListModel(1, true, 2, 10, 1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }

        [Theory]
        [InlineData(1, true, true, true)]
        [InlineData(1, true, false, true)]
        [InlineData(1, false, true, true)]
        [InlineData(1, false, false, false)]
        [InlineData(0, true, true, false)]
        [InlineData(0, false, true, false)]
        [InlineData(0, true, false, false)]
        [InlineData(0, false, false, false)]
        public void CheckMultipleCustomesBelonging_Always_ReturnsCorrectResult(
            byte roleId, 
            bool isInternal, 
            bool isActive,
            bool expectedResult)
        {
            dynamicRepository
               .When(repo => repo.ExecuteAsync(Arg.Any<CheckMultipleCustomesBelongingCriteria>()))
              .Do((callInfo) =>
              {
                  var command = callInfo[0] as CheckMultipleCustomesBelongingCriteria;
                  command.RoleId = roleId;
                  command.IsActive = isActive;
              });

            var actualResutl = service
                .CheckMultipleCustomesBelonging(roleId, 0, "12,13", isInternal)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(actualResutl, expectedResult);
        }
    }
}