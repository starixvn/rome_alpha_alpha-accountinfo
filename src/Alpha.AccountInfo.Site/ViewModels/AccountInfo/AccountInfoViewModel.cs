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

    public class AccountInfoViewModel : AccountInfoViewModelBase
    {
        private readonly AccountInfoQuery message;

        public AccountInfoViewModel(AccountInfoQuery message) 
        {
            this.message = message;
        }

        public string ActorLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId).DisplayName;

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
