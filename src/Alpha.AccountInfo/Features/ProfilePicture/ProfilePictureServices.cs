namespace Alpha.AccountInfo.ProfilePicture.Services
{
    using Alpha.AccountInfo.Features.ProfilePicture.Models;
    using Alpha.AccountInfo.Features.ProfilePicture.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ProfilePictureServices
    {
        Result UpdateManualProfilePictureCommand(byte[] image, string filename);

        Task<ProfilePictureModel> GetActorProfilePicture(int custId);

        Task<IEnumerable<ProfilePictureModel>> GetProfilePictureLibrary();

        Task<Result> UpdateActorProfilePicture(UpdateActorProfilePictureParam param);
    }

    public class ProfilePictureServicesImpl : ProfilePictureServices
    {
        private const string ProfilePictureLibaryKey = "Alpha.AccountInfo.Site.Features.ProfilePicture.Library";
        private readonly IDynamicRepository dynamicRepository;

        public ProfilePictureServicesImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public Result UpdateManualProfilePictureCommand(byte[] image, string filename)
        {
            try
            {
                var command = new UpdateManualProfilePictureCommand(image);

                 dynamicRepository.Execute(command);

                return Result.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return Result.CreateErrorResult(100, filename);
            }
        }

        public async Task<ProfilePictureModel> GetActorProfilePicture(int custId)
        {
            var actorProfilePicture = await dynamicRepository.GetAsync<ActorProfilePictureModel>(new SelectActorProfilePictureCriteria(custId)).ConfigureAwait(false);
            return new ProfilePictureModel(actorProfilePicture.Image);
        }

        public async Task<IEnumerable<ProfilePictureModel>> GetProfilePictureLibrary()
            => await await ConfiguredCacheService.GetOrSetAsync(
                    ProfilePictureLibaryKey,
                    async () => await FectchProfilePictureLibrary().ConfigureAwait(false)).ConfigureAwait(false);

        internal async Task<IEnumerable<ProfilePictureModel>> FectchProfilePictureLibrary()
        {
            var binaryProfilePictures = await dynamicRepository.FetchAsync<ProfilePictureDTO>(new SelectProfilePictureLibraryCriteria()).ConfigureAwait(false);
            return binaryProfilePictures.Select(profilePictureDto => new ProfilePictureModel(profilePictureDto));
        }

        public async Task<Result> UpdateActorProfilePicture(UpdateActorProfilePictureParam param)
        {
            var command = new UpdateActorProfilePictureCommand(
                    param.ActorId,
                    param.ImageId,
                    param.Image,
                    param.IsDefault,
                    param.IP);

            await dynamicRepository.ExecuteAsync(command).ConfigureAwait(false);

            return command.ErrCode.Equals(0) ?
                Result.CreateSuccessResult() :
                Result.CreateErrorResult(command.ErrCode, command.ErrMsg);
        }
    }
}