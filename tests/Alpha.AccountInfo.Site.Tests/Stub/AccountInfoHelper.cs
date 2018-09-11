namespace Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using NSubstitute;
    using System;
    using System.Collections.Generic;

    public static class AccountInfoHelper
    {
        public static Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, BalanceInfoViewModelBase>> BalanceInfoViewModels 
            => new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, BalanceInfoViewModelBase>>
            {
                { UserLevel.Master, BuildMasterBalanceInfoViewModel},
                { UserLevel.Agent, BuildAgentBalanceInfoViewModel},
                { UserLevel.Member, BuildMemberBalanceInfoViewModel}
            };

        private static MemberBalanceInfoModel MemberBalanceInfo
           => new MemberBalanceInfoModel(
                   custId: 1,
                   credit: 12,
                   agentPositionTaking: 13,
                   customerCash: 14,
                   yesterday_CustomerCash: 15,
                   customerOutstanding: 16,
                   casinoOutstanding: 17,
                   casinoTransfer: 18,
                   netPosition: 19,
                   memMaxCredit_Max: 20);

        private static AgentBalanceInfoModel AgentBalanceInfo
            => new AgentBalanceInfoModel(
                      custId: 1,
                      credit: 120,
                      agentCash: 13,
                      yesterday_AgentCash: 14,
                      customerCash: 15,
                      yesterday_CustomerCash: 16,
                      customerOutstanding: 17,
                      casinoOutstanding: 18,
                      agentPositionTaking: 19,
                      casinoTransfer: 20,
                      masterUsedCredit: 21,
                      agentUsedCredit: 22,
                      agentCredit_Min: 23,
                      agentCredit_Max: 85,
                      memMaxCredit_Min: 25,
                      memberMaxCredit: 26);

        private static MasterBalanceInfoModel MasterBalanceInfo
            => new MasterBalanceInfoModel(
                    custId: 1,
                    credit: 12,
                    masterCash: 13,
                    yesterday_MasterCash: 14,
                    masterPositionTaking: 15,
                    agentCash: 16,
                    yesterday_AgentCash: 17,
                    customerCash: 18,
                    yesterday_CustomerCash: 19,
                    customerOutstanding: 20,
                    casinoOutstanding: 21,
                    casinoTransfer: 22,
                    masterUsedCredit: 23,
                    masterCredit_Min: 24,
                    masterCredit_Max: 85);

        public static CustomerInfoModel BuildCustomerInfo (byte searchAccountRoleId = 1) =>
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
            viewer.SubAccId.Returns(0);
            viewer.Id.Returns(1);
            viewer.LevelId.Returns(actorRoleId);
            viewer.CanViewReports.Returns(true);
            viewer.AllowedTotalBalanceTransfer.Returns(true);
            viewer.AllowedAllDayBalTransfer.Returns(false);
            viewer.AllowTransfer.Returns(true);

            return viewer;
        }

        public static string GetMicroSiteUrl(string microSiteName, string virtualPath) => "MicroSiteUrl/";

        public static MemberBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo)
        {
            messages.UserName = "UserTest";
            return new MemberBalanceInfoViewModel(
                  messages,
                  MemberBalanceInfo,
                  BuildCustomerInfo(),
                  GetMicroSiteUrl);
        }

        public static MemberBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery messages,
            IBalanceInfoModel balanceInfo)
        {
            messages.UserName = "UserTest";
            return new MemberBalanceInfoViewModel(
                   messages,
                   balanceInfo,
                   BuildCustomerInfo(),
                   GetMicroSiteUrl);
        }

        public static AgentBalanceInfoViewModel BuildAgentBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo)
        {
            messages.UserName = "UserTest";
            return new AgentBalanceInfoViewModel(
                   messages,
                   AgentBalanceInfo,
                   customerInfo,
                   GetMicroSiteUrl);
        }

        public static MasterBalanceInfoViewModel BuildMasterBalanceInfoViewModel(
            CustomerInfoQuery messages,
            CustomerInfoModel customerInfo)
        {
            messages.UserName = "UserTest";
            return new MasterBalanceInfoViewModel(
                     messages,
                     MasterBalanceInfo,
                     customerInfo,
                     GetMicroSiteUrl);
        }
    }
}
