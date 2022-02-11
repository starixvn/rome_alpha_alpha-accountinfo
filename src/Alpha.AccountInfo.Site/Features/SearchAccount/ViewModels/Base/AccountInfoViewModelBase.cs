namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using System.Web;
    using Alpha.AccountInfo.Configurations;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;

    public class AccountInfoViewModelBase
    {
        private readonly Func<string, string, string> GetMicroSiteUrl;

        public AccountInfoViewModelBase(Func<string, string, string> getMicroSiteUrl = null)
        {
            GetMicroSiteUrl = getMicroSiteUrl ?? SimpleServiceLocator.GetService<Func<string, string, string>>();
        }

        public string AccountInfoUrl => GetMicroSiteUrl(MicroSiteConstants.AccountInfo, "~/");

        protected string OldMainUrl => GetMicroSiteUrl(MicroSiteConstants.OldMain, "~/");

        protected string BetListsUrl => GetMicroSiteUrl(MicroSiteConstants.BetLists, "~/");

        protected string ViewLogUrl => GetMicroSiteUrl(MicroSiteConstants.ViewLog, "~/");

        protected string ReportsUrl => 
            AppSettings.IsStandaloneReports ? $"{ReportDomainUrl}/" : 
            GetMicroSiteUrl(MicroSiteConstants.Reports, "~/");

        protected string MemberInfoUrl => GetMicroSiteUrl(MicroSiteConstants.MemberInfo, "~/");

        private static string ReportDomainUrl =>
            AppSettings.ReportDomain.IndexOf("{0}", StringComparison.OrdinalIgnoreCase) != -1 ?
            string.Format(AppSettings.ReportDomain, HttpContext.Current.Request.Headers["Host"].Replace("mb.", string.Empty).Replace("mb2.", string.Empty)) :
            AppSettings.ReportDomain;
    }
}