namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.Products.Core.Shared;

    public class CustomerInfoViewModel : AccountInfoViewModelBase
    {
        private readonly CustomerInfoQuery message;
        private readonly CustomerInfoModel customerInfo;
        private readonly bool isFirstLoginQRCode;
        private readonly bool canCreateAccount;

        public CustomerInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoViewModel balanceInfo,
            bool isFirstLoginQRCode,
            bool canCreateAccount,
            Func<string, string, string> getMicroSiteUrl = null) : base(getMicroSiteUrl)
        {
            this.message = message;
            this.customerInfo = customerInfo;
            BalanceInfo = balanceInfo;
            this.isFirstLoginQRCode = isFirstLoginQRCode;
            this.canCreateAccount = canCreateAccount;
        }

        private UserLevel SearchAccountLevel => Enumeration.FromValue<UserLevel>(RoleId);

        private UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(ActorRoleId);

        public int CustId => customerInfo.CustId;

        public byte RoleId => customerInfo.RoleId;

        public byte ActorRoleId => message.ActorRoleId;

        public string UserName => customerInfo.UserName;

        public string NickName => DefaultStringValueIfNull(customerInfo.UserName2);

        public bool ShowNickName =>
            SearchAccountLevel.IsDirectDownlineOf(ActorLevel)
            && !string.IsNullOrEmpty(NickName);

        public string FullName => DefaultStringValueIfNull($"{customerInfo.FirstName} {customerInfo.LastName}");

        public string Phone => DefaultStringValueIfNull(customerInfo.Phone);

        public string Fax => DefaultStringValueIfNull(customerInfo.Fax);

        public bool Closed => customerInfo.Closed;

        public byte ClosedReason => customerInfo.ClosedReason;

        public bool Suspended => customerInfo.Suspended;

        public bool Enabled => customerInfo.Enabled;

        public bool ShowMember => SearchAccountLevel.IsMemberLevel();

        public string MemberName => DefaultStringValueIfNull(customerInfo.MemberName);

        public bool ShowAgent => ActorLevel.IsSuperOrMasterLevel()
            && SearchAccountLevel.IsDownlineOfMasterLevel();

        public int AgentId => customerInfo.AgentId;

        public string AgentName => DefaultStringValueIfNull(customerInfo.AgentName);

        public bool ShowMaster => ActorLevel.IsSuperLevel()
            && SearchAccountLevel.IsDownlineOfSuperLevel();

        public int MasterId => customerInfo.MasterId;

        public string MasterName => DefaultStringValueIfNull(customerInfo.MasterName);

        public int SuperId => customerInfo.SuperId;

        public string SuperName => DefaultStringValueIfNull(customerInfo.SuperName);

        public LoginInfoViewModel LoginInfo =>
            new LoginInfoViewModel(
                customerInfo.CreatDate,
                customerInfo.LastLogged,
                customerInfo.LastTxn,
                customerInfo.LastLoginIP);

        public IBalanceInfoViewModel BalanceInfo { get; }

        internal Link CustomerListLink =>
            new Link(SearchAccountLevel.IsMasterLevel() ? Resources.AgentList : Resources.MemberList,
            $"{OldMainUrl}_MemberInfo/CustomerList/{ActorLevel.DisplayName}/{Enumeration.FromValue<UserLevel>((byte)(RoleId - 1)).DisplayName}List.aspx?custid={CustId}");

        internal Link BetListLink =>
            new Link(Resources.BetList, $"{BetListsUrl}MemberBetList/Index?CustID={CustId}&CustName={UserName}");

        internal Link StatemenLink
        {
            get
            {
                var actionName = SearchAccountLevel.IsMemberLevel() ? "MemberStatement" : "SMAStatement";
                var link = $"{ReportsUrl}Statement/{actionName}?CustID={CustId}&CustName={UserName}&CustLevelId={RoleId}";
                return new Link(Resources.Statement, link);
            }
        }

        public IEnumerable<Link> Buttons
        {
            get
            {
                var buttons = new List<Link>();

                if (!SearchAccountLevel.IsMemberLevel())
                {
                    buttons.Add(CustomerListLink);
                }

                if (SearchAccountLevel.IsMemberLevel() && message.CanViewReports)
                {
                    buttons.Add(BetListLink);
                }

                if (message.CanViewReports)
                {
                    buttons.Add(StatemenLink);
                }

                return buttons;
            }
        }

        public bool CanViewLog => message.CanViewLog;

        public string ViewLogLink => $"{ViewLogUrl}setting/index?username={UserName}";

        public string ViewLoginIPLink => $"{OldMainUrl}_IPInfo/IpInfo.aspx";

        public bool IsShowCreateMemberByQRCode => ActorLevel.IsAgentLevel() && canCreateAccount;

        public string CreateMemberByQRCodeUrl => IsShowCreateMemberByQRCode ?
            $"{MemberInfoUrl}/QRCodeMemberSignIn?MemberName={UserName}" : string.Empty;

        public string ShowQRCodeUrl =>
            IsShowCreateMemberByQRCode && 
            customerInfo.LastLogged == DateTime.MinValue && 
            !isFirstLoginQRCode ?
                $"{MemberInfoUrl}/QRCodeMemberSignIn/ShowQRCode?FirstName={customerInfo.FirstName}" +
                $"&SearchAccountId={CustId}&SearchAccountName={UserName}&Credit={BalanceInfo.GivenCredit}" +
                $"&CreditMin={BalanceInfo.GivenCreditMin}&CreditMax={BalanceInfo.GivenCreditMax}" : string.Empty;

        internal string DefaultStringValueIfNull(string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }
    }
}