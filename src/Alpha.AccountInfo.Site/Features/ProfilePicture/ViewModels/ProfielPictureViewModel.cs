namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    using System;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;

    public class ProfielPictureViewModel
    {
        private readonly Func<string, string, string> GetMicroSiteUrl;

        public ProfielPictureViewModel(Func<string, string, string> getMicroSiteUrl = null)
        {
            GetMicroSiteUrl = getMicroSiteUrl ?? SimpleServiceLocator.GetService<Func<string, string, string>>();
        }

        public string AccountInfoUrl => GetMicroSiteUrl(MicroSiteConstants.AccountInfo, "~/");
    }
}