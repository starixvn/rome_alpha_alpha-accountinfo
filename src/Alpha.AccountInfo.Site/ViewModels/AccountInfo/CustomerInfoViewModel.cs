namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Enumerations;

    public class CustomerInfoViewModel
    {
        private readonly CustomerInfoModel model;

        private const string SMAStatementLink = "/site-reports/(S(wa000mahqldvjxctxg3naqhq))/Statement/SMAStatement?CustID=1382167&amp;CustName=ACGUUS3838&amp;CustLevelId=2";
        private const string memberStatementLink = "/site-reports/(S(wa000mahqldvjxctxg3naqhq))/Statement/MemberStatement?CustID=1387971&amp;CustName=ACGUUS3838338&amp;CustLevelId=1";
        private const string betListLink = "/site-betlists/(S(wa000mahqldvjxctxg3naqhq))/MemberBetList/Index?CustID=1387971&amp;CustName=ACGUUS3838338";

        private readonly Dictionary<UserLevel, string> CustomerListLinkDict
            = new Dictionary<UserLevel, string>
            {
                { UserLevel.Master, "../_MemberInfo/CustomerList/Super/AgentList.aspx?custid=1382145"},
                { UserLevel.Agent, "../_MemberInfo/CustomerList/Super/MemberList.aspx?custid=1382167"}
            };

        public CustomerInfoViewModel(byte actorRoleId, CustomerInfoModel model)
        {
            ActorRoleId = actorRoleId;
            this.model = model;
        }

        public int CustId => model.CustId;

        public byte RoleId => model.RoleId;

        public byte ActorRoleId { get; }

        public string UserName => model.UserName;

        public string NickName => model.UserName2;

        public string Phone => model.Phone;

        public string Fax => model.Fax;

        public bool ShowMemberName => RoleId.Equals(1);

        public string MemberName => model.MemberName;

        public bool ShowAgentName => (ActorRoleId.Equals(4) || ActorRoleId.Equals(3)) && RoleId < 3;

        public string AgentName => model.AgentName;

        public bool ShowMasterName => ActorRoleId.Equals(4) && RoleId < 4;

        public string MasterName => model.MasterName;

        public string SuperName => model.SuperName;

        public LoginInfoViewModel LoginInfo {
            get
            {
                return new LoginInfoViewModel(
                    model.CreateDate,
                    model.LastLogged,
                    model.LastTxn,
                    model.LastLoginIP);
            }
        }

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