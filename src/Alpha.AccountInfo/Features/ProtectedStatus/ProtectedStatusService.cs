namespace Alpha.AccountInfo.Features.ProtectedStatus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.ProtectedStatus.AppResources;
    using Alpha.AccountInfo.Features.ProtectedStatus.Models;
    using Alpha.AccountInfo.Features.ProtectedStatus.Params;
    using Alpha.AccountInfo.Features.ProtectedStatus.Params.SPs;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Fanex.Data.Repository;

    public interface ProtectedStatusService
    {
        Task<IEnumerable<CustomerProtectedStatus>> GetProtectedStatusAsync(GetProtectedStatusParam param);

        Task<IEnumerable<Result>> UpdateMultipleAsync(UpdateMultipleProtectedStatusParam param);
    }

    public class ProtectedStatusServiceImpl : ProtectedStatusService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly SearchAccountService searchAccountService;
        private readonly KickoutCustomerService kickoutCustomerService;

        public ProtectedStatusServiceImpl(
            IDynamicRepository dynamicRepository,
            SearchAccountService searchAccountService,
            KickoutCustomerService kickoutCustomerService)
        {
            this.dynamicRepository = dynamicRepository;
            this.searchAccountService = searchAccountService;
            this.kickoutCustomerService = kickoutCustomerService;
        }

        public Task<IEnumerable<CustomerProtectedStatus>> GetProtectedStatusAsync(GetProtectedStatusParam param)
            => dynamicRepository.FetchAsync<CustomerProtectedStatus>(new GetProtectedStatusCriteria(param));

        public async Task<IEnumerable<Result>> UpdateMultipleAsync(UpdateMultipleProtectedStatusParam param)
        {
            var isValidCheckMultipleCustomesBelonging = await CheckMultipleCustomesBelonging(param);

            return isValidCheckMultipleCustomesBelonging
                ? await UpdateMultipleProtectedStatusAsync(param)
                : new[] { Result.CreateForbiddenResult() };
        }

        private Task<bool> CheckMultipleCustomesBelonging(UpdateMultipleProtectedStatusParam param)
        {
            var custIds = string.Join("^", param.ProtectedStatus.Select(p => p.CustId));

            return searchAccountService.CheckMultipleCustomesBelonging(
                param.ActorId, param.SubAccId, custIds, param.IsInternal);
        }

        private async Task<IEnumerable<Result>> UpdateMultipleProtectedStatusAsync(UpdateMultipleProtectedStatusParam param)
        {
            Result result;
            IList<Result> errorResults = new List<Result>();

            foreach (var item in param.ProtectedStatus)
            {
                result = await searchAccountService.UpdateCustomerStatus(
                    new UpdateCustomerStatusParam(
                        item.CustId,
                        param.ActorId,
                        param.SubAccId,
                        param.LangKey,
                        param.IP,
                        item.IsProtectedMode,
                        StatusNames.Protected,
                        param.ActorName));

                if (result.IsSuccess)
                {
                    if (item.IsProtectedMode)
                    {
                        await kickoutCustomerService.KickoutDueToProtectedStatusAsync(item.CustId, item.RoleId, param.ServerId, param.IP);
                    }
                }
                else
                {
                    errorResults.Add(Result.CreateErrorResult(result.Code, $"{item.Username}: {result.Message}"));
                }
            }

            return BuildResults(param.ProtectedStatus.Length, errorResults);
        }

        private static IEnumerable<Result> BuildResults(int total, IList<Result> errorResults)
        {
            var errorCount = errorResults.Count;

            if (errorCount == 0)
            {
                errorResults.Add(Result.CreateSuccessResult(ProtectedStatusResources.EditMultipleAllSuccessfully));
            }
            else if (errorCount < total)
            {
                errorResults.Insert(0, Result.CreateSuccessResult(ProtectedStatusResources.EditMultiplePartialSuccessfully));
            }

            return errorResults;
        }
    }
}
