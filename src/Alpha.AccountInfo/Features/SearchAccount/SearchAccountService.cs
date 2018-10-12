namespace Alpha.AccountInfo.Services.AccountInfo
{
    using System;
    using System.Collections.Generic;
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
        IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal);

        CustomerInfoModel GetCustomerInfoModel(string username, int uplineId);

        IBalanceInfoModel GetBalanceInfoModel(int custId, byte roleId);

        DownlineListModel GetDownlineListModel(int custId);

        CustomerStatusModel GetCustomerAllStatusModel(int custId);

        IEnumerable<OnlineUserModel> GetOnlineListModel(int custid);

        Result UpdateGivenCredit(UpdateGivenCreditParam param);

        Result UpdateCustomerStatus(UpdateCustomerStatusParam param);
    }

    public class SearchAccountServiceImpl : SearchAccountService
    {
        private readonly int failErrorCode = 100;

        private readonly IDynamicRepository dynamicRepository;
        private readonly SearchAccountDataAccess accountInfoDataAccess;
        private readonly RnGCasinoService rnGCasinoService;
        private readonly IDictionary<UserLevel, Func<ICriteria, IBalanceInfoModel>> getBalanceInfoModelByLevel;

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
            IDictionary<UserLevel, Func<ICriteria, IBalanceInfoModel>> getBalanceInfoModelByLevel)
        {
            this.dynamicRepository = dynamicRepository;
            this.accountInfoDataAccess = accountInfoDataAccess;
            this.rnGCasinoService = rnGCasinoService;

            this.getBalanceInfoModelByLevel = getBalanceInfoModelByLevel ??
                new Dictionary<UserLevel, Func<ICriteria, IBalanceInfoModel>>
                {
                    {UserLevel.Master, GetMasterBalanceInfoModel},
                    {UserLevel.Agent, GetAgentBalanceInfoModel},
                    {UserLevel.Member, GetMemberBalanceInfoModel}
                };
        }

        public IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal)
            => dynamicRepository.Fetch<UsernameSuggestion>(new SelectAutoSuggestionCriteria(actorId, suggestionQuery, isInternal));

        public CustomerInfoModel GetCustomerInfoModel(string username, int uplineId)
            => dynamicRepository.Get<CustomerInfoModel>(new SelectCustomerInfoCriteria(username, uplineId));

        public IBalanceInfoModel GetBalanceInfoModel(int custId, byte roleId)
           => getBalanceInfoModelByLevel[Enumeration.FromValue<UserLevel>(roleId)](new SelectBalanceInfoCriteria(custId, roleId));

        public CustomerStatusModel GetCustomerAllStatusModel(int custId)
          => accountInfoDataAccess.GetCustomerAllStatus(new SelectCustomerAllStatusModelCriteria(custId));

        public DownlineListModel GetDownlineListModel(int custId)
            => accountInfoDataAccess.GetDownlineList(new SelectDownlineListCriteria(custId));

        public IEnumerable<OnlineUserModel> GetOnlineListModel(int custid)
            => dynamicRepository.Fetch<OnlineUserModel>(new SelectOnlineListCriteria(custid));

        public Result UpdateGivenCredit(UpdateGivenCreditParam param)
        {
            var command = new UpdateGivenCreditCommand(
                    param.SearchAccountId,
                    param.RoleId,
                    param.CreditAmount,
                    param.ActorId,
                    param.SubAccountId,
                    param.Language,
                    param.IP);

            dynamicRepository.Execute(command);

            return command.CreditProblem
                ? Result.CreateErrorResult(failErrorCode, command.ErrMsg)
                : Result.CreateSuccessResult();
        }

        public Result UpdateCustomerStatus(UpdateCustomerStatusParam param)
        {
            var command = new UpdateCustomerStatusCommand(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.Language,
                   param.IP,
                   param.StatusName,
                   param.StatusValue);

            var result = UpdateCustomerStatus(command);

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

        internal IBalanceInfoModel GetMasterBalanceInfoModel(ICriteria criteria)
          => dynamicRepository.Get<MasterBalanceInfoModel>(criteria);

        internal IBalanceInfoModel GetAgentBalanceInfoModel(ICriteria criteria)
          => dynamicRepository.Get<AgentBalanceInfoModel>(criteria);

        internal IBalanceInfoModel GetMemberBalanceInfoModel(ICriteria criteria)
          => dynamicRepository.Get<MemberBalanceInfoModel>(criteria);

        public Result UpdateCustomerStatus(UpdateCustomerStatusCommand command)
        {
            var result = accountInfoDataAccess.UpdateCustomerStatus(command);

            return result.IsSuccess
                ? Result.CreateSuccessResult()
                : result;
        }
    }
}