namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Models;
    using Alpha.AccountInfo.ProfilePicture.Params;
    using Alpha.AccountInfo.ProfilePicture.Services;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.Products.Core.Services;
    using Alpha.SecurityCode.Services;
    using Fanex.Caching;
    using JW;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, ProfielPictureViewModel>,
        IAsyncRequestHandler<ProfilePictureLibraryQuery, ProfielPictureLibraryViewModel>,
        IAsyncRequestHandler<UpdateActorProfilePictureRequest, Result>
    {
        private const string ProfilePictureLibaryKey = "Alpha.AccountInfo.Site.Features.ProfilePicture.Library";
        private const int NumerousOfDefaultPictures = 7;
        private readonly ProfilePictureServices profilePictureServices;
        private readonly Func<int> getInsertProfilePictureSiteMode;
        private readonly SecurityCodeChecker securityCodeChecker;

        public ProfilePictureHandler(SecurityCodeChecker securityCodeChecker,ProfilePictureServices profilePictureServices)
            : this(securityCodeChecker,profilePictureServices, null)
        {
        }

        internal ProfilePictureHandler(
            SecurityCodeChecker securityCodeChecker,
            ProfilePictureServices profilePictureServices,
            Func<int> getInsertProfilePictureSiteMode)
        {
            this.securityCodeChecker = securityCodeChecker;
            this.profilePictureServices = profilePictureServices;
            this.getInsertProfilePictureSiteMode = getInsertProfilePictureSiteMode ??
             SimpleServiceLocator.GetService<Func<int>>();
        }

        public async Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
        {
            var isEnableSecurityCode = securityCodeChecker.IsEnableSecurityCode();
            var imageLibrary = await GetProfilePictureLibraryAsync().ConfigureAwait(false);

            return new ProfielPictureViewModel(
                imageLibrary.OrderBy(n => Guid.NewGuid()).Take(NumerousOfDefaultPictures),
                message.ActorRoleId,
                message.Language,
                message.AllowUpdate,
                isEnableSecurityCode,
                message.IsOfferProtectedMode);
        }

        public async Task<ProfielPictureLibraryViewModel> Handle(ProfilePictureLibraryQuery message)
        {
            var images = await GetProfilePictureLibraryAsync().ConfigureAwait(false);
            var pager = new Pager(totalItems: images.Count(), currentPage: message.CurrentPage);

            var currentPageItems = images.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
            return new ProfielPictureLibraryViewModel(currentPageItems, pager);
        }

        public Task<Result> Handle(UpdateActorProfilePictureRequest message)
          => profilePictureServices.UpdateActorProfilePictureMainServerAsync(
              new UpdateActorProfilePictureParam(
                  message.ActorId,
                  message.ImageId,
                  message.ImageBinary,
                  getInsertProfilePictureSiteMode(),
                  message.IP));

        internal async Task<IEnumerable<ProfilePictureModel>> GetProfilePictureLibraryAsync()
            => await await ConfiguredCacheService.GetOrSetAsync(
                    ProfilePictureLibaryKey,
                    async () => await profilePictureServices.FectchProfilePictureLibraryAsync(getInsertProfilePictureSiteMode()).ConfigureAwait(false)).ConfigureAwait(false);
    }
}