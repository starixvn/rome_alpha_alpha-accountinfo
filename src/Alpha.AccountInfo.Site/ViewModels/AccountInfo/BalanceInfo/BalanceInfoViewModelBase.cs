namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BalanceInfoViewModelBase
    {
        protected readonly BalanceInfoModel model;
        protected readonly CustomerInfoQuery message;
        protected readonly CustomerInfoModel customerInfoModel;

        protected BalanceInfoViewModelBase(
            CustomerInfoQuery message, 
            BalanceInfoModel model,
            CustomerInfoModel customerInfoModel)
        {
            this.model = model;
            this.message = message;
            this.customerInfoModel = customerInfoModel;
        }

        protected static string OldMainPath => UrlHelper.MapOldMainPath("~/");

        protected static string ReportPath => UrlHelper.MapReportsPath("~/");

        protected static string BetListPath => UrlHelper.MapBetListPath("~/");

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
                    new BreadcrumbLevel(customerInfoModel.SuperId, customerInfoModel.SuperName, 4),
                    new BreadcrumbLevel(customerInfoModel.MasterId, customerInfoModel.MasterName, 3),
                    new BreadcrumbLevel(customerInfoModel.AgentId, customerInfoModel.AgentName, 2)
                };

                return JsonConvert.SerializeObject(
                    custInfos.Where(custInfo => custInfo.CustLevelId > customerInfoModel.RoleId 
                    && custInfo.CustLevelId <= message.ActorRoleId)).Replace("\"", "'");
            }
        }

        protected static decimal RoundNumber(decimal value, byte decimalScale = 2)
            => Math.Round(value, decimalScale, MidpointRounding.AwayFromZero);
    }
}