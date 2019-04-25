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
            IEnumerable<UnsuspendInfoModel> unsuspendByLevel)
        {

            SearchAccountId = searchAccountId;
            UnsuspendByLevel = unsuspendByLevel;
        }

        public int SearchAccountId { get; private set; }

        public IEnumerable<UnsuspendInfoModel> UnsuspendByLevel { get; private set; }

        public int AffectedDownlinesTotal => UnsuspendByLevel.Sum(item => item.AffectedDownlines);

        public int UnchangedDownlinesTotal => UnsuspendByLevel.Sum(item => item.UnchangedDownlines);

    }
}