namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.AppResources;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.Products.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Resources;

    public class UnsuspendMultipleViewModel
    {
        public UnsuspendMultipleViewModel(
            string language,
            IEnumerable<UnsuspendMultipleModel> data,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            Data = data;

            var ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            Resources = ConvertResourcesJson(language, UnsuspendDownlinesResources.ResourceManager);
        }

        public IEnumerable<UnsuspendMultipleModel> Data { get; private set; }

        public string Resources { get; private set; }
    }
}
