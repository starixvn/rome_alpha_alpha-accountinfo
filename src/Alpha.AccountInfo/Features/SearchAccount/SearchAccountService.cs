namespace Alpha.AccountInfo.Services.AccountInfo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.Products.Core.Shared;
    using Fanex.Data.Repository;

    public interface SearchAccountService
    {
        Task<IEnumerable<UsernameSuggestion>> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal);

        Task<CustomerInfoModel> GetCustomerInfo(string username, int uplineId);

        Task<IBalanceInfoModel> GetBalanceInfo(int custId, byte roleId);

        Task<DownlineListModel> GetDownlineList(int custId, int pageIndex);

        Task<CustomerStatusModel> GetCustomerStatus(int custId);

        Task<IEnumerable<OnlineUserModel>> GetOnlineList(int custid);

        Task<Result> UpdateGivenCredit(UpdateGivenCreditParam param);

        Task<Result> UpdateCustomerStatus(UpdateCustomerStatusParam param);
    }

    public class SearchAccountServiceImpl : SearchAccountService
    {
        private readonly int failErrorCode = 100;

        private readonly IDynamicRepository dynamicRepository;
        private readonly SearchAccountDataAccess accountInfoDataAccess;
        private readonly RnGCasinoService rnGCasinoService;
        private readonly IDictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>> getBalanceInfoModelByLevel;

        public SearchAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            SearchAccountDataAccess accountInfoDataAccess,
            RnGCasinoService rnGCasinoService)
            : this(dynamicRepository, accountInfoDataAccess, rnGCasinoService, null)
        {
        }

        internal SearchAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            SearchAccountDataAccess accountInfoDataAccess,
            RnGCasinoService rnGCasinoService,
            IDictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>> getBalanceInfoModelByLevel)
        {
            this.dynamicRepository = dynamicRepository;
            this.accountInfoDataAccess = accountInfoDataAccess;
            this.rnGCasinoService = rnGCasinoService;

            this.getBalanceInfoModelByLevel = getBalanceInfoModelByLevel ??
                new Dictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>>
                {
                    {UserLevel.Master, GetMasterBalanceInfoModel},
                    {UserLevel.Agent, GetAgentBalanceInfoModel},
                    {UserLevel.Member, GetMemberBalanceInfoModel}
                };
        }

        public Task<IEnumerable<UsernameSuggestion>> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal)
            => dynamicRepository.FetchAsync<UsernameSuggestion>(new SelectAutoSuggestionCriteria(actorId, suggestionQuery, isInternal));

        public Task<CustomerInfoModel> GetCustomerInfo(string username, int uplineId)
            => dynamicRepository.GetAsync<CustomerInfoModel>(new SelectCustomerInfoCriteria(username, uplineId));

        public Task<IBalanceInfoModel> GetBalanceInfo(int custId, byte roleId)
           => getBalanceInfoModelByLevel[Enumeration.FromValue<UserLevel>(roleId)](new SelectBalanceInfoCriteria(custId, roleId));

        public Task<CustomerStatusModel> GetCustomerStatus(int custId)
          => accountInfoDataAccess.GetCustomerStatus(new SelectCustomerAllStatusModelCriteria(custId));

        public Task<DownlineListModel> GetDownlineList(int custId, int pageIndex)
            => accountInfoDataAccess.GetDownlineList(new SelectDownlineListCriteria(custId, pageIndex));

        public Task<IEnumerable<OnlineUserModel>> GetOnlineList(int custid)
            => dynamicRepository.FetchAsync<OnlineUserModel>(new SelectOnlineListCriteria(custid));

        public async Task<Result> UpdateGivenCredit(UpdateGivenCreditParam param)
        {
            var command = new UpdateGivenCreditCommand(
                    param.SearchAccountId,
                    param.RoleId,
                    param.CreditAmount,
                    param.ActorId,
                    param.SubAccountId,
                    param.Language,
                    param.IP);

            await dynamicRepository.ExecuteAsync(command).ConfigureAwait(false);

            return command.CreditProblem
                ? Result.CreateErrorResult(failErrorCode, command.ErrMsg)
                : Result.CreateSuccessResult();
        }

        public async Task<Result> UpdateCustomerStatus(UpdateCustomerStatusParam param)
        {
            var command = new UpdateCustomerStatusCommand(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.Language,
                   param.IP,
                   param.StatusName,
                   param.StatusValue);

            var result = await UpdateCustomerStatus(command).ConfigureAwait(false);

            if (result.IsSuccess && param.SyncedCasino3rd)
            {
                rnGCasinoService.SyncedToRnGCasino3rdByStatus(
                    new UpdateRnGCasino3rdParam(
                        param.SearchAccountId,
                        param.StatusName,
                        param.StatusValue,
                        param.ActorId,
                        param.ActorName,
                        param.IP));
            }

            return result;
        }

        internal async Task<IBalanceInfoModel> GetMasterBalanceInfoModel(ICriteria criteria)
          => await dynamicRepository.GetAsync<MasterBalanceInfoModel>(criteria).ConfigureAwait(false);

        internal async Task<IBalanceInfoModel> GetAgentBalanceInfoModel(ICriteria criteria)
          => await dynamicRepository.GetAsync<AgentBalanceInfoModel>(criteria).ConfigureAwait(false);

        internal async Task<IBalanceInfoModel> GetMemberBalanceInfoModel(ICriteria criteria)
          => await dynamicRepository.GetAsync<MemberBalanceInfoModel>(criteria).ConfigureAwait(false);

        public async Task<Result> UpdateCustomerStatus(UpdateCustomerStatusCommand command)
        {
            var result = await accountInfoDataAccess.UpdateCustomerStatus(command).ConfigureAwait(false);

            return result.IsSuccess
                ? Result.CreateSuccessResult()
                : result;
        }
    }
}