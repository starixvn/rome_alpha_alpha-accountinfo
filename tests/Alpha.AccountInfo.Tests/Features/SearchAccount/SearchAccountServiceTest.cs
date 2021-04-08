namespace Alpha.AccountInfo.Tests.Features.SearchAccount
{
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Tests.Extensions;
    using AutoFixture;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SearchAccountServiceTest
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly SearchAccountDataAccess accountInfoDataAccess;
        private readonly IDictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>> getBalanceInfoModelByLevel;
        private readonly SearchAccountServiceImpl accountInfoService;
        private readonly CompareLogic compareLogic = new CompareLogic();
        private readonly Fixture fixture = new Fixture();
        private readonly MemberBalanceInfoModel memberBalanceInfoModel;
        private readonly AgentBalanceInfoModel agentBalanceInfoModel;
        private readonly MasterBalanceInfoModel masterBalanceInfoModel;

        public SearchAccountServiceTest()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            accountInfoDataAccess = Substitute.For<SearchAccountDataAccess>();

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
                getBalanceInfoModelByLevel);
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
                superName: "SuperTest",
                locked: 0);

            dynamicRepository
                .GetAsync<CustomerInfoModel>(Arg.Any<SelectCustomerInfoCriteria>())
                .Returns(Task.FromResult(model));

            var actualModel = accountInfoService
                .GetCustomerInfo("test", 1, 2)
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
        public void GetOnlineListModel_Always_ReturnsCorrectModel()
        {
            var model = Enumerable.Empty<OnlineUserModel>();
            dynamicRepository
                .FetchAsync<OnlineUserModel>(Arg.Any<SelectDownlineListByStatusCriteria>())
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
                .GetDownlineList(Arg.Any<SelectDownlineListByStatusCriteria>())
                .Returns(Task.FromResult(model));

            var actualModel = accountInfoService
                .GetDownlineList(1, 1, 2, 1, 1)
                .GetAwaiter()
                .GetResult();

            Assert.True(compareLogic.Compare(actualModel, model).AreEqual);
        }

        [Fact]
        public void UpdateGivenCredit_Always_ReturnsCorrecResult()
        {
            var result = Result.CreateSuccessResult();
            var param = fixture.Create<UpdateGivenCreditParam>();

            dynamicRepository
                .ExecuteAsync(Arg.Any<UpdateGivenCreditCommand>())
                .Returns(Task.FromResult(result));

            var actualResutl = accountInfoService.UpdateGivenCredit(param);

            Assert.True(compareLogic.Compare(actualResutl.Result, result).AreEqual);
        }

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
            var memberBalanceInfoDTO = new MemberBalanceInfoDTO(1, 12, 15, 20);
            var selectBalanceInfoCriteria = new SelectBalanceInfoCriteria(1, 1);
            dynamicRepository
                .GetAsync<MemberBalanceInfoDTO>(Arg.Any<SelectBalanceInfoCriteria>())
                .Returns(Task.FromResult(memberBalanceInfoDTO));
           
            accountInfoService
                .GetMemberBalanceInfoModel(selectBalanceInfoCriteria)
                .GetAwaiter()
                .GetResult();

            dynamicRepository.Received().ExecuteAsync(Arg.Is<SelectMemberOutstandingCriteria>(criteria => criteria.SearchAccountId == 1));
            dynamicRepository.Received().ExecuteAsync(Arg.Is<SelectMemberBalanceCriteria>(criteria => criteria.SearchAccountId == 1));
            dynamicRepository.Received().GetAsync<MemberBalanceInfoDTO>(Arg.Is<SelectBalanceInfoCriteria>(
                criteria => criteria.SearchAccountId == 1 && criteria.SearchAccountRoleId == 1));
        }

        [Fact]
        public void GetCreditInfo_Always_ReturnsCorrectModel()
        {
            var model = fixture.Create<CreditInfo>();
            dynamicRepository
                .GetAsync<CreditInfo>(Arg.Any<SelectCreditInfoCriteria>())
                .Returns(model);

            var actualModel = accountInfoService
                .GetCreditInfo(1)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(model, actualModel);
        }

        [Fact]
        public void CheckFirstLoginByQRCode_Always_ReturnsCorrectModel()
        {
            dynamicRepository
                .GetAsync<bool>(Arg.Any<SelectFirstLoginByQRCodeCriteria>())
                .Returns(true);

            var actualModel = accountInfoService
                .CheckFirstLoginByQRCode(1)
                .GetAwaiter()
                .GetResult();

            Assert.True(actualModel);
        }

        private Task<IBalanceInfoModel> GetMemberBalanceInfoModel(ICriteria criteria)
            => Task.FromResult(memberBalanceInfoModel as IBalanceInfoModel);

        private Task<IBalanceInfoModel> GetAgentBalanceInfoModel(ICriteria criteria)
             => Task.FromResult(agentBalanceInfoModel as IBalanceInfoModel);

        private Task<IBalanceInfoModel> GetMasterBalanceInfoModel(ICriteria criteria)
             => Task.FromResult(masterBalanceInfoModel as IBalanceInfoModel);
    }
}