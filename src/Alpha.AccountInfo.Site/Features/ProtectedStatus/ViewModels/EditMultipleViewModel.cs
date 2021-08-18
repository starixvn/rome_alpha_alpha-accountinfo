namespace Alpha.AccountInfo.Site.Features.ProtectedStatus.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Resources;
    using Alpha.AccountInfo.Features.ProtectedStatus.AppResources;
    using Alpha.AccountInfo.Features.ProtectedStatus.Models;
    using Alpha.Products.Core.Services;

    public class EditMultipleViewModel
    {
        public EditMultipleViewModel(
           IEnumerable<CustomerProtectedStatus> protectedStatus,
           string language,
           Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            ProtectedStatus = protectedStatus;

            var ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            Resources = ConvertResourcesJson(language, ProtectedStatusResources.ResourceManager);
        }

        public IEnumerable<CustomerProtectedStatus> ProtectedStatus { get; }

        public string Resources { get; }
    }
}