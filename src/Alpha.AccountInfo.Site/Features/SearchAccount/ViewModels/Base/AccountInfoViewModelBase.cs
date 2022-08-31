namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using System.Linq;
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

        protected string ReportsUrl
        {
            get
            {
                var cookieless = HttpContext.Current.Session.IsCookieless ? $"/(S({HttpContext.Current.Session.SessionID}))" : string.Empty;

                return AppSettings.IsStandaloneReports ? $"{ReportDomainUrl}{cookieless}/" :
                GetMicroSiteUrl(MicroSiteConstants.Reports, "~/");
            }
        }
        protected string MemberInfoUrl => GetMicroSiteUrl(MicroSiteConstants.MemberInfo, "~/");

        private static string ReportDomainUrl
        {
            get
            {
                var mainDomain = HttpContext.Current.Request.Headers["Host"];
                if (HttpContext.Current.Session.IsCookieless)
                {
                    var subDomain = mainDomain.Split('.').ElementAt(0);
                    mainDomain = mainDomain.Replace($"{subDomain}.", string.Empty);
                }

                return AppSettings.ReportDomain.IndexOf("{0}", StringComparison.OrdinalIgnoreCase) != -1 ?
                    string.Format(AppSettings.ReportDomain, mainDomain) :
                    AppSettings.ReportDomain;
            }
        }
    }
}