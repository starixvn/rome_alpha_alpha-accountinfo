namespace Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers
{
    using System;
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using NSubstitute;

    public static class AccountInfoHelper
    {
        public static Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, BalanceInfoViewModelBase>> BalanceInfoViewModels
            => new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, CreditInfo, BalanceInfoViewModelBase>>
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
            viewer.ViewMemberInfo.Returns(true);
            viewer.AllowTransfer.Returns(true);
            viewer.GetDisabledProduct(Arg.Any<int>()).Returns(true);
            viewer.GetHideProduct(Arg.Any<int>()).Returns(true);
            viewer.GetSyncedProduct(Arg.Any<int>()).Returns(true);
            viewer.IsHideKeno.Returns(true);
            viewer.CanUpdateAccount.Returns(true);
            viewer.IsCasinoSync.Returns(true);
            viewer.IsCasinoDisabled.Returns(true);
            viewer.DisableGoldDeluxe.Returns(true);
            viewer.IsHideGoldDeluxe.Returns(true);
            viewer.IsDisabledESports.Returns(true);
            viewer.IsNumberGameDisabled.Returns(true);
            viewer.CanCreateAccount.Returns(true);

            return viewer;
        }

        public static MemberBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo)
            => new MemberBalanceInfoViewModel(
                  AssignMessagesUsername(messages),
                  BuildCustomerInfo(),
                  creditInfo,
                  MemberBalanceInfo,
                  GetMicroSiteUrl);

        public static MemberBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfo)
            => new MemberBalanceInfoViewModel(
                   AssignMessagesUsername(messages),
                   BuildCustomerInfo(),
                   creditInfo,
                   balanceInfo,
                   GetMicroSiteUrl);

        public static AgentBalanceInfoViewModel BuildAgentBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo)
            => new AgentBalanceInfoViewModel(
                   AssignMessagesUsername(messages),
                   customerInfo,
                   creditInfo,
                   AgentBalanceInfo,
                   GetMicroSiteUrl);

        public static MasterBalanceInfoViewModel BuildMasterBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo,
            CreditInfo creditInfo)
            => new MasterBalanceInfoViewModel(
                     AssignMessagesUsername(messages),
                     customerInfo,
                     creditInfo,
                     MasterBalanceInfo,
                     GetMicroSiteUrl);

        public static string GetMicroSiteUrl(string microSiteName, string virtualPath) => "MicroSiteUrl/";

        public static MemberBalanceInfoModel MemberBalanceInfo
          => new MemberBalanceInfoModel(new MemberBalanceInfoDTO(1, 12, 15, 20), 20, 16, 19);

        public static AgentBalanceInfoModel AgentBalanceInfo
            => new AgentBalanceInfoModel(
                      custId: 1,
                      agentCash: 13,
                      yesterdayAgentCash: 14,
                      customerCash: 15,
                      yesterdayCustomerCash: 16,
                      customerOutstanding: 17,
                      agentPositionTaking: 19);

        public static MasterBalanceInfoModel MasterBalanceInfo
            => new MasterBalanceInfoModel(
                    custId: 1,
                    masterCash: 13,
                    yesterdayMasterCash: 14,
                    masterPositionTaking: 15,
                    agentCash: 16,
                    yesterdayAgentCash: 17,
                    customerCash: 18,
                    yesterdayCustomerCash: 19,
                    customerOutstanding: 20);

        public static CreditInfo CreditInfo
            => new CreditInfo(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

        private static CustomerInfoQuery AssignMessagesUsername(CustomerInfoQuery messages)
        {
            messages.SearchAccountName = "UserTest";
            return messages;
        }
    }
}