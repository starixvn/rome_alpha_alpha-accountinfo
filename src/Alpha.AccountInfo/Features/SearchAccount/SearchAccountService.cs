namespace Alpha.AccountInfo.Services.AccountInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.Products.Core.Shared;
    using Alpha.Utilities.IP;
    using Fanex.Data.Repository;

    public interface SearchAccountService
    {
        Task<CustomerInfoModel> GetCustomerInfo(string username, int uplineId, int roleId);

        Task<bool> UpdateUserSearch(int userId, string search, string firstName, string lastName, string nickname);

        Task<IBalanceInfoModel> GetBalanceInfo(int custId, byte roleId);

        Task<DownlineListModel> GetDownlineList(int custId, int pageIndex, byte roleId, byte statusId, int totalPages);

        Task<IEnumerable<TotalDownlinesSummary>> GetTotalDownlinesSummary(int custId, byte roleId);

        Task<IEnumerable<OnlineUserModel>> GetOnlineList(int custid, bool adminCanViewVNIP);

        Task<Result> UpdateGivenCredit(UpdateGivenCreditParam param);

        Task<Result> UpdateCustomerStatus(UpdateCustomerStatusParam param);

        Task<CreditInfo> GetCreditInfo(int custId);

        Task<Result> UpdateAllDayBalanceTransfer(UpdateAllDayBalanceTransferParam param);

        Task<bool> CheckFirstLoginByQRCode(int custId);

        Task<bool> CheckMultipleCustomesBelonging(int actorId, int subAccountId, string custIds, bool isInternal);

        Task<bool> IsAllDayBalanceTransfer(int custId);
    }

    public class SearchAccountServiceImpl : SearchAccountService
    {
        private readonly int failErrorCode = 100;

        private readonly IDynamicRepository dynamicRepository;
        private readonly SearchAccountDataAccess accountInfoDataAccess;
        private readonly IDictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>> getBalanceInfoModelByLevel;
        private readonly IPService ipService;

        public SearchAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            SearchAccountDataAccess accountInfoDataAccess,
            IPService ipService)
            : this(dynamicRepository, accountInfoDataAccess, ipService, null)
        {
        }

        internal SearchAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            SearchAccountDataAccess accountInfoDataAccess,
            IPService ipService,
            IDictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>> getBalanceInfoModelByLevel)
        {
            this.dynamicRepository = dynamicRepository;
            this.accountInfoDataAccess = accountInfoDataAccess;
            this.ipService = ipService;

            this.getBalanceInfoModelByLevel = getBalanceInfoModelByLevel ??
                new Dictionary<UserLevel, Func<ICriteria, Task<IBalanceInfoModel>>>
                {
                    {UserLevel.Master, GetMasterBalanceInfoModel},
                    {UserLevel.Agent, GetAgentBalanceInfoModel},
                    {UserLevel.Member, GetMemberBalanceInfoModel}
                };
        }

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

        public async Task<bool> UpdateUserSearch(int userId, string search, string firstName, string lastName, string nickname)
            => await dynamicRepository.GetAsync<bool>(
                new UpdateUserSearchCriteria(userId, search, firstName, lastName, nickname)).ConfigureAwait(false);

        public async Task<IBalanceInfoModel> GetBalanceInfo(int custId, byte roleId)
           => await getBalanceInfoModelByLevel[Enumeration.FromValue<UserLevel>(roleId)](
               new SelectBalanceInfoCriteria(custId, roleId)).ConfigureAwait(false);

        public async Task<DownlineListModel> GetDownlineList(int custId, int pageIndex, byte roleId, byte statusId, int totalPages)
            => await accountInfoDataAccess.GetDownlineList(
                new SelectDownlineListByStatusCriteria(custId, pageIndex, roleId, statusId, totalPages)).ConfigureAwait(false);

        public async Task<IEnumerable<TotalDownlinesSummary>> GetTotalDownlinesSummary(int custId, byte roleId)
          => await dynamicRepository.FetchAsync<TotalDownlinesSummary>(
              new SelectTotalDownlineSummaryCriteria(custId, roleId)).ConfigureAwait(false);

        public async Task<IEnumerable<OnlineUserModel>> GetOnlineList(int custid, bool adminCanViewVNIP)
        {
            if (AppSettings.IsDisabledRecheckin)
            {
                return Enumerable.Empty<OnlineUserModel>();
            }

            var onlineUsers = await dynamicRepository.FetchAsync<OnlineUserModel>(
                new SelectOnlineListCriteria(custid)).ConfigureAwait(false);

            if (!adminCanViewVNIP)
            {
                foreach (var item in onlineUsers)
                {
                    item.IP = ipService.IsVNIP(item.IP) ? string.Empty : item.IP;
                }
            }

            return onlineUsers;
        }

        public async Task<Result> UpdateGivenCredit(UpdateGivenCreditParam param)
        {
            UpdateSMAGivenCreditCommand command;
            if (param.RoleId == UserLevel.Member.Value)
            {
                var totalAmtCommand = new GetTotalAmtCommand(param.SearchAccountId);
                await dynamicRepository.ExecuteAsync(totalAmtCommand);
                command = new UpdateMemberGivenCreditCommand(
                    param.SearchAccountId,
                    param.RoleId,
                    param.CreditAmount,
                    param.ActorId,
                    param.SubAccountId,
                    param.Language,
                    param.IP,
                    totalAmtCommand.TotalAmt);
                await dynamicRepository.ExecuteAsync(command);
            }
            else
            {
                command = new UpdateSMAGivenCreditCommand(
                    param.SearchAccountId,
                    param.RoleId,
                    param.CreditAmount,
                    param.ActorId,
                    param.SubAccountId,
                    param.Language,
                    param.IP);

                await dynamicRepository.ExecuteAsync(command);
            }

            if (command.IsInvalid() || command.CreditProblem)
            {
                return Result.CreateErrorResult(failErrorCode, command.ErrMsg);
            }
            else
            {
                if (param.UpdateCreditReplicateServer)
                {
                    var replicateServerCommand = new UpdateGivenCreditReplicateServerCommand(param.SearchAccountId, param.CreditAmount);
                    await dynamicRepository.ExecuteAsync(replicateServerCommand).ConfigureAwait(false);
                }

                return Result.CreateSuccessResult();
            }
        }

        public async Task<Result> UpdateCustomerStatus(UpdateCustomerStatusParam param)
            => await UpdateCustomerStatus(new UpdateCustomerStatusCommand(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.Language,
                   param.IP,
                   param.StatusName,
                   param.StatusValue,
                   param.ActorName)).ConfigureAwait(false);

        internal async Task<IBalanceInfoModel> GetMasterBalanceInfoModel(ICriteria criteria)
          => await dynamicRepository.GetAsync<MasterBalanceInfoModel>(criteria).ConfigureAwait(false);

        internal async Task<IBalanceInfoModel> GetAgentBalanceInfoModel(ICriteria criteria)
          => await dynamicRepository.GetAsync<AgentBalanceInfoModel>(criteria).ConfigureAwait(false);

        internal async Task<IBalanceInfoModel> GetMemberBalanceInfoModel(ICriteria criteria)
        {
            var tasks = new List<Task>();

            var memberOutstandingCriteria = new SelectMemberOutstandingCriteria(((AccountInfoCriteriaBase)criteria).SearchAccountId);
            var memberBalanceCriteria = new SelectMemberBalanceCriteria(((AccountInfoCriteriaBase)criteria).SearchAccountId);
            var memberBalanceInfoDTOTask = dynamicRepository.GetAsync<MemberBalanceInfoDTO>(criteria);

            tasks.Add(dynamicRepository.ExecuteAsync(memberOutstandingCriteria));
            tasks.Add(dynamicRepository.ExecuteAsync(memberBalanceCriteria));
            tasks.Add(memberBalanceInfoDTOTask);

            await Task.WhenAll(tasks);

            return new MemberBalanceInfoModel(
                await memberBalanceInfoDTOTask,
                memberBalanceCriteria.Balance,
                memberBalanceCriteria.PendingTransfer3rd,
                memberOutstandingCriteria.Outstanding,
                memberBalanceCriteria.NetPosition);
        }

        public async Task<Result> UpdateCustomerStatus(UpdateCustomerStatusCommand command)
        {
            var result = await accountInfoDataAccess.UpdateCustomerStatus(command).ConfigureAwait(false);
            return result.IsSuccess
                ? Result.CreateSuccessResult()
                : result;
        }

        public async Task<CreditInfo> GetCreditInfo(int custId)
            => await dynamicRepository.GetAsync<CreditInfo>(new SelectCreditInfoCriteria(custId));

        public async Task<Result> UpdateAllDayBalanceTransfer(UpdateAllDayBalanceTransferParam param)
        {
            if (!await IsAllDayBalanceTransfer(param.ActorId))
            {
                const int DisallowedAllDayBalanceTransferError = 6001;
                return Result.CreateErrorResult(DisallowedAllDayBalanceTransferError, Resources.DisallowedAllDayBalanceTransferError);
            }

            var command = new UpdateAllDayBalanceTransferCommand(
                param.SearchAccountId,
                param.ActorId,
                param.Language,
                param.IP,
                param.StatusName,
                param.StatusValue);
            var result = await accountInfoDataAccess.UpdateAllDayBalanceTransfer(command);

            return result.IsSuccess ?
                Result.CreateSuccessResult() :
                result;
        }

        public async Task<bool> CheckFirstLoginByQRCode(int custId)
            => await dynamicRepository.GetAsync<bool>(
                new SelectFirstLoginByQRCodeCriteria(custId)).ConfigureAwait(false);

        public async Task<bool> CheckMultipleCustomesBelonging(int actorId, int subAccountId, string custIds, bool isInternal)
        {
            var criteria = new CheckMultipleCustomesBelongingCriteria(actorId, subAccountId, custIds);

            await dynamicRepository.ExecuteAsync(criteria).ConfigureAwait(false);

            return criteria.RoleId > 0 && (isInternal || criteria.IsActive);
        }

        public Task<bool> IsAllDayBalanceTransfer(int custId)
            => accountInfoDataAccess.IsAllDayBalanceTransfer(new SelectIsAllDayBalTransferCriteria(custId));
    }
}