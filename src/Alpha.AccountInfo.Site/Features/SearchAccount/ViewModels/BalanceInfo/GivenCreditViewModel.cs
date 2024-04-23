namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.SearchAccount.AppResources;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using System;
    using System.Resources;

    public class GivenCreditViewModel : AccountInfoViewModelBase
    {
        private readonly GivenCreditQuery message;
        private readonly Func<string, ResourceManager, string> ConvertResourcesJson;
        private readonly NumberHelpers numberHelpers;

        public GivenCreditViewModel(
            GivenCreditQuery message,
            CreditInfo creditInfo,
            decimal memberCreditMin,
            Func<string, ResourceManager, string> convertResourcesJson = null,
             NumberHelpers numberHelpers = null)
        {
            this.numberHelpers = numberHelpers ?? new NumberHelpersImpl();

            this.message = message;
            CurrentGivenCredit = creditInfo.Credit;

            var level = Enumeration.FromValue<UserLevel>(message.SearchAccountRoleId);
            if (level.IsMasterLevel())
            {
                GivenCreditMax = this.numberHelpers.RoundDown(creditInfo.MasterCreditMax);
                GivenCreditMin = this.numberHelpers.RoundUp(creditInfo.MasterCreditMin);
            }
            else if (level.IsAgentLevel())
            {
                GivenCreditMax = this.numberHelpers.RoundDown(creditInfo.AgentCreditMax);
                GivenCreditMin = this.numberHelpers.RoundUp(creditInfo.AgentCreditMin);
            }
            else
            {
                GivenCreditMax = this.numberHelpers.RoundDown(creditInfo.MemMaxCreditMax);
                GivenCreditMin = this.numberHelpers.RoundUp(memberCreditMin);
            }

            ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
        }

        public int SearchAccountId => message.SearchAccountId;

        public string SearchAccountName => message.SearchAccountName;

        public byte SearchAccountRoleId => message.SearchAccountRoleId;

        public decimal CurrentGivenCredit { get; }

        public decimal GivenCreditMin { get; }

        public decimal GivenCreditMax { get; }

        public decimal MemberMaxCredit { get; }

        public string GivenCreditResources => ConvertResourcesJson(message.Language, GivenCredit.ResourceManager);
    }
}