namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.AppResources;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.Products.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;

    public class UnsuspendSingleViewModel
    {
        public UnsuspendSingleViewModel(
            string language,
            UnsuspendSingleDataViewModel data,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            Data = data;

            var ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            Resources = ConvertResourcesJson(language, UnsuspendDownlinesResources.ResourceManager);
        }

        public UnsuspendSingleDataViewModel Data{ get; private set; }

        public string Resources { get; private set; }
    }

    public class UnsuspendSingleDataViewModel : AccountInfoViewModelBase
    {
        public UnsuspendSingleDataViewModel(
            int searchAccountId,
            string searchAccountName,
            IEnumerable<UnsuspendSingleModel> levels)
        {
            SearchAccountId = searchAccountId;
            SearchAccountName = searchAccountName;
            Levels = levels.OrderByDescending(level => level.RoleId);
        }

        public int SearchAccountId { get; private set; }

        public string SearchAccountName { get; private set; }

        public IEnumerable<UnsuspendSingleModel> Levels { get; private set; }

        public int AffectedDownlinesTotal => Levels.Sum(item => item.AffectedDownlines);

        public int UnchangedDownlinesTotal => Levels.Sum(item => item.UnchangedDownlines);

    }
}