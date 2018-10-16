namespace Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo
{
    using System;
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using NSubstitute;

    public static class AccountInfoHelper
    {
        public static Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, BalanceInfoViewModelBase>> BalanceInfoViewModels
            => new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, BalanceInfoViewModelBase>>
            {
                { UserLevel.Master, BuildMasterBalanceInfoViewModel},
                { UserLevel.Agent, BuildAgentBalanceInfoViewModel},
                { UserLevel.Member, BuildMemberBalanceInfoViewModel}
            };

        public static CustomerInfoModel BuildCustomerInfo(byte searchAccountRoleId = 1) =>
            new CustomerInfoModel(
                custId: 1,
                roleId: searchAccountRoleId,
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

        public static IUser ViewerFixture(byte actorRoleId)
        {
            var viewer = Substitute.For<IUser>();
            viewer.Id.Returns(1);
            viewer.Name.Returns("ActorName");
            viewer.LevelId.Returns(actorRoleId);
            viewer.SubAccId.Returns(0);
            viewer.SubAccName.Returns("subaccname");
            viewer.UserId.Returns(8);
            viewer.IP.Returns("::1");
            viewer.LangKey.Returns("E");
            viewer.Language.Returns("En");
            viewer.IsInternal.Returns(false);
            viewer.AllowedTotalBalanceTransfer.Returns(true);
            viewer.AllowedAllDayBalTransfer.Returns(false);
            viewer.CanViewReports.Returns(true);
            viewer.AllowTransfer.Returns(true);
            viewer.GetDisabledProduct(Arg.Any<int>()).Returns(true);
            viewer.GetHideProduct(Arg.Any<int>()).Returns(true);
            viewer.GetSyncedProduct(Arg.Any<int>()).Returns(true);
            viewer.IsHideKeno.Returns(true);
            viewer.CanUpdateAccount.Returns(true);
            viewer.IsCasinoSync.Returns(true);
            viewer.IsCasinoDisabled.Returns(true);
            viewer.IsHideColossusBets.Returns(true);
            viewer.DisableColossusBets.Returns(true);
            viewer.IsDisabledESports.Returns(true);
            viewer.IsNumberGameDisabled.Returns(true);

            return viewer;
        }

        public static MemberBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo)
            => new MemberBalanceInfoViewModel(
                  AssignMessagesUsername(messages),
                  MemberBalanceInfo,
                  BuildCustomerInfo(),
                  GetMicroSiteUrl);

        public static MemberBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery messages,
            IBalanceInfoModel balanceInfo)
            => new MemberBalanceInfoViewModel(
                   AssignMessagesUsername(messages),
                   balanceInfo,
                   BuildCustomerInfo(),
                   GetMicroSiteUrl);

        public static AgentBalanceInfoViewModel BuildAgentBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo)
            => new AgentBalanceInfoViewModel(
                   AssignMessagesUsername(messages),
                   AgentBalanceInfo,
                   customerInfo,
                   GetMicroSiteUrl);

        public static MasterBalanceInfoViewModel BuildMasterBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo)
            => new MasterBalanceInfoViewModel(
                     AssignMessagesUsername(messages),
                     MasterBalanceInfo,
                     customerInfo,
                     GetMicroSiteUrl);

        public static string GetMicroSiteUrl(string microSiteName, string virtualPath) => "MicroSiteUrl/";

        public static MemberBalanceInfoModel MemberBalanceInfo
          => new MemberBalanceInfoModel(
                  custId: 1,
                  credit: 12,
                  customerCash: 14,
                  yesterdayCustomerCash: 15,
                  customerOutstanding: 16,
                  casinoOutstanding: 17,
                  casinoTransfer: 18,
                  netPosition: 19,
                  memMaxCreditMax: 20);

        public static AgentBalanceInfoModel AgentBalanceInfo
            => new AgentBalanceInfoModel(
                      custId: 1,
                      credit: 120,
                      agentCash: 13,
                      yesterdayAgentCash: 14,
                      customerCash: 15,
                      yesterdayCustomerCash: 16,
                      customerOutstanding: 17,
                      casinoOutstanding: 18,
                      agentPositionTaking: 19,
                      casinoTransfer: 20,
                      masterUsedCredit: 21,
                      agentUsedCredit: 22,
                      agentCreditMin: 23,
                      agentCreditMax: 85,
                      memMaxCreditMin: 25,
                      memberMaxCredit: 26);

        public static MasterBalanceInfoModel MasterBalanceInfo
            => new MasterBalanceInfoModel(
                    custId: 1,
                    credit: 12,
                    masterCash: 13,
                    yesterdayMasterCash: 14,
                    masterPositionTaking: 15,
                    agentCash: 16,
                    yesterdayAgentCash: 17,
                    customerCash: 18,
                    yesterdayCustomerCash: 19,
                    customerOutstanding: 20,
                    casinoOutstanding: 21,
                    casinoTransfer: 22,
                    masterUsedCredit: 23,
                    masterCreditMin: 24,
                    masterCreditMax: 85);

        private static CustomerInfoQuery AssignMessagesUsername(CustomerInfoQuery messages)
        {
            messages.SearchAccountName = "UserTest";
            return messages;
        }
    }
}