namespace Alpha.AccountInfo.Features.Shared.Services
{
    using System;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.Features.Shared.Params;
    using Alpha.AccountInfo.Features.Shared.Params.SPs;
    using Fanex.Data.Repository;
    using Fanex.Logging.Extensions.Web;

    public interface KickoutCustomerService
    {
        Task KickoutDueToProtectedStatusAsync(int custId, byte roleId, int serverId, string ip);

        Task KickoutAsync(KickoutCustomerParam param);
    }

    public class KickoutCustomerServiceImpl : KickoutCustomerService
    {
        private readonly IDynamicRepository dynamicRepository;

        public KickoutCustomerServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public Task KickoutDueToProtectedStatusAsync(int custId, byte roleId, int serverId, string ip)
            => KickoutAsync(new KickoutCustomerParam(custId, roleId, serverId, ip, 0, "ChangeProtectionMode"));

        public async Task KickoutAsync(KickoutCustomerParam param)
        {
            try
            {
                if (!AppSettings.IsDisabledRecheckin)
                {
                    await dynamicRepository.ExecuteAsync(new KickoutCustomerCriteria(
                        param.CustId,
                        param.CustLevelId,
                        param.ServerId,
                        param.IP,
                        param.SubAccId,
                        param.Mode));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
