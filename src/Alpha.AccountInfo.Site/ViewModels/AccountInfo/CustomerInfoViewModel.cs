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

        public bool Closed => customerInfo.Closed;

        public byte ClosedReason => customerInfo.ClosedReason;

        public bool Suspended => customerInfo.Suspended;

        public bool Enabled => customerInfo.Enabled;

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

        public string CustomerListLink =>
            !Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ?
            $"{OldMainPath}{CustomerListLinkDict[Enumeration.FromValue<UserLevel>(RoleId)]}?custid={CustId}" : string.Empty;

        public string BetListLink =>
            Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ?
            $"{BetListPath}MemberBetList/Index?CustID={CustId}&amp;CustName={UserName}" : string.Empty;

        public string StatemenLink =>
            Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ?
            $"{ReportPath}Statement/MemberStatement?CustID={CustId}&amp;CustName={UserName}&amp;CustLevelId={RoleId}" :
            $"{ReportPath}Statement/SMAStatement?CustID={CustId}&amp;CustName={UserName}&amp;CustLevelId={RoleId}";

        public string ViewLogLink => $"{ViewLogsPath}setting/index?username={UserName}";

        public string AccountinfoPath => UrlHelper.MapAccountInfoPath("~/");

        public string ViewLoginIPLink => $"{UrlHelper.MapOldMainPath("~/")}_IPInfo/IpInfo.aspx?ip=";

        private static string ViewLogsPath => UrlHelper.MapViewLogsPath("~/");

        private static string ReportPath => UrlHelper.MapReportsPath("~/");

        private static string BetListPath => UrlHelper.MapBetListPath("~/");

        private static string OldMainPath => UrlHelper.MapOldMainPath("~/");
    }
}