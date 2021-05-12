namespace Alpha.AccountInfo.Tests.Features.OpenDownlines
{
    using Alpha.AccountInfo.Features.OpenDownlines;
    using Alpha.AccountInfo.Features.OpenDownlines.Models;
    using Alpha.AccountInfo.Features.OpenDownlines.Params;
    using Extensions;
    using Fanex.Data;
    using NSubstitute;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    [Collection("ObjectDbCollection")]
    public class OpenDownlinesDataAccessTest
    {
        private readonly OpenDownlinesDataAccessImpl openDownlinesDataAccessImpl;
        private readonly IObjectDb objectDb;
        private readonly IMultiResultReader multiResultReader;

        public OpenDownlinesDataAccessTest()
        {
            DbSettingProviderManager
                .StartTestingSession()
                .Run();

            objectDb = Substitute.For<IObjectDb>();
            multiResultReader = Substitute.For<IMultiResultReader>();

            openDownlinesDataAccessImpl = new OpenDownlinesDataAccessImpl();

            ObjectDbFactory.Mock((key, timeout) => objectDb);
        }

        [Fact]
        public void GetDownlinesInfoStatus_HasData_ReturnsModel()
        {
            // Arrange
            var criteria = new SelectCloseInfoDetailCriteria(1, 1, true, 1, 1);
            objectDb
                .QueryMultipleAsync(Arg.Any<Hashtable>())
                .Returns(Task.FromResult(multiResultReader));

            var outputParams = new Hashtable
            {
                { "totalrows", 4 }
            };
            objectDb.GetOuputParams().Returns(outputParams);

            var downlines = new List<OpenDownlineDto>
            {
                new OpenDownlineDto("test1", 1),
                new OpenDownlineDto("test2", 1),
                new OpenDownlineDto("test3", 2),
                new OpenDownlineDto("test4", 2)
            };

            multiResultReader
                .ReadAsync<OpenDownlineDto>()
                .Returns(Task.FromResult(downlines as IEnumerable<OpenDownlineDto>));

            var levels = new List<OpenDownlineByLevelModel>
            {
                new OpenDownlineByLevelModel(1, new List<OpenDownlineDto>
            {
                new OpenDownlineDto("test1", 1),
                new OpenDownlineDto("test2", 1)
            }),
                 new OpenDownlineByLevelModel(2, new List<OpenDownlineDto>
            {
                 new OpenDownlineDto("test3", 2),
                new OpenDownlineDto("test4", 2)
            })
            };

            var expectedData = new OpenViewListModel(levels, 4);

            // Act
            var actualData = openDownlinesDataAccessImpl
                                .GetDownlinesInfoStatus(criteria)
                                .GetAwaiter()
                                .GetResult();

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }

        [Fact]
        public void GetOpenMultipleModel_HasData_ReturnsModel()
        {
            // Arrange
            var criteria = new SelectCloseInfoMultipleCriteria("1, 2", 3);
            objectDb
                .QueryMultipleAsync(Arg.Any<Hashtable>())
                .Returns(Task.FromResult(multiResultReader));

            var searchAccountModel = new List<OpenSearchAccountModel>
            {
                new OpenSearchAccountModel(100, "test1", true),
                new OpenSearchAccountModel(200, "test2", true)
            };

            var downlinesModel = new List<OpenDownlineModel>
            {
                new OpenDownlineModel(100, 1, 10, 10),
                new OpenDownlineModel(100, 2, 10, 10),
                new OpenDownlineModel(200, 1, 10, 10),
                new OpenDownlineModel(200, 2, 10, 10)
            };

            var expectedData = new List<OpenMultipleModel>
            {
                new OpenMultipleModel(
                    100, "test1", true,
                    new List<OpenDownlineModel>
                    {
                        new OpenDownlineModel(100, 2, 10, 10),
                        new OpenDownlineModel(100, 1, 10, 10)
                    } as IEnumerable<OpenDownlineModel>),
                new OpenMultipleModel(
                    200, "test2", true,
                    new List<OpenDownlineModel>
                    {
                        new OpenDownlineModel(200, 2, 10, 10),
                        new OpenDownlineModel(200, 1, 10, 10)
                    }),
            };

            multiResultReader
                .ReadAsync<OpenSearchAccountModel>()
                .Returns(Task.FromResult(searchAccountModel as IEnumerable<OpenSearchAccountModel>));

            multiResultReader
                .ReadAsync<OpenDownlineModel>()
                .Returns(Task.FromResult(downlinesModel as IEnumerable<OpenDownlineModel>));

            // Act
            var actualData = openDownlinesDataAccessImpl
                                .GetOpenMultipleModel(criteria)
                                .GetAwaiter()
                                .GetResult()
                                .ToList();

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }
    }
}