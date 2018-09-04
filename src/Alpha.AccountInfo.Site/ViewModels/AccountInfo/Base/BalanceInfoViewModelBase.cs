namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BalanceInfoViewModelBase : AccountInfoViewModelBase, IBalanceInfoViewModel
    {
        protected readonly CustomerInfoQuery message;
        protected readonly CustomerInfoModel customerInfo;

        protected BalanceInfoViewModelBase(CustomerInfoQuery message, CustomerInfoModel customerInfo)
        {
            this.message = message;
            this.customerInfo = customerInfo;
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
    }
}