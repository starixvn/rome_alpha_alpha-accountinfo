namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using System.Resources;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;

    public class AccountInfoViewModel : AccountInfoViewModelBase
    {
        private readonly SearchAccountQuery message;
        private readonly Func<string, ResourceManager, string> ConvertResourcesJson;

        public AccountInfoViewModel(
            SearchAccountQuery message,
            Func<string, string, string> getMicroSiteUrl = null,
            Func<string, ResourceManager, string> convertResourcesJson = null)
            : base(getMicroSiteUrl)
        {
            this.message = message;
            ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
        }

        public string SearchAccountName => message.SearchAccountName;

        public string ActorLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId).DisplayName;

        public byte ActorRoleId => message.ActorRoleId;

        public string ActoreName => message.ActorName;

        public bool MobileView => message.MobileView;

        public bool ShowLoginInfo => !message.IsEnabledCustomerDateLimit;

        public string AccountInfoResources => ConvertResourcesJson(message.Language, Resources.ResourceManager);
    }
}