namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;

    public abstract class BalanceInfoViewModelBase : AccountInfoViewModelBase, IBalanceInfoViewModel
    {
        protected readonly CustomerInfoQuery message;
        protected readonly CustomerInfoModel customerInfo;

        protected BalanceInfoViewModelBase(
            CustomerInfoQuery message, 
            CustomerInfoModel customerInfo,
            Func<string, string, string> getMicroSiteUrl = null,
            Func<string, ResourceManager, string> convertResourcesJson = null) : base(getMicroSiteUrl, convertResourcesJson)
        {
            this.message = message;
            this.customerInfo = customerInfo;
        }

        public abstract decimal TotalBalance { get; }

        public abstract decimal YesterdayBalance { get; }

        public abstract decimal Outstanding { get; }

        internal virtual bool ShowTransferLink => !message.AllDayBalTransfer && message.AllowTransfer;

        internal virtual bool ShowReportLink => message.ViewReports && Outstanding > 0;

        public string TransferLinkOnTotalBalance =>
            ShowTransferLink && message.AllowedTotalBalanceTransfer && !TotalBalance.Equals(0) ?
            BuildTransferLink(TotalBalance) : string.Empty;

        public string TransferLinkOnYesterdayBalance =>
            ShowTransferLink && !message.AllowedTotalBalanceTransfer && !YesterdayBalance.Equals(0) ?
            BuildTransferLink(YesterdayBalance) : string.Empty;

        protected abstract string BuildTransferLink(decimal transferValue);

        protected string BreadcrumbLevelJson
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
                    custInfos.Where(custInfo => custInfo.CustLevelId > customerInfo.RoleId 
                    && custInfo.CustLevelId <= message.ActorRoleId)).Replace("\"", "'");
            }
        }

        protected static decimal RoundNumber(decimal value, byte decimalScale = 2)
            => Math.Round(value, decimalScale, MidpointRounding.AwayFromZero);
    }
}