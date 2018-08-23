namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Enumerations;

    public class CustomerInfoViewModel
    {
        private readonly CustomerInfoQuery message;
        private readonly CustomerInfoModel customerInfo;
        private readonly BalanceInfoModel balanceInfo;

        private readonly Dictionary<UserLevel, string> CustomerListLinkDict
            = new Dictionary<UserLevel, string>
            {
                { UserLevel.Master, "_MemberInfo/CustomerList/Super/AgentList.aspx"},
                { UserLevel.Agent,  "_MemberInfo/CustomerList/Super/MemberList.aspx"}
            };

        private readonly Dictionary<byte, string> ClosedReasonClassDict
            = new Dictionary<byte, string>
            {
                { 0, "closed-wrongPwdOthers"},
                { 1, "closed-byAdmin"},
                { 2, "closed-byUpline"},
                { 3, "closed-wrongOTPSC"}
            };

        private readonly Dictionary<byte, string> ClosedReasonMessageDict
           = new Dictionary<byte, string>
           {
                { 0, Resources.ClosedByFailedLoginOrAccountExpired},
                { 1, Resources.ClosedByAdmin},
                { 2, Resources.ClosedByUpline},
                { 3, Resources.ClosedByFailedOTPOrSC}
           };

        public CustomerInfoViewModel()
        {
        }

        public CustomerInfoViewModel(
            CustomerInfoQuery message, 
            CustomerInfoModel customerInfo,
            BalanceInfoModel balanceInfo)
        {
            this.message = message;
            this.customerInfo = customerInfo;
            this.balanceInfo = balanceInfo;
        }

        public int CustId => customerInfo.CustId;

        public byte RoleId => customerInfo.RoleId;

        public byte ActorRoleId => message.ActorRoleId;

        public string UserName => customerInfo.UserName;

        public string NickName => customerInfo.UserName2;

        public string FullName => $"{customerInfo.FirstName} {customerInfo.LastName}";

        public string Phone => customerInfo.Phone;

        public string Fax => customerInfo.Fax;

        public bool ShowMemberName => RoleId.Equals(1);

        public string MemberName => customerInfo.MemberName;

        public bool ShowAgentName => (ActorRoleId.Equals(4) || ActorRoleId.Equals(3)) && RoleId < 3;

        public int AgentId => customerInfo.AgentId;

        public string AgentName => customerInfo.AgentName;

        public bool ShowMasterName => ActorRoleId.Equals(4) && RoleId < 4;

        public int MasterId => customerInfo.MasterId;

        public string MasterName => customerInfo.MasterName;

        public int SuperId => customerInfo.SuperId;

        public string SuperName => customerInfo.SuperName;

        public LoginInfoViewModel LoginInfo {
            get
            {
                return new LoginInfoViewModel(
                    customerInfo.CreatDate,
                    customerInfo.LastLogged,
                    customerInfo.LastTxn,
                    customerInfo.LastLoginIP);
            }
        }

        public MasterBalanceInfoViewModel MasterBalanceInfo => 
            Enumeration.FromValue<UserLevel>(customerInfo.RoleId).IsMasterLevel() ?
            new MasterBalanceInfoViewModel(message, balanceInfo, customerInfo) : null;

        public AgentBalanceInfoViewModel AgentBalanceInfo =>
            Enumeration.FromValue<UserLevel>(customerInfo.RoleId).IsAgentLevel() ?
            new AgentBalanceInfoViewModel(message, balanceInfo, customerInfo) : null;

        public MemberBalanceInfoViewModel MemberBalanceInfo =>
            Enumeration.FromValue<UserLevel>(customerInfo.RoleId).IsMemberLevel() ?
            new MemberBalanceInfoViewModel(message, balanceInfo, customerInfo) : null;

        public string CustomerListLink
        {
            get
            {
                return !Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ? 
                    $"{OldMainPath}{CustomerListLinkDict[Enumeration.FromValue<UserLevel>(RoleId)]}?custid={CustId}" : string.Empty;
            }
        }

        public string BetListLink
        {
            get
            {
                return Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ? 
                    $"{BetListPath}MemberBetList/Index?CustID={CustId}&amp;CustName={UserName}" : string.Empty;
            }
        }

        public string StatemenLink
        {
            get
            {
                var memberStatementLink = $"{ReportPath}Statement/MemberStatement?CustID={CustId}&amp;CustName={UserName}&amp;CustLevelId={RoleId}";
                var SMAStatementLink = $"{ReportPath}Statement/SMAStatement?CustID={CustId}&amp;CustName={UserName}&amp;CustLevelId={RoleId}";

                return Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ? memberStatementLink : SMAStatementLink;
            }
        }

        public string ViewLogLink => $"{ViewLogsPath}setting/index?username={UserName}";

        public string ViewLoginIPLink => $"{UrlHelper.MapOldMainPath("~/")}_IPInfo/IpInfo.aspx?ip=";

        private static string ViewLogsPath => UrlHelper.MapViewLogsPath("~/");

        private static string ReportPath => UrlHelper.MapReportsPath("~/");

        private static string BetListPath => UrlHelper.MapBetListPath("~/");

        private static string OldMainPath => UrlHelper.MapOldMainPath("~/");

        public string AccountinfoPath => UrlHelper.MapAccountInfoPath("~/");

        public string StatusMessage 
        {
            get
            {
                if (customerInfo.Closed)
                {
                    return ClosedReasonMessageDict[customerInfo.ClosedReason];
                }

                if (customerInfo.Suspended)
                {
                    return Resources.ThisAccountWasSuspended;
                }

                if (customerInfo.Enabled)
                {
                    return string.Empty;
                }

                return Resources.ThisAccountWasDisabled;
            }
        }

        public string StatusCss
        {
            get
            {
                if (customerInfo.Closed)
                {
                    return ClosedReasonClassDict[customerInfo.ClosedReason];
                }

                if (customerInfo.Suspended)
                {
                    return "suspended";
                }

                if (customerInfo.Enabled)
                {
                    return "enabled";
                }

                return "disabled";
            }
        }
    }
}