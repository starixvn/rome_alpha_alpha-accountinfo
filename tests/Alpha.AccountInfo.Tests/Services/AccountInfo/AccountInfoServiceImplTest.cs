namespace Alpha.AccountInfo.Tests.Services.AccountInfo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Tests.Extensions;
    using AutoFixture;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class AccountInfoServiceImplTest
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly SearchAccountDataAccess accountInfoDataAccess;
        private readonly IDictionary<UserLevel, Func<ICriteria, IBalanceInfoModel>> getBalanceInfoModelByLevel;
        private readonly RnGCasinoService rnGCasinoService;
        private readonly SearchAccountServiceImpl accountInfoService;
        private readonly CompareLogic compareLogic = new CompareLogic();
        private readonly Fixture fixture = new Fixture();
        private readonly Task<MemberBalanceInfoModel> memberBalanceInfoModel;
        private readonly Task<AgentBalanceInfoModel> agentBalanceInfoModel;
        private readonly Task<MasterBalanceInfoModel> masterBalanceInfoModel;

        public AccountInfoServiceImplTest()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            accountInfoDataAccess = Substitute.For<SearchAccountDataAccess>();
            rnGCasinoService = Substitute.For<RnGCasinoService>();

            memberBalanceInfoModel = Task.FromResult(fixture.Create<MemberBalanceInfoModel>());
            agentBalanceInfoModel = Task.FromResult(fixture.Create<AgentBalanceInfoModel>());
            masterBalanceInfoModel = Task.FromResult(fixture.Create<MasterBalanceInfoModel>());

            getBalanceInfoModelByLevel = null;
                //new Dictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>>
                //{
                //    {UserLevel.Master, GetMasterBalanceInfoModel},
                //    {UserLevel.Agent, GetAgentBalanceInfoModel},
                //    {UserLevel.Member, GetMemberBalanceInfoModel}
                //};

            accountInfoService =
                new SearchAccountServiceImpl(
                dynamicRepository,
                accountInfoDataAccess,
                rnGCasinoService);
        }

        [Fact]
        public void GetUsernamesSuggestion_Always_ReturnsCorrectModel()
        {
            var model = new List<UsernameSuggestion>
            {
                new UsernameSuggestion("UserName", "FirstName", "LastName")
            };

            dynamicRepository.Fetch<UsernameSuggestion>(Arg.Any<SelectAutoSuggestionCriteria>()).Returns(model);

            var actualModel = accountInfoService.GetUsernamesSuggestion(1, "test", false).GetAwaiter().GetResult();

            AssertExtension.Equal(actualModel, model);
        }

        [Fact]
        public void GetCustomerInfoModel_Always_ReturnsCorrectModel()
        {
            var model = new CustomerInfoModel(
                custId: 1,
                roleId: 1,
                userName: "UserTest",
                userName2: "NickName",
                firstName: "James",
                lastName: "Bond",
                phone: "+896245361",
                mobilePhone: "+963256475",
                fax: "456795312",
                closedReason: 2,
                enabled: true,
                uplineEnabled: true,
                closed: true,
                suspended: true,
                creatDate: DateTime.Now,
                lastLogged: DateTime.Now,
                lastTxn: DateTime.Now,
                lastLoginIP: string.Empty,
                pwdExpiry: DateTime.Now,
                memberId: 11,
                memberName: "MemberTest",
                agentId: 12,
                agentName: "AgentTest",
                masterId: 13,
                masterName: "MasterTest",
                superId: 14,
                superName: "SuperTest");

            dynamicRepository.Get<CustomerInfoModel>(Arg.Any<SelectCustomerInfoCriteria>()).Returns(model);

            var actualModel = accountInfoService.GetCustomerInfoModel("test", 1);

            Assert.True(compareLogic.Compare(actualModel, model).AreEqual);
        }

        [Fact]
        public void GetBalanceInfoModel_Always_ReturnsCorrectModel()
        {
            var actualModel = accountInfoService.GetBalanceInfoModel(1, 1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(actualModel, memberBalanceInfoModel.GetAwaiter().GetResult());
        }

        [Fact]
        public void GetCustomerAllStatusModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<CustomerStatusModel>();
            accountInfoDataAccess.GetCustomerAllStatus(Arg.Any<SelectCustomerAllStatusModelCriteria>()).Returns(model);

            var actualModel = accountInfoService.GetCustomerAllStatusModel(1).GetAwaiter().GetResult();

            AssertExtension.Equal(actualModel, model);
        }

        //[Fact]
        //public void GetOnlineListModel_Always_ReturnsCorrectModel()
        //{
        //    var model = Enumerable.Empty<OnlineUserModel>();
        //    accountInfoDataAccess.GetOnlineList(Arg.Any<SelectDownlineListCriteria>()).Returns(model);

        //     var actualModel = accountInfoService.GetOnlineListModel(1);

        //    AssertExtension.Equal(actualModel, model);
        //}

        [Fact]
        public void GetDownlineListModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<DownlineListModel>();
            accountInfoDataAccess.GetDownlineList(Arg.Any<SelectDownlineListCriteria>()).Returns(model);

            var actualModel = accountInfoService.GetDownlineListModel(1);

            Assert.True(compareLogic.Compare(actualModel, model).AreEqual);
        }

        ////[Fact]
        ////public void UpdateGivenCredit_Always_ReturnsCorrecResult()
        ////{
        ////    var result = Result.CreateSuccessResult();
        ////    var param = fixture.Create<UpdateGivenCreditParam>();

        ////    //dynamicRepository
        ////    //    .Execute(Arg.Any<UpdateGivenCreditCommand>())
        ////    //    .Returns(result);
        ////    dynamicRepository.When(repo => repo.Execute(Arg.))

        ////    var actualResutl = accountInfoService.UpdateGivenCredit(param);

        ////    Assert.True(compareLogic.Compare(actualResutl, result).AreEqual);
        ////}

        //[Fact]
        //public void UpdateCustomerStatus_Always_ReturnsCorrecResult()
        //{
        //    var result = Result.CreateSuccessResult();
        //    var param = fixture.Create<UpdateCustomerStatusParam>();

        //    accountInfoDataAccess.UpdateCustomerStatus(Arg.Any<string>(), Arg.Any<Hashtable>()).Returns(result);

        //    var actualResutl = accountInfoService.UpdateCustomerStatus(param);

        //    Assert.True(compareLogic.Compare(actualResutl, result).AreEqual);
        //}

        //[Fact]
        //public void UpdateCustomerStatus_SuccessAndSyncedCasino_CallSyncedToRnGCasino3rdByStatus()
        //{
        //    var result = Result.CreateSuccessResult();
        //    var param = new UpdateCustomerStatusParam(
        //        searchAccountId: 1,
        //        actorId: 1,
        //        subAccountId: 0,
        //        language: "E",
        //        ip: "192.168.1.1",
        //        statusName: "ouright",
        //        statusValue: true,
        //        actorName: "Actor",
        //        syncedCasino3rd: true);

        //    accountInfoDataAccess.UpdateCustomerStatus(Arg.Any<string>(), Arg.Any<Hashtable>()).Returns(result);

        //    var actualResutl = accountInfoService.UpdateCustomerStatus(param);

        //    rnGCasinoService.Received().SyncedToRnGCasino3rdByStatus(
        //        Arg.Is<UpdateRnGCasino3rdParam>(rngParam =>
        //        rngParam.SearchAccountId.Equals(1) &&
        //        rngParam.StatusName.Equals("ouright") &&
        //        rngParam.StatusValue.Equals(true) &&
        //        rngParam.ActorId.Equals(1) &&
        //        rngParam.ActorName.Equals("Actor") &&
        //        rngParam.IP.Equals("192.168.1.1") ));
        //}

        //[Fact]
        //public void UpdateCustomerStatus_SuccessAndSyncedCasino_DoesNotCallSyncedToRnGCasino3rdByStatus()
        //{
        //    var result = Result.CreateErrorResult(10, string.Empty);
        //    var param = new UpdateCustomerStatusParam(
        //        searchAccountId: 1,
        //        actorId: 1,
        //        subAccountId: 0,
        //        language: "E",
        //        ip: "192.168.1.1",
        //        statusName: "ouright",
        //        statusValue: true,
        //        actorName: "Actor",
        //        syncedCasino3rd: true);

        //    accountInfoDataAccess.UpdateCustomerStatus(Arg.Any<string>(), Arg.Any<Hashtable>()).Returns(result);

        //    var actualResutl = accountInfoService.UpdateCustomerStatus(param);

        //    rnGCasinoService.DidNotReceive().SyncedToRnGCasino3rdByStatus(Arg.Any<UpdateRnGCasino3rdParam>());
        //}

        [Fact]
        public void GetMasterBalanceInfoModel_Always_ReturnsCorrectModel()
        {
            var criteria = new SelectBalanceInfoCriteria(1, 3);

            dynamicRepository
                .GetAsync<MasterBalanceInfoModel>(Arg.Any<SelectBalanceInfoCriteria>())
                .Returns(masterBalanceInfoModel);

            var actualModel = accountInfoService.GetMasterBalanceInfoModel(criteria);

            Assert.True(compareLogic.Compare(actualModel, masterBalanceInfoModel).AreEqual);
        }

        [Fact]
        public void GetAgentBalanceInfoModel_Always_ReturnsCorrectModel()
        {
            var criteria = new SelectBalanceInfoCriteria(1, 2);

            dynamicRepository
                .GetAsync<AgentBalanceInfoModel>(Arg.Any<SelectBalanceInfoCriteria>())
                .Returns(agentBalanceInfoModel);

            var actualModel = accountInfoService.GetAgentBalanceInfoModel(criteria);

            Assert.True(compareLogic.Compare(actualModel, agentBalanceInfoModel).AreEqual);
        }

        [Fact]
        public void GetMemberBalanceInfoModel_Always_ReturnsCorrectModel()
        {
            var criteria = new SelectBalanceInfoCriteria(1, 1);

            dynamicRepository
                .GetAsync<MemberBalanceInfoModel>(Arg.Any<SelectBalanceInfoCriteria>())
                .Returns(memberBalanceInfoModel);

            var actualModel = accountInfoService.GetMemberBalanceInfoModel(criteria);

            Assert.True(compareLogic.Compare(actualModel, memberBalanceInfoModel).AreEqual);
        }

        private async Task<IBalanceInfoModel> GetMemberBalanceInfoModel(ICriteria criteria) => await memberBalanceInfoModel;

        private async Task<IBalanceInfoModel> GetAgentBalanceInfoModel(ICriteria criteria) => await agentBalanceInfoModel;

        private async Task<IBalanceInfoModel> GetMasterBalanceInfoModel(ICriteria criteria) => await masterBalanceInfoModel;
    }
}