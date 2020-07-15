namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.SearchAccount.AppResources;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using System;
    using System.Resources;

    public class GivenCreditViewModel : AccountInfoViewModelBase
    {
        private readonly GivenCreditQuery message;
        private readonly Func<string, ResourceManager, string> ConvertResourcesJson;

        public GivenCreditViewModel(
            GivenCreditQuery message,
            CreditInfo creditInfo,
            decimal memberCreditMin,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            this.message = message;
            CurrentGivenCredit = creditInfo.Credit;

            var level = Enumeration.FromValue<UserLevel>(message.SearchAccountRoleId);
            if (level.IsMasterLevel())
            {
                GivenCreditMax = creditInfo.MasterCreditMax;
                GivenCreditMin = creditInfo.MasterCreditMin;
            }
            else if(level.IsAgentLevel())
            {
                GivenCreditMax = creditInfo.AgentCreditMax;
                GivenCreditMin = creditInfo.AgentCreditMin;
            }
            else
            {
                GivenCreditMax = creditInfo.MemMaxCreditMax;
                GivenCreditMin = memberCreditMin;
            }

            ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
        }

        public int SearchAccountId => message.SearchAccountId;

        public string SearchAccountName => message.SearchAccountName;

        public byte SearchAccountRoleId => message.SearchAccountRoleId;

        public decimal CurrentGivenCredit { get; }

        public decimal GivenCreditMin { get;  }

        public decimal GivenCreditMax { get; }

        public string GivenCreditResources => ConvertResourcesJson(message.Language, GivenCredit.ResourceManager);
    }
}