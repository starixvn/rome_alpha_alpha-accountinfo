namespace Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using NSubstitute;
    using System;

    public static class StubAccountInfo
    {
        public static CustomerInfoModel CustomerInfo =>
            new CustomerInfoModel(
                custId: 1,
                roleId: 1,
                userName: "UserTest",
                userName2: string.Empty,
                firstName: string.Empty,
                lastName: string.Empty,
                phone: string.Empty,
                mobilePhone: string.Empty,
                fax: string.Empty,
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
                memberName: "Member Name",
                agentId: 12,
                agentName: "Agent Name",
                masterId: 13,
                masterName: "Master Name",
                superId: 14,
                superName: "Super Name");

        public static IUser GetViewer(byte roleId)
        {
            var viewer = Substitute.For<IUser>();
            viewer.SubAccId.Returns(0);
            viewer.Id.Returns(1);
            viewer.LevelId.Returns(roleId);
            viewer.CanViewReports.Returns(true);
            viewer.AllowedTotalBalanceTransfer.Returns(true);
            viewer.AllowedAllDayBalTransfer.Returns(false);
            viewer.AllowTransfer.Returns(true);

            return viewer;
        }          
    }
}
