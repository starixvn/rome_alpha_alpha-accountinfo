namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.Products.Core.Shared;
    using Newtonsoft.Json;

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

                var directDownlineLevelName = Enumeration.FromValue<UserLevel>((byte)(message.ActorRoleId - 1)).DisplayName;
                var outstandingLink = $"{ReportsUrl}outstanding/{directDownlineLevelName}?" +
                        $"CustId={customerInfo.CustId}&CustName={message.SearchAccountName}" +
                        $"&CustLevelId={customerInfo.RoleId}&BreadcrumbLevelJson={BreadcrumbLevelJson}";

                return new Link(labels[SearchAccountLevel], SearchAccountLevel.IsMemberLevel() ? betListLink : outstandingLink);
            }
        }

        public bool ShowTotalBalanceTransferLink => ShowTransferLink && message.AllowedTotalBalanceTransfer && !TotalBalance.Equals(0);

        public string TotalBalanceTransferLink => BuildTransferLink(TotalBalance);

        public bool ShowYesterdayBalanceTransferLink => ShowTransferLink && !message.AllowedTotalBalanceTransfer && !YesterdayBalance.Equals(0);

        public string YesterdayBalanceTransferLink => BuildTransferLink(YesterdayBalance);

        internal string BuildTransferLink(decimal transferValue)
            => SearchAccountLevel.IsMemberLevel() ?
             $"{OldMainUrl}_Transfer/Agent/MemberTransfer.aspx?custid={customerInfo.CustId}&CallBack=false" :
             $"{OldMainUrl}_Transfer/{ActorLevel.DisplayName}/{SearchAccountLevel.DisplayName}Transfer.aspx?custid={customerInfo.CustId}" +
             $"&status=1&CallBack=false&username={message.SearchAccountName}&isyesterday=1&amt={transferValue}";

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