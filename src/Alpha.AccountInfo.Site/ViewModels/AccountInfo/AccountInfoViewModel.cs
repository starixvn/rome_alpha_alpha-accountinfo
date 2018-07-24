namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Site.Helpers;

    public class AccountInfoViewModel
    {
        public AccountInfoViewModel(string accountInfoSiteAppPath = null)
        {
            AccountInfoSiteAppPath = string.IsNullOrEmpty(accountInfoSiteAppPath) ? UrlHelper.MapAccountInfoPath("~/") : accountInfoSiteAppPath;
        }
        public string AccountInfoSiteAppPath { get; }
    }
}