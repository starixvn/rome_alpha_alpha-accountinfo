namespace Alpha.AccountInfo.ProfilePicture.Services
{
    using Alpha.AccountInfo.Features.ProfilePicture.Models;
    using Alpha.AccountInfo.Features.ProfilePicture.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ProfilePictureServices
    {
        Result UpdateManualProfilePictureCommand(byte[] image, string filename);

        Task<ActorProfilePictureModel> GetActorProfilePicture(int custId);

        Task<IEnumerable<ProfilePictureModel>> GetProfilePictureLibrary();

        Task<Result> UpdateActorProfilePicture(UpdateActorProfilePictureParam param);
    }

    public class ProfilePictureServicesImpl : ProfilePictureServices
    {
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

        public async Task<ActorProfilePictureModel> GetActorProfilePicture(int custId)
        => await dynamicRepository.GetAsync<ActorProfilePictureModel>(new SelectActorProfilePictureCriteria(custId)).ConfigureAwait(false);

        public async Task<IEnumerable<ProfilePictureModel>> GetProfilePictureLibrary()
          => await dynamicRepository.FetchAsync<ProfilePictureModel>(new SelectProfilePictureLibraryCriteria()).ConfigureAwait(false);

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