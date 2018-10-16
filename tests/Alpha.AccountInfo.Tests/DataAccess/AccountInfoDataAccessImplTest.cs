namespace Alpha.AccountInfo.Tests.DataAccess
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Tests.Extensions;
    using Alpha.AccountInfo.Tests.Fixtures;
    using Fanex.Data;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    [Collection("ObjectDbCollection")]
    public class AccountInfoDataAccessImplTest
    {
        private readonly ObjectDbFixture objectDbFixture;
        private readonly CompareLogic compareLogic;
        private readonly SearchAccountDataAccess accountInfoDataAccessImpl;
        private readonly IObjectDb objectDb;
        private readonly IMultiResultReader multiResultReader;

        public AccountInfoDataAccessImplTest(ObjectDbFixture objectDbFixture)
        {
            this.objectDbFixture = objectDbFixture;
            objectDb = Substitute.For<IObjectDb>();
            multiResultReader = Substitute.For<IMultiResultReader>();

            compareLogic = new CompareLogic();
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
            var criteria = new SelectDownlineListCriteria(1);

            objectDb
                .QueryMultipleAsync(Arg.Any<Hashtable>())
                .Returns(Task.FromResult(multiResultReader));

            var downlines = new List<DownlineDto>
                    {
                       new DownlineDto("Username1"),
                       new DownlineDto("Username2")
                    };
            var expectedData = new DownlineListModel(
                    downlines,
                    totalAgActive: 1,
                    totalAgClosed: 2,
                    totalAgSuspended: 3,
                    totalMemActive: 4,
                    totalMemClosed: 5,
                    totalMemSuspended: 6);

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
        public void GetCustomerAllStatus_HasData_ReturnsCustomerStatusModel()
        {
            // Arrange
            var criteria = new SelectCustomerAllStatusModelCriteria(1);
            objectDb
                .QueryMultipleAsync(Arg.Any<Hashtable>())
                .Returns(Task.FromResult(multiResultReader));

            var nonProduct = StubNonProductStatus();
            multiResultReader
                .ReadAsync<NonProductStatusModel>()
                .Returns(Task.FromResult(new List<NonProductStatusModel> { nonProduct } as IEnumerable<NonProductStatusModel>));

            var nonDCSProduct = StubNonDCSProduct();
            multiResultReader
                .ReadAsync<NonDCSProductStatusModel>()
                .Returns(Task.FromResult(new List<NonDCSProductStatusModel> { nonDCSProduct } as IEnumerable<NonDCSProductStatusModel>));

            var dcsProducts = StubDcsProducts();
            multiResultReader
               .ReadAsync<DCSProductStatusModel>()
               .Returns(Task.FromResult(dcsProducts as IEnumerable<DCSProductStatusModel>));

            // Act
            var actualData = accountInfoDataAccessImpl
                                .GetCustomerStatus(criteria)
                                .GetAwaiter()
                                .GetResult();

            // Assert
            AssertExtension.Equal(nonProduct, actualData.NonProduct);
            AssertExtension.Equal(nonDCSProduct, actualData.NonDCSProduct);
            AssertExtension.Equal(dcsProducts, actualData.DCSProducts);
        }

        private static NonDCSProductStatusModel StubNonDCSProduct()
            => new NonDCSProductStatusModel(
                             syncCasino: true,
                             disableCasino: true,
                             uplineDisableCS: true,
                             disableNumber: true,
                             uplineDisableNumber: true,
                             disableEsports: true,
                             uplineDisableEsports: true,
                             disableColossusBet: true,
                             hideColossusBet: true,
                             uplineDisableColossusBet: true,
                             hideKeno: true);

        private static NonProductStatusModel StubNonProductStatus()
            => new NonProductStatusModel(
                             hasDownline: true,
                             disableAutoMPT: true,
                             closed: true,
                             uplineClosed: true,
                             suspended: true,
                             uplineSuspend: true,
                             transferOption: "00000000",
                             doubleCommLosing: true,
                             uplineDoubleCommLosing: true,
                             disableOutright: true,
                             uplineDisableOutright: true);

        private static List<DCSProductStatusModel> StubDcsProducts()
            => new List<DCSProductStatusModel>
                 {
                     new DCSProductStatusModel(
                                  productId: 5,
                                  sportType: 12,
                                  betType: 24,
                                  isSync: true,
                                  isHidden: true,
                                  isDisabled: true,
                                  isSyncedUpline: true,
                                  isHiddenUpline: true,
                                  isDisabledUpline: true,
                                  hasDownline: true),
                     new DCSProductStatusModel(
                                  productId: 6,
                                  sportType: 13,
                                  betType: 26,
                                  isSync: true,
                                  isHidden: true,
                                  isDisabled: true,
                                  isSyncedUpline: true,
                                  isHiddenUpline: true,
                                  isDisabledUpline: true,
                                  hasDownline: true)
                    };

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
                ip: "192.168.1.1");

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
                ip: "192.168.1.1");

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
    }
}