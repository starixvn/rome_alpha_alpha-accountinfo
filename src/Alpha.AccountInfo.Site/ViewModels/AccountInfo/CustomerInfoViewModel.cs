namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Enumerations;

    public class CustomerInfoViewModel
    {
        private readonly CustomerInfoQuery message;
        private readonly CustomerInfoModel customerInfo;
        private readonly BalanceInfoModel balanceInfo;

        private const string SMAStatementLink = "/site-reports/(S(wa000mahqldvjxctxg3naqhq))/Statement/SMAStatement?CustID=1382167&amp;CustName=ACGUUS3838&amp;CustLevelId=2";
        private const string memberStatementLink = "/site-reports/(S(wa000mahqldvjxctxg3naqhq))/Statement/MemberStatement?CustID=1387971&amp;CustName=ACGUUS3838338&amp;CustLevelId=1";
        private const string betListLink = "/site-betlists/(S(wa000mahqldvjxctxg3naqhq))/MemberBetList/Index?CustID=1387971&amp;CustName=ACGUUS3838338";

        private readonly Dictionary<UserLevel, string> CustomerListLinkDict
            = new Dictionary<UserLevel, string>
            {
                { UserLevel.Master, "../_MemberInfo/CustomerList/Super/AgentList.aspx?custid=1382145"},
                { UserLevel.Agent, "../_MemberInfo/CustomerList/Super/MemberList.aspx?custid=1382167"}
            };

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

        public string Phone => customerInfo.Phone;

        public string Fax => customerInfo.Fax;

        public bool ShowMemberName => RoleId.Equals(1);

        public string MemberName => customerInfo.MemberName;

        public bool ShowAgentName => (ActorRoleId.Equals(4) || ActorRoleId.Equals(3)) && RoleId < 3;

        public string AgentName => customerInfo.AgentName;

        public bool ShowMasterName => ActorRoleId.Equals(4) && RoleId < 4;

        public string MasterName => customerInfo.MasterName;

        public string SuperName => customerInfo.SuperName;

        public LoginInfoViewModel LoginInfo {
            get
            {
                return new LoginInfoViewModel(
                    customerInfo.CreateDate,
                    customerInfo.LastLogged,
                    customerInfo.LastTxn,
                    customerInfo.LastLoginIP);
            }
        }

        public MasterBalanceInfoViewModel MasterBalanceInfo 
            => Enumeration.FromValue<UserLevel>(customerInfo.RoleId).IsMasterLevel() 
            ? new MasterBalanceInfoViewModel(message, balanceInfo) : null;

        public AgentBalanceInfoViewModel AgentBalanceInfo
            => Enumeration.FromValue<UserLevel>(customerInfo.RoleId).IsAgentLevel()
            ? new AgentBalanceInfoViewModel(message, balanceInfo) : null;

        public MemberBalanceInfoViewModel MemberBalanceInfo
             => Enumeration.FromValue<UserLevel>(customerInfo.RoleId).IsMemberLevel()
            ? new MemberBalanceInfoViewModel(message, balanceInfo) : null;

        public string CustomerListLink
        {
            get
            {
                return RoleId > 1 ? CustomerListLinkDict[Enumeration.FromValue<UserLevel>(RoleId)] : string.Empty;
            }
        }

        public string BetListLink
        {
            get
            {
                return RoleId.Equals(1) ? betListLink : string.Empty;
            }
        }

        public string StatemenLink
        {
            get
            {
                return RoleId.Equals(1) ? memberStatementLink : SMAStatementLink;
            }
        }

        public string ViewLogLink { get; } = "/site-viewlogs/(S(wa000mahqldvjxctxg3naqhq))/setting/index?username=ACGUUS3838338";
    }
}