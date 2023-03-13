namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Resources;
    using Alpha.AccountInfo.Features.ProfilePicture.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Models;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using Alpha.Web.Helpers;

    public class ProfielPictureViewModel
    {
        private readonly Func<string, ResourceManager, string> ConvertResourcesJson;
        private readonly string Language;

        public ProfielPictureViewModel(
            IEnumerable<ProfilePictureModel> images,
            byte actorRoleId,
            string language,
            bool allowUpdate,
            bool enableSecurityCode,
            bool isOfferProtectedMode,
            string displayLanguage,
            IEnumerable<KeyValuePair<string, string>> languages,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            Images = images;
            Language = language;
            AllowUpdate = allowUpdate;
            ActorLevel = Enumeration.FromValue<UserLevel>(actorRoleId).DisplayName;
            ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            EnableSecurityCode = enableSecurityCode;
            IsOfferProtectedMode = isOfferProtectedMode;
            DisplayLanguage = displayLanguage;
            Languages = languages;
        }

        public IEnumerable<ProfilePictureModel> Images { get; private set; }

        public string ActorLevel { get; private set; }

        public bool AllowUpdate { get; private set; }

        public string Resources => ConvertResourcesJson(Language, ProfilePictureResources.ResourceManager);

        public string SetUpOTPLink => MicroSites.MainUrl("~/Otp/SetupOtp");

        public string ChangePasswordLink => MicroSites.MainUrl("~/Password/ChangePassword");

        public string SecurityCodeLink => MicroSites.MainUrl("~/SecurityCode/ChangeUserOwnSecurityCode");

        public string ProtectionCodeLink => MicroSites.MainUrl("~/ProtectionCode/Setup");

        public bool EnableSecurityCode { get; }

        public bool IsOfferProtectedMode { get; }

        public string DisplayLanguage { get; }

        public IEnumerable<KeyValuePair<string, string>> Languages { get; }
    }
}