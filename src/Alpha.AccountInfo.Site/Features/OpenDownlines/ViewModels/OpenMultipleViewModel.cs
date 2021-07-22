namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.OpenDownlines.AppResources;
    using Alpha.AccountInfo.Features.OpenDownlines.Models;
    using Alpha.Products.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Resources;

    public class OpenMultipleViewModel
    {
        public OpenMultipleViewModel(
            byte searchAccountRoleId,
            string language,
            IEnumerable<OpenMultipleModel> data,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            SearchAccountRoleId = searchAccountRoleId;
            Data = data;

            var ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            Resources = ConvertResourcesJson(language, OpenDownlinesResources.ResourceManager);
        }

        public byte SearchAccountRoleId { get; }

        public IEnumerable<OpenMultipleModel> Data { get; }

        public string Resources { get; }
    }
}