namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.AppResources;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using Alpha.Products.Core.Services;
    using Alpha.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;

    public class SuspendMultipleConfirmViewModel
    {

        public SuspendMultipleConfirmViewModel(SuspendMultipleConfirmRequest message, Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            Data = new SuspendMultipleConfirmDataViewModel(message);
            var ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            Resources = ConvertResourcesJson(message.Language, UnsuspendDownlinesResources.ResourceManager);
        }

        
        public string Resources { get; private set; }
        public SuspendMultipleConfirmDataViewModel Data { get; private set; }

    }

    public class SuspendMultipleConfirmDataViewModel
    {
        private const int numberOfDisplayDownlines = 10;

        public SuspendMultipleConfirmDataViewModel(SuspendMultipleConfirmRequest message)
        {
            Ids = message.Ids;
            RoleId = message.RoleId;
            UserNames = message.UserNames;
            selectedDownlineUsernames = message.UserNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            topDownlineUsernames = selectedDownlineUsernames.Take(numberOfDisplayDownlines);
            DisplayShowAllAccountOption = selectedDownlineUsernames.Count() > numberOfDisplayDownlines;
            MoreDownlineUsernames = selectedDownlineUsernames.Skip(numberOfDisplayDownlines);
            UnsuspendList = getUnsuspends();
        }

        private IEnumerable<UnsuspendCustomerInfo> getUnsuspends()
        {
            var idArray = Ids.Split(',');
            var userArray = UserNames.Split(',');
            var result = new List<UnsuspendCustomerInfo>();
            for(var i=0; i<userArray.Length;i++)
            {
                result.Add(new UnsuspendCustomerInfo()
                {
                    SearchAccountId = int.Parse(idArray[i]),
                    Username = userArray[i],
                    Suspended = true
                });
            }
            return result;
        }

        private IEnumerable<string> selectedDownlineUsernames { get; }
        
        private IEnumerable<string> topDownlineUsernames { get; }

        public IEnumerable<UnsuspendCustomerInfo> UnsuspendList { get; set; }

        public bool DisplayShowAllAccountOption { get; }

        public IEnumerable<string> MoreDownlineUsernames { get; }

        public string Ids { get; }
        
        public string UserNames { get; }

        public byte RoleId { get; }

        public UserLevel Role => Enumeration.FromValue<UserLevel>(RoleId);

        public string ConfirmMessage => $"{UnsuspendDownlinesResources.SelectedAccounts} {string.Join(", ", topDownlineUsernames)}";

        public string ShowMoreText => string.Format(UnsuspendDownlinesResources.ShowAllAccounts, selectedDownlineUsernames.Count());
    }
}
