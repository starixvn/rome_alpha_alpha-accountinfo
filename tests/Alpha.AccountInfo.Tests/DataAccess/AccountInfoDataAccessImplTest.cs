namespace Alpha.AccountInfo.Tests.DataAccess
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Tests.Database;
    using Alpha.AccountInfo.Tests.Extensions;
    using Alpha.AccountInfo.Tests.Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    [Collection("ObjectDbCollection")]
    public class AccountInfoDataAccessImplTest
    {
        private readonly ObjectDbFixture objectDbFixture;
        private readonly CompareLogic compareLogic;
        private readonly AccountInfoDataAccess accountInfoDataAccessImpl;

        public AccountInfoDataAccessImplTest(ObjectDbFixture objectDbFixture)
        {
            this.objectDbFixture = objectDbFixture;
            compareLogic = new CompareLogic();
            accountInfoDataAccessImpl = new AccountInfoDataAccessImpl();
        }

        [Fact]
        public void GetOnlineList_HasData_ReturnsDataFromObjectDb()
        {
            // Arrange
            var criteria = new SelectOnlineListCriteria(1);

            var dataSet = AccountInfoDatabase.OnlineListData.CreateDataReader();
            objectDbFixture.ObjectDb.ExecuteReader(Arg.Any<Hashtable>()).Returns(dataSet);

            var expectedData = new List<OnlineUserModel>
            {
                new OnlineUserModel("192.168.1.1", new DateTime(2018, 08, 29).ToString("dd MMM hh:mm tt")),
                new OnlineUserModel("192.168.1.2", new DateTime(2018, 08, 30).ToString("dd MMM hh:mm tt")),
            };

            // Act
            var actualData = accountInfoDataAccessImpl.GetOnlineList(criteria);

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }

        [Fact]
        public void GetDownlineList_HasData_ReturnsDataFromObjectDb()
        {
            // Arrange
            var criteria = new SelectDownlineListCriteria(1);

            var dataSet = AccountInfoDatabase.DownlineListData;
            objectDbFixture.ObjectDb.ExecuteDataSet(Arg.Any<Hashtable>()).Returns(dataSet);

            var expectedData = new DownlineListModel(
                    downlines: new List<string>
                    {
                        "User 1",
                        "User 2"
                    },
                    totalAgActive: 1,
                    totalAgClosed: 2,
                    totalAgSuspended: 3,
                    totalMemActive:4,
                    totalMemClosed:5,
                    totalMemSuspended: 6);


            // Act
            var actualData = accountInfoDataAccessImpl.GetDownlineList(criteria);

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }

        [Fact]
        public void GetCustomerAllStatus_HasData_ReturnsDataFromObjectDb()
        {
            // Arrange
            var criteria = new SelectCustomerAllStatusModelCriteria(1);

            var dataSet = AccountInfoDatabase.CustomerAllStatusData;
            objectDbFixture.ObjectDb.ExecuteDataSet(Arg.Any<Hashtable>()).Returns(dataSet);

            var expectedData = new CustomerAllStatusModel(
                 customerStatus: new CustomerStatusModel(
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
                             uplineDisableOutright: true),
                 nonDCSProduct : new NonDCSProductModel(
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
                             hideKeno: true),
                 dcsProducts: new List<DCSProductModel>
                 {
                     new DCSProductModel(
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
                     new DCSProductModel(
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
                    }
                );


            // Act
            var actualData = accountInfoDataAccessImpl.GetCustomerAllStatus(criteria);

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }

        //[Fact]
        //public void ExecuteNonQuery_SuccessCode_ReturnSuccessResult()
        //{
        //    // Arrange
        //    var criteria = new UpdateClosedCriteria(
        //        searchAccountId: 1,
        //        actorId: 2,
        //        subAccountId:0,
        //        closed: true,
        //        language: "E",
        //        ip: "192.168.1.1");


        //    var expectedResult = Result.CreateSuccessResult();
        //    objectDbFixture.ObjectDb.GetOuputParams().Returns(new Hashtable { { "errcode", 0 }, { "ErrMsg", string.Empty } });

        //    // Act
        //    var actualResult = accountInfoDataAccessImpl.ExecuteNonQuery(criteria);

        //    // Assert
        //    AssertExtension.Equal(expectedResult, actualResult);
        //}

        //[Fact]
        //public void ExecuteNonQuery_ErrorCode_ReturnErrorResult()
        //{
        //    // Arrange
        //    var criteria = new UpdateClosedCriteria(
        //        searchAccountId: 1,
        //        actorId: 2,
        //        subAccountId: 0,
        //        closed: true,
        //        language: "E",
        //        ip: "192.168.1.1");

        //    var errorMsg = "Update Failed !";
        //    var expectedResult = Result.CreateErrorResult(100, errorMsg);
        //    objectDbFixture.ObjectDb.GetOuputParams().Returns(new Hashtable { { "errcode", 100 }, { "ErrMsg", errorMsg } });

        //    // Act
        //    var actualResult = accountInfoDataAccessImpl.ExecuteNonQuery(criteria);

        //    // Assert
        //    AssertExtension.Equal(expectedResult, actualResult);
        //}

        [Fact]
        public void UpdateGivenCredit_NonCreditProblem_ReturnSuccessResult()
        {
            // Arrange
            var criteria = new UpdateGivenCreditCriteria(
                searchAccountId: 1,
                roleId: 2,
                creditAmount: 500000,
                actorId: 56,
                actorRoleId: 4,
                subAccountId: 0,
                language: "E",
                ip: "192.168.1.1");

            var expectedResult = Result.CreateSuccessResult();
            objectDbFixture.ObjectDb.GetOuputParams().Returns(new Hashtable { { "CreditProblem", false}, { "ErrMsg", string.Empty } });

            // Act
            var actualResult = accountInfoDataAccessImpl.UpdateGivenCredit(criteria);

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void UpdateGivenCredit_CreditProblem_ReturnFailedResult()
        {
            // Arrange
            var criteria = new UpdateGivenCreditCriteria(
                searchAccountId: 1,
                roleId: 2,
                creditAmount: 500000,
                actorId: 56,
                actorRoleId: 4,
                subAccountId: 0,
                language: "E",
                ip: "192.168.1.1");

            var errMsg = "Credit Problem!";
            var expectedResult = Result.CreateErrorResult(100, errMsg);
            objectDbFixture.ObjectDb.GetOuputParams().Returns(new Hashtable { { "CreditProblem", true }, { "ErrMsg", errMsg } });

            // Act
            var actualResult = accountInfoDataAccessImpl.UpdateGivenCredit(criteria);

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }

    }
}
