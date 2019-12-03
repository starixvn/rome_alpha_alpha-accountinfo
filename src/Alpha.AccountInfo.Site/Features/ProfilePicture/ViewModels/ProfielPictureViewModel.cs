namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    using Alpha.AccountInfo.Features.ProfilePicture.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Models;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using System;
    using System.Collections.Generic;
    using System.Resources;

    public class ProfielPictureViewModel
    {
        private readonly Func<string, string, string> GetMicroSiteUrl;
        private readonly Func<string, ResourceManager, string> ConvertResourcesJson;
        private readonly string Language;

        public ProfielPictureViewModel(
            IEnumerable<ProfilePictureModel> images,
            byte actorRoleId,
            string language,
            bool allowUpdate,
            Func<string, ResourceManager, string> convertResourcesJson = null,
            Func<string, string, string> getMicroSiteUrl = null)
        {
            Images = images;
            Language = language;
            AllowUpdate = allowUpdate;
            ActorLevel = Enumeration.FromValue<UserLevel>(actorRoleId).DisplayName;
            ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            GetMicroSiteUrl = getMicroSiteUrl ?? SimpleServiceLocator.GetService<Func<string, string, string>>();
        }

        public IEnumerable<ProfilePictureModel> Images { get; private set; }

        public string ActorLevel { get; private set; }

        public bool AllowUpdate { get; private set; }

        public string Resources => ConvertResourcesJson(Language, ProfilePictureResources.ResourceManager);

        public string SetUpOTPLink => $"{GetMicroSiteUrl(MicroSiteConstants.Main, "~/")}/Otp/SetupOtp";
    }
}