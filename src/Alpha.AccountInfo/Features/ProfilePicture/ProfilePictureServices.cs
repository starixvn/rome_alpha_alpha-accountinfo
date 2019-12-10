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

        Task<Result> UpdateActorProfilePictureAsync(UpdateActorProfilePictureParam param);
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
            try
            {
                var binaryProfilePictures = await dynamicRepository.FetchAsync<ProfilePictureDTO>(
                    new SelectProfilePictureLibraryCriteria(systemMode)).ConfigureAwait(false);
                return binaryProfilePictures == null ? Enumerable.Empty<ProfilePictureModel>() :
                    binaryProfilePictures.Select(profilePictureDto => new ProfilePictureModel(profilePictureDto.ImageId, profilePictureDto.Image));
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return Enumerable.Empty<ProfilePictureModel>();
            }
        }

        public async Task<Result> UpdateActorProfilePictureAsync(UpdateActorProfilePictureParam param)
        {
            var command = new UpdateActorProfilePictureCommand(
                        param.ActorId,
                        param.ImageId,
                        param.Image,
                        param.Mode,
                        param.IP);

            await dynamicRepository.ExecuteAsync(command).ConfigureAwait(false);

            return command.ErrCode.Equals(0) ?
                Result.CreateSuccessResult() :
                Result.CreateErrorResult(command.ErrCode, command.ErrMsg);
        }
    }
}