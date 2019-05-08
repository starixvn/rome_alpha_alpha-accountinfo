namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.Products.Core.Shared;
    using Fanex.Data.Repository;
    using Criteria.ProductStatus;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface SearchAccountService
    {
        Task<IEnumerable<UsernameSuggestion>> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal);

        Task<CustomerInfoModel> GetCustomerInfo(string username, int uplineId, int roleId);

        Task<bool> UpdateUserSearch(int userId, string search, string firstName, string lastName, string nickname);

        Task<IBalanceInfoModel> GetBalanceInfo(int custId, byte roleId);

        Task<DownlineListModel> GetDownlineList(int custId, int pageIndex, byte roleId, byte statusId, int totalPages);

        Task<IEnumerable<TotalDownlinesSummary>> GetTotalDownlinesSummary(int custId, byte roleId);

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
        private readonly IDictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>> getBalanceInfoModelByLevel;

        public SearchAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            SearchAccountDataAccess accountInfoDataAccess)
            : this(dynamicRepository, accountInfoDataAccess, null)
        {
        }

        internal SearchAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            SearchAccountDataAccess accountInfoDataAccess,
            IDictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>> getBalanceInfoModelByLevel)
        {
            this.dynamicRepository = dynamicRepository;
            this.accountInfoDataAccess = accountInfoDataAccess;

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

        public async Task<CustomerInfoModel> GetCustomerInfo(string username, int uplineId, int roleId)
        { 
            var customerInfo = await dynamicRepository.GetAsync<CustomerInfoModel>(new SelectCustomerInfoCriteria(username, uplineId))
                .ConfigureAwait(false);
            if (customerInfo != null)
            {
                var nickname = roleId - customerInfo.RoleId == 1 ? customerInfo.UserName2 : string.Empty;
                await UpdateUserSearch(uplineId, customerInfo.UserName, customerInfo.FirstName, customerInfo.LastName, nickname)
                    .ConfigureAwait(false);
            }

            return customerInfo;
        }

        public Task<bool> UpdateUserSearch(int userId, string search, string firstName, string lastName, string nickname)
            => dynamicRepository.GetAsync<bool>(new UpdateUserSearchCriteria(userId, search, firstName, lastName, nickname));

        public Task<IBalanceInfoModel> GetBalanceInfo(int custId, byte roleId)
           => getBalanceInfoModelByLevel[Enumeration.FromValue<UserLevel>(roleId)](new SelectBalanceInfoCriteria(custId, roleId));

        public Task<CustomerStatusModel> GetCustomerStatus(int custId)
          => accountInfoDataAccess.GetCustomerStatus(new SelectCustomerAllStatusModelCriteria(custId));

        public Task<DownlineListModel> GetDownlineList(int custId, int pageIndex, byte roleId, byte statusId, int totalPages)
            => accountInfoDataAccess.GetDownlineList(new SelectDownlineListByStatusCriteria(custId, pageIndex, roleId, statusId, totalPages));

        public Task<IEnumerable<TotalDownlinesSummary>> GetTotalDownlinesSummary(int custId, byte roleId)
          => dynamicRepository.FetchAsync<TotalDownlinesSummary>(new SelectTotalDownlineSummaryCriteria(custId, roleId));

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
            => await UpdateCustomerStatus(new UpdateCustomerStatusCommand(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.Language,
                   param.IP,
                   param.StatusName,
                   param.StatusValue)).ConfigureAwait(false);

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