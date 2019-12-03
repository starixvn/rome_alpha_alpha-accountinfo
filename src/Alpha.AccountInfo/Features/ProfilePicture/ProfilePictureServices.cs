namespace Alpha.AccountInfo.ProfilePicture.Services
{
    using Alpha.AccountInfo.Features.ProfilePicture.AppResources;
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
        Result UpdateManualProfilePictureCommand(byte[] image, bool isMBC);

        Task<IEnumerable<ProfilePictureModel>> FectchProfilePictureLibraryAsync();

        Task<Result> UpdateActorProfilePictureAsync(UpdateActorProfilePictureParam param);
    }

    public class ProfilePictureServicesImpl : ProfilePictureServices
    {
        private const int ErrorCodeDefault = 100;
        private readonly IDynamicRepository dynamicRepository;
        private readonly Result ErrorDefaultResult =
            Result.CreateErrorResult(ErrorCodeDefault, ProfilePictureResources.ProfilePictureUpdatedFailed);

        public ProfilePictureServicesImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public Result UpdateManualProfilePictureCommand(byte[] image, bool isMBC)
        {
            try
            {
                var command = new UpdateManualProfilePictureCommand(image, isMBC);

                dynamicRepository.Execute(command);

                return Result.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return Result.CreateErrorResult(ErrorCodeDefault, ex.Message);
            }
        }

        public async Task<IEnumerable<ProfilePictureModel>> FectchProfilePictureLibraryAsync()
        {
            try
            {
                var binaryProfilePictures = await dynamicRepository.FetchAsync<ProfilePictureDTO>(new SelectProfilePictureLibraryCriteria()).ConfigureAwait(false);
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
            try
            {
                var command = new UpdateActorProfilePictureCommand(
                        param.ActorId,
                        param.ImageId,
                        param.Image,
                        param.IsDefault,
                        param.Mode,
                        param.IP);

                await dynamicRepository.ExecuteAsync(command).ConfigureAwait(false);

                return command.ErrCode.Equals(0) ?
                    Result.CreateSuccessResult() :
                    Result.CreateErrorResult(command.ErrCode, command.ErrMsg);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return ErrorDefaultResult;
            }
        }
    }
}