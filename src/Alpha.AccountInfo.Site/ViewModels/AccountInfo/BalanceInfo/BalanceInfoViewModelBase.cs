namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Constants;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BalanceInfoViewModelBase : IBalanceInfoViewModel
    {
        protected readonly CustomerInfoQuery message;
        protected readonly CustomerInfoModel customerInfo;
        private readonly Func<string, string, string> GetMicroSiteUrl;

        protected BalanceInfoViewModelBase(
            CustomerInfoQuery message, 
            CustomerInfoModel customerInfo,
            Func<string, string, string> getMicroSiteUrl = null)
        {
            this.message = message;
            this.customerInfo = customerInfo;
            GetMicroSiteUrl = getMicroSiteUrl ?? SimpleServiceLocator.GetService<Func<string, string, string>>();
        }

        public abstract decimal TotalBalance { get; }

        public abstract decimal YesterdayBalance { get; }

        protected abstract bool ShowTransferLink { get; }

        protected abstract string BuildTransferLink(decimal transferValue);

        public string TransferLinkOnTotalBalance =>
            ShowTransferLink && message.AllowedTotalBalanceTransfer && !TotalBalance.Equals(0) ?
            BuildTransferLink(TotalBalance) : string.Empty;

        public string TransferLinkOnYesterdayBalance =>
            ShowTransferLink && !message.AllowedTotalBalanceTransfer && !YesterdayBalance.Equals(0) ?
            BuildTransferLink(YesterdayBalance) : string.Empty;

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

        protected string OldMainUrl => GetMicroSiteUrl(AccountInfoDataKey.Microsites.OldMain, "~/");

        protected string ReportsUrl => GetMicroSiteUrl(AccountInfoDataKey.Microsites.Reports, "~/");

        protected string BetListsUrl => GetMicroSiteUrl(AccountInfoDataKey.Microsites.BetLists, "~/");
    }
}