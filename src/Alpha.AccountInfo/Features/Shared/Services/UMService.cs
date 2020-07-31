namespace Alpha.AccountInfo.Features.Shared.Services
{
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.Shared.Constants;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Params;
    using Alpha.AccountInfo.Features.Shared.Params.SPs;
    using Fanex.Data.Repository;

    public interface UMService
    {
        Task<UMMessageItem> GetUMMessageAsync(CheckUMParam param);

        byte GetDCSSettingType(int productId);
    }

    public class UMServiceImpl : UMService
    {
        private readonly IDynamicRepository dynamicRepository;

        public UMServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<UMMessageItem> GetUMMessageAsync(CheckUMParam param)
            => await dynamicRepository.GetAsync<UMMessageItem>(new CheckUMCriteria(param));

        public byte GetDCSSettingType(int productId)
        {
            switch (productId)
            {
                case ProductIdConstants.NG2:
                    return 11;

                case ProductIdConstants.SabaVirtualSports:
                    return 4;

                default:
                    return 13;
            }
        }
    }
}
