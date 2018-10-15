namespace Alpha.AccountInfo.Site.ViewModels.SearchAccount
{
    using System;
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Requests.SearchAccount;
    using Alpha.Products.Core.Shared;

    public class CustomerInfoViewModel : AccountInfoViewModelBase
    {
        private readonly CustomerInfoQuery message;
        private readonly CustomerInfoModel customerInfo;

        public CustomerInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoViewModel balanceInfo,
            Func<string, string, string> getMicroSiteUrl = null) : base(getMicroSiteUrl)
        {
            this.message = message;
            this.customerInfo = customerInfo;
            BalanceInfo = balanceInfo;
        }

        private UserLevel SearchAccountLevel => Enumeration.FromValue<UserLevel>(RoleId);

        private UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(ActorRoleId);

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

        public bool ShowMember => SearchAccountLevel.IsMemberLevel();

        public string MemberName => customerInfo.MemberName;

        public bool ShowAgent => ActorLevel.IsSuperOrMasterLevel()
            && SearchAccountLevel.IsDownlineOfMasterLevel();

        public int AgentId => customerInfo.AgentId;

        public string AgentName => customerInfo.AgentName;

        public bool ShowMaster => ActorLevel.IsSuperLevel()
            && SearchAccountLevel.IsDownlineOfSuperLevel();

        public int MasterId => customerInfo.MasterId;

        public string MasterName => customerInfo.MasterName;

        public int SuperId => customerInfo.SuperId;

        public string SuperName => customerInfo.SuperName;

        public LoginInfoViewModel LoginInfo =>
            new LoginInfoViewModel(
                customerInfo.CreatDate,
                customerInfo.LastLogged,
                customerInfo.LastTxn,
                customerInfo.LastLoginIP);

        public IBalanceInfoViewModel BalanceInfo { get; }

        public Link CustomerListOrBetListLink
        {
            get
            {
                var labels = new Dictionary<UserLevel, string>
                {
                    {UserLevel.Master, Resources.AgentList },
                    {UserLevel.Agent, Resources.MemberList },
                    {UserLevel.Member, Resources.BetList },
                };

                var link = SearchAccountLevel.IsMemberLevel() ?
                     $"{BetListsUrl}MemberBetList/Index?CustID={CustId}&CustName={UserName}" :
                     $"{OldMainUrl}_MemberInfo/CustomerList/{ActorLevel.DisplayName}/{Enumeration.FromValue<UserLevel>((byte)(RoleId - 1)).DisplayName}List.aspx?custid={CustId}";

                return new Link(labels[SearchAccountLevel], link);
            }
        }

        public string StatemenLink
        {
            get
            {
                var actionName = SearchAccountLevel.IsMemberLevel() ? "MemberStatement" : "SMAStatement";
                return $"{ReportsUrl}Statement/{actionName}?CustID={CustId}&CustName={UserName}&CustLevelId={RoleId}";
            }
        }

        public string ViewLogLink => $"{ViewLogUrl}setting/index?username={UserName}";

        public string ViewLoginIPLink => $"{OldMainUrl}_IPInfo/IpInfo.aspx";
    }
}