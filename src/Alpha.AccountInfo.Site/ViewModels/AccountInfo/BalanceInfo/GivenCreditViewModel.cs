namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class GivenCreditViewModel : AccountInfoViewModelBase
    {
        private readonly GivenCreditQuery message;

        public GivenCreditViewModel(GivenCreditQuery message)
        {
            this.message = message;
        }

        public int SearchAccountId => message.SearchAccountId;

        public string SearchAccountName => message.SearchAccountName;

        public byte SearchAccountRoleId => message.SearchAccountRoleId;

        public decimal CurrentGivenCredit => message.CurrentGivenCredit;

        public string GivenCreditResources 
            => ResourcesJsonConverter.ToConvert(message.Language, GivenCredit.ResourceManager);
    }
}