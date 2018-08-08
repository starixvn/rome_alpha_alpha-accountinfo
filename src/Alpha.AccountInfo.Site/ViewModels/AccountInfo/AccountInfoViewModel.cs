namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class AccountInfoViewModel
    {
        public AccountInfoViewModel(AccountInfoQuery message)
        {
            ActorLevelName = Enumeration.FromValue<UserLevel>(message.ActorRoleId).DisplayName;
        }
        public string AccountInfoSiteAppPath => UrlHelper.MapAccountInfoPath("~/");

        public string ActorLevelName { get; }
    }
}