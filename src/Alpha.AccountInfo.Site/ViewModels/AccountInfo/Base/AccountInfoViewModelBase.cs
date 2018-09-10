namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using System;
    using System.Resources;

    public class AccountInfoViewModelBase
    {
        private readonly Func<string, string, string> GetMicroSiteUrl;

        public AccountInfoViewModelBase(
            Func<string, string, string> getMicroSiteUrl = null,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            GetMicroSiteUrl = getMicroSiteUrl ?? SimpleServiceLocator.GetService<Func<string, string, string>>();
            ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
        }

        public string AccountInfoUrl => GetMicroSiteUrl(MicroSiteConstants.AccountInfo, "~/");

        protected string OldMainUrl => GetMicroSiteUrl(MicroSiteConstants.OldMain, "~/");

        protected string BetListsUrl => GetMicroSiteUrl(MicroSiteConstants.BetLists, "~/");

        protected string ViewLogUrl => GetMicroSiteUrl(MicroSiteConstants.ViewLog, "~/");

        protected string ReportsUrl => GetMicroSiteUrl(MicroSiteConstants.Reports, "~/");

        protected readonly Func<string, ResourceManager, string> ConvertResourcesJson;
    }
}
