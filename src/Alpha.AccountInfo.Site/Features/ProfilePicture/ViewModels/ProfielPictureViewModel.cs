namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;

    public class ProfielPictureViewModel
    {
        private readonly Func<string, string, string> GetMicroSiteUrl;

        public ProfielPictureViewModel(
            IEnumerable<string> images,
            string actorAvatar,
            Func<string, string, string> getMicroSiteUrl = null)
        {
            ActorAvatar = actorAvatar;
            Images = images;
            GetMicroSiteUrl = getMicroSiteUrl ?? SimpleServiceLocator.GetService<Func<string, string, string>>();
        }

        public string ActorAvatar { get; private set; }

        public IEnumerable<string> Images { get; private set; }

        public string AccountInfoUrl => GetMicroSiteUrl(MicroSiteConstants.AccountInfo, "~/");
    }
}