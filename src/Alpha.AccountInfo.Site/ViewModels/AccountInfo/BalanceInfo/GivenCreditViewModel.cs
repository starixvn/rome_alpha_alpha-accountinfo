namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Text;
    using System.Threading;

    public class GivenCreditViewModel
    {
        private readonly GivenCreditQuery query;

        public GivenCreditViewModel(GivenCreditQuery query)
        {
            this.query = query;
        }

        public int SearchAccountId => query.SearchAccountId;

        public string SearchAccountName => query.SearchAccountName;

        public byte SearchAccountRoleId => query.SearchAccountRoleId;

        public decimal CurrentGivenCredit => query.CurrentGivenCredit;

        public string AccountInfoSiteAppPath => UrlHelper.MapAccountInfoPath("~/");

        public string AccountInfoResources
        {
            get
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(query.Language);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                ResourceSet resourceSet = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                StringBuilder builder = new StringBuilder();
                builder.Append("{");

                var count = 1;
                var aa = resourceSet.Cast<DictionaryEntry>().ToList();
                var length = aa.Count;
                resourceSet.Cast<DictionaryEntry>().ToList().ForEach(x =>
                {

                    //builder.Append("'"x.Key.ToString());
                    //builder.Append(":" +  x.Value + ",");
                    builder.Append($"\"{x.Key.ToString()}\":\"{x.Value}\"");
                    if (count < length)
                    {
                        builder.Append(",");
                    }

                    count++;

                    //var output = new ExpandoObject();
                    //AddProperty(output, x.Key.ToString(), x.Value);
                    //return output;
                });

                builder.Append("}");
                // var model = JsonConvert.SerializeObject(aa);

                return builder.ToString();
            }
        }
    }
}