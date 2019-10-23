namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Features.ProfilePicture.Models;
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

        Task<IEnumerable<DefaultProfilePicture>> GetProfilePictureLibrary();
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

        public async Task<IEnumerable<DefaultProfilePicture>> GetProfilePictureLibrary()
          => await dynamicRepository.FetchAsync<DefaultProfilePicture>(new SelectProfilePictureLibraryCriteria()).ConfigureAwait(false);
    }
}