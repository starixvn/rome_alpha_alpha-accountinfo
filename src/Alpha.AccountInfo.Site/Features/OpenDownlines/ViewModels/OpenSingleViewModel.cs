namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.OpenDownlines.AppResources;
    using Alpha.AccountInfo.Features.OpenDownlines.Models;
    using Alpha.Products.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;

    public class OpenSingleViewModel
    {
        public OpenSingleViewModel(
            string language,
            OpenSingleDataViewModel data,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            Data = data;

            var ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            Resources = ConvertResourcesJson(language, OpenDownlinesResources.ResourceManager);
        }

        public OpenSingleDataViewModel Data { get; private set; }

        public string Resources { get; private set; }
    }

    public class OpenSingleDataViewModel : AccountInfoViewModelBase
    {
        public OpenSingleDataViewModel(
            int searchAccountId,
            string searchAccountName,
            IEnumerable<OpenSingleModel> levels)
        {
            SearchAccountId = searchAccountId;
            SearchAccountName = searchAccountName;
            Levels = levels.OrderByDescending(level => level.RoleId);
        }

        public int SearchAccountId { get; }

        public string SearchAccountName { get; }

        public IEnumerable<OpenSingleModel> Levels { get; }

        public int AffectedDownlinesTotal => Levels.Sum(item => item.AffectedDownlines);

        public int UnchangedDownlinesTotal => Levels.Sum(item => item.UnchangedDownlines);
    }
}