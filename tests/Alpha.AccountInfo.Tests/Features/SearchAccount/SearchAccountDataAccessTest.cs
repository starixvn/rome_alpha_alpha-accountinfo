namespace Alpha.AccountInfo.Tests.Features.SearchAccount
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Tests.Extensions;
    using Fanex.Data;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    [Collection("ObjectDbCollection")]
    public class SearchAccountDataAccessTest
    {
        private readonly SearchAccountDataAccess accountInfoDataAccessImpl;
        private readonly IObjectDb objectDb;
        private readonly IMultiResultReader multiResultReader;

        public SearchAccountDataAccessTest()
        {
            DbSettingProviderManager
                .StartTestingSession()
                .Run();

            objectDb = Substitute.For<IObjectDb>();
            multiResultReader = Substitute.For<IMultiResultReader>();

            accountInfoDataAccessImpl = new SearchAccountDataAccessImpl();

            ObjectDbFactory.Mock(
                (key, timeout)
                =>
                objectDb);
        }

        [Fact]
        public void GetDownlineList_HasData_ReturnsDownlineListModel()
        {
            // Arrange
            var criteria = new SelectDownlineListByStatusCriteria(1, 1, 2, 1, 1);

            objectDb
                .QueryMultipleAsync(Arg.Any<Hashtable>())
                .Returns(Task.FromResult(multiResultReader));

            var downlines = new List<DownlineDto>
                    {
                       new DownlineDto("Username1"),
                       new DownlineDto("Username2")
                    };
            var expectedData = new DownlineListModel(downlines, totalPages: 1);

            multiResultReader
                .ReadAsync<DownlineDto>()
                .Returns(Task.FromResult(downlines as IEnumerable<DownlineDto>));
            multiResultReader
                .ReadAsync<DownlineListModel>()
                .Returns(Task.FromResult(new List<DownlineListModel> { expectedData } as IEnumerable<DownlineListModel>));

            // Act
            var actualData = accountInfoDataAccessImpl
                .GetDownlineList(criteria)
                .GetAwaiter()
                .GetResult();

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }

        [Fact]
        public void UpdateCustomerStatus_UpdatedSuccessfully_ReturnSuccessResult()
        {
            // Arrange
            var command = new UpdateCustomerStatusCommand(
                searchAccountId: 1,
                actorId: 2,
                subAccountId: 0,
                statusName: StatusNames.Closed,
                statusValue: true,
                language: "E",
                ip: "192.168.1.1",
                actorName: "abc");

            var expectedResult = Result.CreateSuccessResult();
            objectDb.GetOuputParams().Returns(new Hashtable { { "errcode", 0 }, { "errmsg", string.Empty } });

            // Act
            var actualResult = accountInfoDataAccessImpl
                .UpdateCustomerStatus(command)
                .GetAwaiter()
                .GetResult();

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void UpdateCustomerStatus_UpdateFailed_ReturnErrorResult()
        {
            // Arrange
            var command = new UpdateCustomerStatusCommand(
                searchAccountId: 1,
                actorId: 2,
                subAccountId: 0,
                statusName: StatusNames.Closed,
                statusValue: true,
                language: "E",
                ip: "192.168.1.1",
                actorName: "abc");

            var errorMsg = "Update Failed !";
            var expectedResult = Result.CreateErrorResult(100, errorMsg);
            objectDb.GetOuputParams().Returns(new Hashtable { { "errcode", 100 }, { "errmsg", errorMsg } });

            // Act
            var actualResult = accountInfoDataAccessImpl
                .UpdateCustomerStatus(command)
                .GetAwaiter()
                .GetResult();

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task IsAllDayBalanceTransfer_Always_CallsExecuteReaderAsync()
        {
            // Arrange
            var custId = 1176086;
            var criteria = new SelectIsAllDayBalTransferCriteria(custId);

            // Act
            await accountInfoDataAccessImpl.IsAllDayBalanceTransfer(criteria);

            // Assert
            await objectDb.Received().ExecuteReaderAsync(Arg.Any<Hashtable>());
        }
    }
}