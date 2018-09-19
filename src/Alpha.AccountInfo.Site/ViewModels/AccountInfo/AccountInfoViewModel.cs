namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using System;
    using System.Resources;

    public class AccountInfoViewModel : AccountInfoViewModelBase
    {
        private readonly AccountInfoQuery message;
        private readonly Func<string, ResourceManager, string> ConvertResourcesJson;

        public AccountInfoViewModel(
            AccountInfoQuery message,
            Func<string, string, string> getMicroSiteUrl = null,
            Func<string, ResourceManager, string> convertResourcesJson = null)
            : base(getMicroSiteUrl)
        {
            this.message = message;
            ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
        }

        public string ActorLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId).DisplayName;

        public byte ActorRoleId => message.ActorRoleId;

        public string ActoreName => message.ActorName;

        public string AccountInfoResources => ConvertResourcesJson(message.Language, Resources.ResourceManager);
    }
}
