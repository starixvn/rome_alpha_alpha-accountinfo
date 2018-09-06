namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Shared;

    public class AccountInfoViewModel : AccountInfoViewModelBase
    {
        private readonly AccountInfoQuery message;

        public AccountInfoViewModel(AccountInfoQuery message) 
        {
            this.message = message;
        }

        public string ActorLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId).DisplayName;

        public string AccountInfoResources 
            => ResourcesJsonConverter.ToConvert(message.Language, Resources.ResourceManager);
    }
}
