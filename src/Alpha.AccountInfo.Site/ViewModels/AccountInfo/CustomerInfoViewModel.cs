namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Services;
    using Enumerations;
    using System;
    using System.Collections.Generic;

    public class CustomerInfoViewModel
    {
        private readonly CustomerInfoQuery message;
        private readonly CustomerInfoModel customerInfo;
        private readonly Func<string, string, string> GetMicroSiteUrl;

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
            IBalanceInfoViewModel balanceInfo,
            Func<string, string, string> getMicroSiteUrl = null)
        {
            this.message = message;
            this.customerInfo = customerInfo;
            this.BalanceInfo = balanceInfo;
            GetMicroSiteUrl = getMicroSiteUrl ?? SimpleServiceLocator.GetService<Func<string, string, string>>();
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

        public IBalanceInfoViewModel BalanceInfo { get; }

        public string CustomerListLink =>
            !Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ?
            $"{OldMainUrl}{CustomerListLinkDict[Enumeration.FromValue<UserLevel>(RoleId)]}?custid={CustId}" : string.Empty;

        public string BetListLink =>
            Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ?
            $"{BetListUrl}MemberBetList/Index?CustID={CustId}&amp;CustName={UserName}" : string.Empty;

        public string StatemenLink =>
            Enumeration.FromValue<UserLevel>(RoleId).IsMemberLevel() ?
            $"{ReportsUrl}Statement/MemberStatement?CustID={CustId}&amp;CustName={UserName}&amp;CustLevelId={RoleId}" :
            $"{ReportsUrl}Statement/SMAStatement?CustID={CustId}&amp;CustName={UserName}&amp;CustLevelId={RoleId}";

        public string ViewLogLink => $"{ViewLogUrl}setting/index?username={UserName}";

        public string AccountinfoUrl => GetMicroSiteUrl(AccountInfoDataKey.Microsites.AccountInfo, "~/");

        public string ViewLoginIPLink => $"{OldMainUrl}_IPInfo/IpInfo.aspx?ip=";

        private string ViewLogUrl => GetMicroSiteUrl(AccountInfoDataKey.Microsites.ViewLog, "~/");

        private string ReportsUrl => GetMicroSiteUrl(AccountInfoDataKey.Microsites.Reports, "~/");

        private string BetListUrl => GetMicroSiteUrl(AccountInfoDataKey.Microsites.BetLists, "~/");

        private string OldMainUrl => GetMicroSiteUrl(AccountInfoDataKey.Microsites.OldMain, "~/");
    }
}