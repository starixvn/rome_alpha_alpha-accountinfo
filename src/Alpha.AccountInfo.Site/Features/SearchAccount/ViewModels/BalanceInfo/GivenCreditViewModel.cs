namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using System.Resources;
    using Alpha.AccountInfo.Features.SearchAccount.AppResources;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.Products.Core.Services;

    public class GivenCreditViewModel : AccountInfoViewModelBase
    {
        private readonly GivenCreditQuery message;
        private readonly Func<string, ResourceManager, string> ConvertResourcesJson;

        public GivenCreditViewModel(
            GivenCreditQuery message,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            this.message = message;
            ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
        }

        public int SearchAccountId => message.SearchAccountId;

        public string SearchAccountName => message.SearchAccountName;

        public byte SearchAccountRoleId => message.SearchAccountRoleId;

        public decimal CurrentGivenCredit => message.CurrentGivenCredit;

        public string GivenCreditResources => ConvertResourcesJson(message.Language, GivenCredit.ResourceManager);
    }
}