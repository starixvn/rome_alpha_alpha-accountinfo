namespace Alpha.AccountInfo.Tests.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria;
    using Alpha.AccountInfo.Tests.Extensions;
    using Fanex.Data;
    using NSubstitute;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    [Collection("ObjectDbCollection")]
    public class UnsuspendDownlinesDataAccessTest
    {
        private readonly UnsuspendDownlinesDataAccessImpl unsuspendDownlinesDataAccessImpl;
        private readonly IObjectDb objectDb;
        private readonly IMultiResultReader multiResultReader;

        public UnsuspendDownlinesDataAccessTest()
        {
            DbSettingProviderManager
                .StartTestingSession()
                .Run();

            objectDb = Substitute.For<IObjectDb>();
            multiResultReader = Substitute.For<IMultiResultReader>();

            unsuspendDownlinesDataAccessImpl = new UnsuspendDownlinesDataAccessImpl();

            ObjectDbFactory.Mock(
                (key, timeout)
                =>
                objectDb);
        }

        [Fact]
        public void GetDownlinesInfoStatus_HasData_ReturnsModel()
        {
            // Arrange
            var criteria = new SelectSuspendInfoDetailCriteria(1, 1, true, 1, 1);
            objectDb
                .QueryMultipleAsync(Arg.Any<Hashtable>())
                .Returns(Task.FromResult(multiResultReader));

            var outputParams = new Hashtable();
            outputParams.Add("totalrows", 4);
            objectDb.GetOuputParams().Returns(outputParams);

            var downlines = new List<UnsuspendDownlineDto>
            {
                new UnsuspendDownlineDto("test1", 1),
                new UnsuspendDownlineDto("test2", 1),
                new UnsuspendDownlineDto("test3", 2),
                new UnsuspendDownlineDto("test4", 2)
            };

            multiResultReader
                .ReadAsync<UnsuspendDownlineDto>()
                .Returns(Task.FromResult(downlines as IEnumerable<UnsuspendDownlineDto>));

            var levels = new List<UnsuspendDownlineByLevelModel>
            {
                new UnsuspendDownlineByLevelModel(1, new List<UnsuspendDownlineDto>
            {
                new UnsuspendDownlineDto("test1", 1),
                new UnsuspendDownlineDto("test2", 1)
            }),
                 new UnsuspendDownlineByLevelModel(2, new List<UnsuspendDownlineDto>
            {
                 new UnsuspendDownlineDto("test3", 2),
                new UnsuspendDownlineDto("test4", 2)
            })
            };

            var expectedData = new UnsuspendViewListModel(levels, 4);

            // Act
            var actualData = unsuspendDownlinesDataAccessImpl
                                .GetDownlinesInfoStatus(criteria)
                                .GetAwaiter()
                                .GetResult();

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }

        [Fact]
        public void GetUnsuspendMultipleModel_HasData_ReturnsModel()
        {
            // Arrange
            var criteria = new SelectSuspendInfoMultipleCriteria("1, 2", 3);
            objectDb
                .QueryMultipleAsync(Arg.Any<Hashtable>())
                .Returns(Task.FromResult(multiResultReader));

            var searchAccountModel = new List<UnsuspendSearchAccountModel>
            {
                new UnsuspendSearchAccountModel(100, "test1", true),
                new UnsuspendSearchAccountModel(200, "test2", true)
            };

            var downlinesModel = new List<UnsuspendDownlineModel>
            {
                new UnsuspendDownlineModel(100, 1, 10, 10),
                new UnsuspendDownlineModel(100, 2, 10, 10),
                new UnsuspendDownlineModel(200, 1, 10, 10),
                new UnsuspendDownlineModel(200, 2, 10, 10)
            };

            var expectedData = new List<UnsuspendMultipleModel>
            {
                new UnsuspendMultipleModel(
                    100, "test1", true,
                    new List<UnsuspendDownlineModel>
                    {
                        new UnsuspendDownlineModel(100, 2, 10, 10),
                        new UnsuspendDownlineModel(100, 1, 10, 10)
                    } as IEnumerable<UnsuspendDownlineModel>),
                new UnsuspendMultipleModel(
                    200, "test2", true,
                    new List<UnsuspendDownlineModel>
                    {
                        new UnsuspendDownlineModel(200, 2, 10, 10),
                        new UnsuspendDownlineModel(200, 1, 10, 10)
                    }),
            };

            multiResultReader
                .ReadAsync<UnsuspendSearchAccountModel>()
                .Returns(Task.FromResult(searchAccountModel as IEnumerable<UnsuspendSearchAccountModel>));


            multiResultReader
                .ReadAsync<UnsuspendDownlineModel>()
                .Returns(Task.FromResult(downlinesModel as IEnumerable<UnsuspendDownlineModel>));


            // Act
            var actualData = unsuspendDownlinesDataAccessImpl
                                .GetUnsuspendMultipleModel(criteria)
                                .GetAwaiter()
                                .GetResult()
                                .ToList();

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }
    }
}