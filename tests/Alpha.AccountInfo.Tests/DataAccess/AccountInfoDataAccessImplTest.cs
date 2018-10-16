namespace Alpha.AccountInfo.Tests.DataAccess
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.DataAccess;
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

        //[Fact]
        //public void GetDownlineList_HasData_ReturnsDataFromObjectDb()
        //{
        //    // Arrange
        //    var criteria = new SelectDownlineListCriteria(1);

        //    var dataSet = AccountInfoDatabase.DownlineListData;
        //    objectDbFixture.ObjectDb.ExecuteDataSet(Arg.Any<Hashtable>()).Returns(dataSet);

        //    var expectedData = new DownlineListModel(
        //            downlines: new List<string>
        //            {
        //                "User 1",
        //                "User 2"
        //            },
        //            totalAgActive: 1,
        //            totalAgClosed: 2,
        //            totalAgSuspended: 3,
        //            totalMemActive:4,
        //            totalMemClosed:5,
        //            totalMemSuspended: 6);

        //    // Act
        //    var actualData = accountInfoDataAccessImpl.GetDownlineList(criteria);

        //    // Assert
        //    AssertExtension.Equal(expectedData, actualData);
        //}

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
                                .GetCustomerAllStatus(criteria)
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
        public void ExecuteNonQuery_SuccessCode_ReturnSuccessResult()
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
            objectDbFixture.ObjectDb.GetOuputParams().Returns(new Hashtable { { "errcode", 0 }, { "ErrMsg", string.Empty } });

            // Act
            var actualResult = accountInfoDataAccessImpl
                .UpdateCustomerStatus(command)
                .GetAwaiter()
                .GetResult();

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }

        //[Fact]
        //public void ExecuteNonQuery_ErrorCode_ReturnErrorResult()
        //{
        //    // Arrange
        //    var criteria = new UpdateCustomerStatusCommand(
        //        searchAccountId: 1,
        //        actorId: 2,
        //        subAccountId: 0,
        //        statusName: StatusMapping.Closed,
        //        language: "E",
        //        ip: "192.168.1.1");

        //    var errorMsg = "Update Failed !";
        //    var expectedResult = Result.CreateErrorResult(100, errorMsg);
        //    objectDbFixture.ObjectDb.GetOuputParams().Returns(new Hashtable { { "errcode", 100 }, { "ErrMsg", errorMsg } });

        //    // Act
        //    var actualResult = accountInfoDataAccessImpl.UpdateCustomerStatus(
        //       criteria.GetSettingKey(),
        //       criteria.GetSpParams());

        //    // Assert
        //    AssertExtension.Equal(expectedResult, actualResult);
        //}

        //[Fact]
        ////public void UpdateGivenCredit_NonCreditProblem_ReturnSuccessResult()
        ////{
        ////    // Arrange
        ////    var criteria = new UpdateGivenCreditCommand(
        ////        searchAccountId: 1,
        ////        searchAccountRoleId: 2,
        ////        creditAmount: 500000,
        ////        actorId: 56,
        ////        subAccountId: 0,
        ////        language: "E",
        ////        ip: "192.168.1.1");

        ////    var expectedResult = Result.CreateSuccessResult();
        ////    objectDbFixture.ObjectDb.GetOuputParams().Returns(new Hashtable { { "CreditProblem", false }, { "ErrMsg", string.Empty } });

        ////    // Act
        ////    var actualResult = accountInfoDataAccessImpl.UpdateGivenCredit(criteria);

        ////    // Assert
        ////    AssertExtension.Equal(expectedResult, actualResult);
        ////}

        ////[Fact]
        ////public void UpdateGivenCredit_CreditProblem_ReturnFailedResult()
        ////{
        ////    // Arrange
        ////    var criteria = new UpdateGivenCreditCommand(
        ////        searchAccountId: 1,
        ////        searchAccountRoleId: 2,
        ////        creditAmount: 500000,
        ////        actorId: 56,
        ////        subAccountId: 0,
        ////        language: "E",
        ////        ip: "192.168.1.1");

        ////    var errMsg = "Credit Problem!";
        ////    var expectedResult = Result.CreateErrorResult(100, errMsg);
        ////    objectDbFixture.ObjectDb.GetOuputParams().Returns(new Hashtable { { "CreditProblem", true }, { "ErrMsg", errMsg } });

        ////    // Act
        ////    var actualResult = accountInfoDataAccessImpl.UpdateGivenCredit(criteria);

        ////    // Assert
        ////    AssertExtension.Equal(expectedResult, actualResult);
        ////}
    }
}