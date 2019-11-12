namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    using Alpha.AccountInfo.Features.ProfilePicture.AppResources;
    using Alpha.AccountInfo.Features.ProfilePicture.Models;
    using Alpha.Products.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Resources;

    public class ProfielPictureViewModel
    {
        public ProfielPictureViewModel(
            IEnumerable<ProfilePictureModel> images,
            string language,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            Images = images;

            var ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            Resources = ConvertResourcesJson(language, ProfilePictureResources.ResourceManager);
        }

        public IEnumerable<ProfilePictureModel> Images { get; private set; }

        public string Resources { get; private set; }
    }
}