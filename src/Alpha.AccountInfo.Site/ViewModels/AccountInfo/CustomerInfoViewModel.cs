namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Shared;
    using System.Collections.Generic;

    public class CustomerInfoViewModel : AccountInfoViewModelBase
    {
        private readonly CustomerInfoQuery message;
        private readonly CustomerInfoModel customerInfo;

        private readonly Dictionary<UserLevel, string> CustomerListLinkDict
            = new Dictionary<UserLevel, string>
            {
                { UserLevel.Master, "_MemberInfo/CustomerList/Super/AgentList.aspx"},
                { UserLevel.Agent,  "_MemberInfo/CustomerList/Super/MemberList.aspx"}
            };

        public CustomerInfoViewModel(
            CustomerInfoQuery message, 
            CustomerInfoModel customerInfo,
            IBalanceInfoViewModel balanceInfo)
        {
            this.message = message;
            this.customerInfo = customerInfo;
            this.BalanceInfo = balanceInfo;
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

        public bool ShowMember => SearchAccountLevel.IsMemberLevel();

        public string MemberName => customerInfo.MemberName;

        public bool ShowAgent => ActorLevel.IsSuperOrMasterLevel() && SearchAccountLevel.IsDownlineOfMasterLevel();

        public int AgentId => customerInfo.AgentId;

        public string AgentName => customerInfo.AgentName;

        public bool ShowMaster => ActorLevel.IsSuperLevel() && SearchAccountLevel.IsDownlineOfSuperLevel();

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

        public string CustomerListLink => !SearchAccountLevel.IsMemberLevel() ?
            $"{OldMainUrl}{CustomerListLinkDict[SearchAccountLevel]}?custid={CustId}" : string.Empty;

        public string BetListLink => SearchAccountLevel.IsMemberLevel() ?
            $"{BetListsUrl}MemberBetList/Index?CustID={CustId}&CustName={UserName}" : string.Empty;

        public string StatemenLink 
            => SearchAccountLevel.IsMemberLevel() ?
            $"{ReportsUrl}Statement/MemberStatement?CustID={CustId}&CustName={UserName}&CustLevelId={RoleId}" :
            $"{ReportsUrl}Statement/SMAStatement?CustID={CustId}&CustName={UserName}&CustLevelId={RoleId}";

        public string ViewLogLink => $"{ViewLogUrl}setting/index?username={UserName}";

        public string ViewLoginIPLink => $"{OldMainUrl}_IPInfo/IpInfo.aspx";

        private UserLevel SearchAccountLevel => Enumeration.FromValue<UserLevel>(RoleId);

        private UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(ActorRoleId);
    }
}