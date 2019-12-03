namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Models;
    using Alpha.AccountInfo.ProfilePicture.Params;
    using Alpha.AccountInfo.ProfilePicture.Services;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.Products.Core.Services;
    using Fanex.Caching;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, ProfielPictureViewModel>,
        IAsyncRequestHandler<ProfilePictureLibraryQuery, IEnumerable<ProfilePictureModel>>,
        IAsyncRequestHandler<UpdateActorProfilePictureQuery, Result>
    {
        private const string ProfilePictureLibaryKey = "Alpha.AccountInfo.Site.Features.ProfilePicture.Library";
        private readonly ProfilePictureServices profilePictureServices;
        private readonly Func<int> getInsertProfilePictureSiteMode;

        public ProfilePictureHandler(ProfilePictureServices profilePictureServices)
            : this(profilePictureServices, null)
        {
        }

        internal ProfilePictureHandler(
            ProfilePictureServices profilePictureServices,
            Func<int> getInsertProfilePictureSiteMode)
        {
            this.profilePictureServices = profilePictureServices;
            this.getInsertProfilePictureSiteMode = getInsertProfilePictureSiteMode ??
             SimpleServiceLocator.GetService<Func<int>>();
        }

        public Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
            => Task.FromResult(new ProfielPictureViewModel(message.Language, message.AllowUpdate));

        public Task<IEnumerable<ProfilePictureModel>> Handle(ProfilePictureLibraryQuery message)
            => profilePictureServices.FectchProfilePictureLibraryAsync();

        public Task<Result> Handle(UpdateActorProfilePictureQuery message)
          => profilePictureServices.UpdateActorProfilePictureAsync(
              new UpdateActorProfilePictureParam(
                  message.ActorId,
                  message.ImageId,
                  message.ImageBinary,
                  getInsertProfilePictureSiteMode(),
                  false,
                  message.IP));
    }
}