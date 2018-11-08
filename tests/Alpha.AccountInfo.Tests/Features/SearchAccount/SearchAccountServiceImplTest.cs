namespace Alpha.AccountInfo.Tests.Features.SearchAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    public class SearchAccountServiceImplTest
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly SearchAccountDataAccess accountInfoDataAccess;
        private readonly IDictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>> getBalanceInfoModelByLevel;
        private readonly RnGCasinoService rnGCasinoService;
        private readonly SearchAccountServiceImpl accountInfoService;
        private readonly CompareLogic compareLogic = new CompareLogic();
        private readonly Fixture fixture = new Fixture();
        private readonly MemberBalanceInfoModel memberBalanceInfoModel;
        private readonly AgentBalanceInfoModel agentBalanceInfoModel;
        private readonly MasterBalanceInfoModel masterBalanceInfoModel;

        public SearchAccountServiceImplTest()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            accountInfoDataAccess = Substitute.For<SearchAccountDataAccess>();
            rnGCasinoService = Substitute.For<RnGCasinoService>();

            memberBalanceInfoModel = fixture.Create<MemberBalanceInfoModel>();
            agentBalanceInfoModel = fixture.Create<AgentBalanceInfoModel>();
            masterBalanceInfoModel = fixture.Create<MasterBalanceInfoModel>();

            getBalanceInfoModelByLevel = new Dictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>>
            {
                {UserLevel.Master, GetMasterBalanceInfoModel},
                {UserLevel.Agent, GetAgentBalanceInfoModel},
                {UserLevel.Member, GetMemberBalanceInfoModel}
            };

            accountInfoService =
                new SearchAccountServiceImpl(
                dynamicRepository,
                accountInfoDataAccess,
                rnGCasinoService,
                getBalanceInfoModelByLevel);
        }

        [Fact]
        public void GetUsernamesSuggestion_Always_ReturnsCorrectModel()
        {
            var model = new List<UsernameSuggestion>
            {
                new UsernameSuggestion("UserName", "FirstName", "LastName")
            };

            dynamicRepository
                .FetchAsync<UsernameSuggestion>(Arg.Any<SelectAutoSuggestionCriteria>())
                .Returns(Task.FromResult(model as IEnumerable<UsernameSuggestion>));

            var actualModel = accountInfoService
                .GetUsernamesSuggestion(1, "test", false)
                .GetAwaiter()
                .GetResult();

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

            dynamicRepository
                .GetAsync<CustomerInfoModel>(Arg.Any<SelectCustomerInfoCriteria>())
                .Returns(Task.FromResult(model));

            var actualModel = accountInfoService
                .GetCustomerInfo("test", 1)
                .GetAwaiter()
                .GetResult();

            Assert.True(compareLogic.Compare(actualModel, model).AreEqual);
        }

        [Fact]
        public void GetBalanceInfoModel_GetMember_ReturnsCorrectModel()
        {
            var actualModel = accountInfoService
                .GetBalanceInfo(1, 1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(memberBalanceInfoModel, actualModel);
        }

        [Fact]
        public void GetBalanceInfoModel_GetAgent_ReturnsCorrectModel()
        {
            var actualModel = accountInfoService
                .GetBalanceInfo(1, 2)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(agentBalanceInfoModel, actualModel);
        }

        [Fact]
        public void GetBalanceInfoModel_GetMaster_ReturnsCorrectModel()
        {
            var actualModel = accountInfoService
                .GetBalanceInfo(1, 3)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(masterBalanceInfoModel, actualModel);
        }

        [Fact]
        public void GetCustomerStatus_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<CustomerStatusModel>();
            accountInfoDataAccess
                .GetCustomerStatus(Arg.Any<SelectCustomerAllStatusModelCriteria>())
                .Returns(Task.FromResult(model));

            var actualModel = accountInfoService
                .GetCustomerStatus(1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }

        [Fact]
        public void GetOnlineListModel_Always_ReturnsCorrectModel()
        {
            var model = Enumerable.Empty<OnlineUserModel>();
            dynamicRepository
                .FetchAsync<OnlineUserModel>(Arg.Any<SelectDownlineListCriteria>())
                .Returns(Task.FromResult(model));

            var actualModel = accountInfoService
                .GetOnlineList(1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }

        [Fact]
        public void GetDownlineListModel_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<DownlineListModel>();
            accountInfoDataAccess
                .GetDownlineList(Arg.Any<SelectDownlineListCriteria>())
                .Returns(Task.FromResult(model));

            var actualModel = accountInfoService
                .GetDownlineList(1, 1)
                .GetAwaiter()
                .GetResult();

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

        [Fact]
        public void UpdateCustomerStatus_Always_ReturnsCorrecResult()
        {
            var result = Result.CreateSuccessResult();
            var param = fixture.Create<UpdateCustomerStatusParam>();

            accountInfoDataAccess
                .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusCommand>())
                .Returns(Task.FromResult(result));

            var actualResutl = accountInfoService
                .UpdateCustomerStatus(param)
                .GetAwaiter()
                .GetResult();

            Assert.True(compareLogic.Compare(actualResutl, result).AreEqual);
        }

        [Fact]
        public void UpdateCustomerStatus_SuccessAndSyncedCasino_CallSyncedToRnGCasino3rdByStatus()
        {
            var result = Result.CreateSuccessResult();
            var param = new UpdateCustomerStatusParam(
                searchAccountId: 1,
                actorId: 1,
                subAccountId: 0,
                language: "E",
                ip: "192.168.1.1",
                statusName: "ouright",
                statusValue: true,
                actorName: "Actor",
                syncedCasino3rd: true);

            accountInfoDataAccess
                .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusCommand>())
                .Returns(Task.FromResult(result));

            accountInfoService
                .UpdateCustomerStatus(param)
                .GetAwaiter()
                .GetResult();

            rnGCasinoService.Received().SyncedToRnGCasino3rdByStatus(
                Arg.Is<UpdateRnGCasino3rdParam>(rngParam =>
                rngParam.SearchAccountId.Equals(1) &&
                rngParam.StatusName.Equals("ouright") &&
                rngParam.StatusValue.Equals(true) &&
                rngParam.ActorId.Equals(1) &&
                rngParam.ActorName.Equals("Actor") &&
                rngParam.IP.Equals("192.168.1.1")));
        }

        [Fact]
        public void UpdateCustomerStatus_SuccessAndSyncedCasino_DoesNotCallSyncedToRnGCasino3rdByStatus()
        {
            var result = Result.CreateErrorResult(10, string.Empty);
            var param = new UpdateCustomerStatusParam(
                searchAccountId: 1,
                actorId: 1,
                subAccountId: 0,
                language: "E",
                ip: "192.168.1.1",
                statusName: "ouright",
                statusValue: true,
                actorName: "Actor",
                syncedCasino3rd: true);

            accountInfoDataAccess
                .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusCommand>())
                .Returns(Task.FromResult(result));

            accountInfoService
                .UpdateCustomerStatus(param)
                .GetAwaiter()
                .GetResult();

            rnGCasinoService.DidNotReceive().SyncedToRnGCasino3rdByStatus(Arg.Any<UpdateRnGCasino3rdParam>());
        }

        [Fact]
        public void GetMasterBalanceInfoModel_Always_ReturnsCorrectModel()
        {
            var criteria = new SelectBalanceInfoCriteria(1, 3);

            dynamicRepository
                .GetAsync<MasterBalanceInfoModel>(Arg.Any<SelectBalanceInfoCriteria>())
                .Returns(Task.FromResult(masterBalanceInfoModel));

            var actualModel = accountInfoService
                .GetMasterBalanceInfoModel(criteria)
                .GetAwaiter()
                .GetResult();

            Assert.True(compareLogic.Compare(actualModel, masterBalanceInfoModel).AreEqual);
        }

        [Fact]
        public void GetAgentBalanceInfoModel_Always_ReturnsCorrectModel()
        {
            var criteria = new SelectBalanceInfoCriteria(1, 2);

            dynamicRepository
                .GetAsync<AgentBalanceInfoModel>(Arg.Any<SelectBalanceInfoCriteria>())
                .Returns(agentBalanceInfoModel);

            var actualModel = accountInfoService
                .GetAgentBalanceInfoModel(criteria)
                .GetAwaiter()
                .GetResult();

            Assert.True(compareLogic.Compare(actualModel, agentBalanceInfoModel).AreEqual);
        }

        [Fact]
        public void GetMemberBalanceInfoModel_Always_ReturnsCorrectModel()
        {
            var criteria = new SelectBalanceInfoCriteria(1, 1);

            dynamicRepository
                .GetAsync<MemberBalanceInfoModel>(Arg.Any<SelectBalanceInfoCriteria>())
                .Returns(Task.FromResult(memberBalanceInfoModel));

            var actualModel = accountInfoService
                .GetMemberBalanceInfoModel(criteria)
                .GetAwaiter()
                .GetResult();

            Assert.True(compareLogic.Compare(actualModel, memberBalanceInfoModel).AreEqual);
        }

        private Task<IBalanceInfoModel> GetMemberBalanceInfoModel(ICriteria criteria)
            => Task.FromResult(memberBalanceInfoModel as IBalanceInfoModel);

        private Task<IBalanceInfoModel> GetAgentBalanceInfoModel(ICriteria criteria)
             => Task.FromResult(agentBalanceInfoModel as IBalanceInfoModel);

        private Task<IBalanceInfoModel> GetMasterBalanceInfoModel(ICriteria criteria)
             => Task.FromResult(masterBalanceInfoModel as IBalanceInfoModel);
    }
}