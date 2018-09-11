namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Shared;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BalanceInfoViewModelBase : AccountInfoViewModelBase, IBalanceInfoViewModel
    {
        protected readonly CustomerInfoQuery message;
        protected readonly CustomerInfoModel customerInfo;

        protected BalanceInfoViewModelBase(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            Func<string, string, string> getMicroSiteUrl = null) : base(getMicroSiteUrl)
        {
            this.message = message;
            this.customerInfo = customerInfo;
        }

        public abstract decimal TotalBalance { get; }

        public abstract decimal YesterdayBalance { get; }

        public abstract decimal Outstanding { get; }

        internal virtual bool ShowTransferLink => !message.AllDayBalTransfer && message.AllowTransfer;

        public bool ShowOutstandingLink => message.ViewReports && Outstanding > 0;

        public Link ReportLink
        {
            get
            {
                var labels = new Dictionary<UserLevel, string>
                {
                    {UserLevel.Master, Resources.MasterOutstanding },
                    {UserLevel.Agent, Resources.AgentOutstanding },
                    {UserLevel.Member, Resources.Outstanding },
                };

                var betListLink = $"{BetListsUrl}RunningBetList/Index?CustID={customerInfo.CustId}&amp;CustName=ACGUUS3838033&amp;GetAll=True";

                var outstandingLink = $"{ReportsUrl}outstanding/{Enumeration.FromValue<UserLevel>((byte)(message.ActorRoleId - 1)).DisplayName}?" +
                        $"CustId={customerInfo.CustId}&CustName={message.UserName}" +
                        $"&CustLevelId={customerInfo.RoleId}&BreadcrumbLevelJson={BreadcrumbLevelJson}";

                return new Link(labels[SearchAccountLevel], SearchAccountLevel.IsMemberLevel() ? betListLink : outstandingLink);
            }
        }

        public bool ShowTotalBalanceTransferLink => ShowTransferLink && message.AllowedTotalBalanceTransfer && !TotalBalance.Equals(0);

        public string TotalBalanceTransferLink => BuildTransferLink(TotalBalance);

        public bool ShowYesterdayBalanceTransferLink => ShowTransferLink && !message.AllowedTotalBalanceTransfer && !YesterdayBalance.Equals(0);

        public string YesterdayBalanceTransferLink => BuildTransferLink(YesterdayBalance);

        protected string BuildTransferLink(decimal transferValue)
            => SearchAccountLevel.IsMemberLevel() ?
             $"{OldMainUrl}_Transfer/Agent/MemberTransfer.aspx?custid={customerInfo.CustId}&CallBack=false" :
             $"{OldMainUrl}_Transfer/{ActorLevel.DisplayName}/{SearchAccountLevel.DisplayName}Transfer.aspx?custid={customerInfo.CustId}" +
             $"&status=1&CallBack=false&username={message.UserName}&isyesterday=1&amt={transferValue}";

        protected static decimal RoundNumber(decimal value, byte decimalScale = 2)
            => Math.Round(value, decimalScale, MidpointRounding.AwayFromZero);

        private string BreadcrumbLevelJson
        {
            get
            {
                var custInfos = new List<BreadcrumbLevel>
                {
                    new BreadcrumbLevel(customerInfo.SuperId, customerInfo.SuperName, 4),
                    new BreadcrumbLevel(customerInfo.MasterId, customerInfo.MasterName, 3),
                    new BreadcrumbLevel(customerInfo.AgentId, customerInfo.AgentName, 2)
                };

                return JsonConvert.SerializeObject(
                    custInfos.Where(custInfo =>
                    custInfo.CustLevelId > customerInfo.RoleId && custInfo.CustLevelId <= message.ActorRoleId))
                    .Replace("\"", "'");
            }
        }

        protected UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId);

        protected UserLevel SearchAccountLevel => Enumeration.FromValue<UserLevel>(customerInfo.RoleId);
    }
}