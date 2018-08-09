namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Text;
    using System.Threading;

    public class AccountInfoViewModel
    {
        private readonly AccountInfoQuery message;

        public AccountInfoViewModel(AccountInfoQuery message)
        {
            this.message = message;
        }
        public string AccountInfoSiteAppPath => UrlHelper.MapAccountInfoPath("~/");

        public string ActorLevelName => Enumeration.FromValue<UserLevel>(message.ActorRoleId).DisplayName;

        public string AccountInfoResources {
            get
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(message.Language);
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
                    if(count < length)
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

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

    }
}
