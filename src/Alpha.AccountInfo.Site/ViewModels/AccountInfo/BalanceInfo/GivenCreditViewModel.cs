namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Threading;

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
        {
            get
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(message.Language);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                ResourceSet resourceSet = GivenCredit.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

                return string.Format("{0}{1}{2}", "{", string.Join(",", resourceSet.Cast<DictionaryEntry>().Select(x => $"\"{x.Key.ToString()}\":\"{x.Value}\"")), "}");
            }
        }
    }
}