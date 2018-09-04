namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Threading;

    public class AccountInfoViewModel
    {
        private readonly AccountInfoQuery message;
        private readonly Func<string, string, string> GetMicroSiteUrl;

        public AccountInfoViewModel(AccountInfoQuery message, Func<string, string, string> getMicroSiteUrl = null)
        {
            this.message = message;
             GetMicroSiteUrl = getMicroSiteUrl ?? SimpleServiceLocator.GetService<Func<string, string, string>>();
        }

        public string AccountInfoUrl => GetMicroSiteUrl(MicroSiteConstants.AccountInfo, "~/");

        public string ActorLevelName => Enumeration.FromValue<UserLevel>(message.ActorRoleId).DisplayName;

        public string AccountInfoResources
        {
            get
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(message.Language);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                ResourceSet resourceSet = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

                return string.Format("{0}{1}{2}", "{", string.Join(",", resourceSet.Cast<DictionaryEntry>().Select(x => $"\"{x.Key.ToString()}\":\"{x.Value}\"")), "}");
            }
        }
    }
}
