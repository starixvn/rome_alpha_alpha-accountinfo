namespace Alpha.AccountInfo.ProfilePicture.Services
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Models;
    using Alpha.AccountInfo.ProfilePicture.Params;
    using Alpha.AccountInfo.ProfilePicture.Params.SPs.Commands;
    using Alpha.AccountInfo.ProfilePicture.Params.SPs.Criteria;
    using Fanex.Data.Repository;
    using Fanex.Logging.Extensions.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ProfilePictureServices
    {
        Task<IEnumerable<ProfilePictureModel>> FectchProfilePictureLibraryAsync(int systemMode);

        Task<Result> UpdateActorProfilePictureMainServerAsync(UpdateActorProfilePictureParam param);
    }

    public class ProfilePictureServicesImpl : ProfilePictureServices
    {
        private readonly IDynamicRepository dynamicRepository;

        public ProfilePictureServicesImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<ProfilePictureModel>> FectchProfilePictureLibraryAsync(int systemMode)
        {
            var binaryProfilePictures = await dynamicRepository.FetchAsync<ProfilePictureDTO>(
                new SelectProfilePictureLibraryCriteria(systemMode)).ConfigureAwait(false);

            return binaryProfilePictures.Select(profilePictureDto => new ProfilePictureModel(profilePictureDto.ImageId, profilePictureDto.Image));
        }

        public async Task<Result> UpdateActorProfilePictureMainServerAsync(UpdateActorProfilePictureParam param)
        {
            var command = new UpdateActorProfilePictureMainServerCommand(
                        param.ActorId,
                        param.ImageId,
                        param.Image,
                        param.Mode,
                        param.IP);

            await dynamicRepository.ExecuteAsync(command).ConfigureAwait(false);

            if(command.ErrCode.Equals(0))
            {
                await UpdateActorProfilePictureStandbyServerAsync(param).ConfigureAwait(false);
                return Result.CreateSuccessResult();
            }

            return Result.CreateErrorResult(command.ErrCode, command.ErrMsg);
        }

        internal async Task UpdateActorProfilePictureStandbyServerAsync(UpdateActorProfilePictureParam param)
        {
            try
            {
                var command = new UpdateActorProfilePictureStandbyServerCommand(
                            param.ActorId,
                            param.ImageId,
                            param.Image,
                            param.Mode,
                            param.IP);

                await dynamicRepository.ExecuteAsync(command).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}