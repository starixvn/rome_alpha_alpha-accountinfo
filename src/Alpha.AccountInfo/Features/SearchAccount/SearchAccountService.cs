namespace Alpha.AccountInfo.Services.AccountInfo
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Configurations;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.Products.Core.Shared;
    using AlphaPlus.Utilities.IP;
    using Fanex.Data.Repository;
    using Newtonsoft.Json;

    public interface SearchAccountService
    {
        Task<CustomerInfoModel> GetCustomerInfo(string username, int uplineId, int roleId);

        Task<bool> UpdateUserSearch(int userId, string search, string firstName, string lastName, string nickname);

        Task<IBalanceInfoModel> GetBalanceInfo(int custId, byte roleId, string agIP);

        Task<DownlineListModel> GetDownlineList(int custId, int pageIndex, byte roleId, byte statusId, int totalPages, bool isDirectMember = false);

        Task<IEnumerable<TotalDownlinesSummary>> GetTotalDownlinesSummary(int custId, byte roleId);

        Task<IEnumerable<OnlineUserModel>> GetOnlineList(int custid, bool adminCanViewVNIP);

        Task<Result> UpdateGivenCredit(UpdateGivenCreditParam param);

        Task<Result> UpdateCustomerStatus(UpdateCustomerStatusParam param);

        Task<CreditInfo> GetCreditInfo(int custId);

        Task<Result> UpdateAllDayBalanceTransfer(UpdateAllDayBalanceTransferParam param);

        Task<bool> CheckFirstLoginByQRCode(int custId);

        Task<bool> CheckMultipleCustomesBelonging(int actorId, int subAccountId, string custIds, bool isInternal);

        Task<bool> IsAllDayBalanceTransfer(int custId);

        Task<DateTime> GetLastTxn(string username, int uplineId, bool isInternal);
    }

    public class SearchAccountServiceImpl : SearchAccountService
    {
        private const string English = "en-US";
        private const string ChineseTraditional = "zh-TW";
        private const string ChineseSimplified = "zh-CN";
        private const string Thai = "th-TH";
        private const string Vietnamese = "vi-VN";

        private readonly int failErrorCode = 100;

        private readonly IDynamicRepository dynamicRepository;
        private readonly IPService ipService;

        public SearchAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            IPService ipService)
        {
            this.dynamicRepository = dynamicRepository;
            this.ipService = ipService;
        }

        public async Task<CustomerInfoModel> GetCustomerInfo(string username, int uplineId, int roleId)
        {
            var customerInfo = await dynamicRepository.GetAsync<CustomerInfoModel>(new SelectCustomerInfoCriteria(username, uplineId));
            if (customerInfo != null)
            {
                var custClosedReason = await dynamicRepository.GetAsync<CustomerClosedReasonModel>(
                    new SelectClosedReasonCriteria(customerInfo.CustId, customerInfo.RoleId));
                customerInfo.ClosedReason = custClosedReason?.ClosedReason ?? 0;

                var nickname = roleId - customerInfo.RoleId == 1 ? customerInfo.UserName2 : string.Empty;
                await UpdateUserSearch(uplineId, customerInfo.UserName, customerInfo.FirstName, customerInfo.LastName, nickname);
            }

            return customerInfo;
        }

        public async Task<bool> UpdateUserSearch(int userId, string search, string firstName, string lastName, string nickname)
            => await dynamicRepository.GetAsync<bool>(
                new UpdateUserSearchCriteria(userId, search, firstName, lastName, nickname));

        public async Task<IBalanceInfoModel> GetBalanceInfo(int custId, byte roleId, string agIP)
        {
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);
            var criteria = new SelectBalanceInfoCriteria(custId, roleId);

            if (userLevel.IsMasterLevel())
            {
                return await GetMasterBalanceInfoModel(criteria);
            }

            if (userLevel.IsAgentLevel())
            {
                return await GetAgentBalanceInfoModel(criteria);
            }

            if (userLevel.IsMemberLevel())
            {
                return await GetMemberBalanceInfoModel(criteria, agIP);
            }

            return null;
        }

        public async Task<DownlineListModel> GetDownlineList(int custId, int pageIndex, byte roleId, byte statusId, int totalPages, bool isDirectMember = false)
        {
            var (downlineListModel, downlines) = await dynamicRepository.FetchMultipleAsync<DownlineListModel, DownlineDto>(
                new SelectDownlineListByStatusCriteria(custId, pageIndex, roleId, statusId, totalPages, isDirectMember));
            var downlineListFirst = downlineListModel.FirstOrDefault();

            return downlineListFirst == null
                ? downlineListFirst
                : new DownlineListModel(downlines, downlineListFirst);
        }

        public async Task<IEnumerable<TotalDownlinesSummary>> GetTotalDownlinesSummary(int custId, byte roleId)
          => await dynamicRepository.FetchAsync<TotalDownlinesSummary>(
              new SelectTotalDownlineSummaryCriteria(custId, roleId));

        public async Task<IEnumerable<OnlineUserModel>> GetOnlineList(int custid, bool adminCanViewVNIP)
        {
            if (AppSettings.IsDisabledRecheckin)
            {
                return Enumerable.Empty<OnlineUserModel>();
            }

            var onlineUsers = await dynamicRepository.FetchAsync<OnlineUserModel>(
                new SelectOnlineListCriteria(custid));

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
                await dynamicRepository.ExecuteAsync(totalAmtCommand, param.AGIP);
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
                    await dynamicRepository.ExecuteAsync(replicateServerCommand);
                }

                return Result.CreateSuccessResult();
            }
        }

        public async Task<Result> UpdateCustomerStatus(UpdateCustomerStatusParam param)
        {
            UpdateCustomerStatusCommand command = null;
            switch (param.StatusName)
            {
                case StatusNames.Closed:
                    command = new UpdateClosedStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.DisableAutoMasterPT:
                    command = new UpdateDisableAutoMPTStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.DoubleComm:
                    command = new UpdateDoubleCommLosingStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.DisableOutright:
                    command = new UpdateDisableOutrightStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.Suspended:
                    command = new UpdateSuspendedStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;

                case StatusNames.Unlock:
                    command = new UpdateUnlockedCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName);
                    break;

                default:
                    command = new UpdateDisableProtectedStatusCommand(
                        param.SearchAccountId,
                        param.ActorId,
                        param.SubAccountId,
                        param.Language,
                        param.IP,
                        param.StatusName,
                        param.StatusValue);
                    break;
            }

            await dynamicRepository.ExecuteAsync(command);
            return new Result(command.ErrCode, command.ErrMsg);
        }

        internal async Task<IBalanceInfoModel> GetMasterBalanceInfoModel(ICriteria criteria)
          => await dynamicRepository.GetAsync<MasterBalanceInfoModel>(criteria);

        internal async Task<IBalanceInfoModel> GetAgentBalanceInfoModel(ICriteria criteria)
          => await dynamicRepository.GetAsync<AgentBalanceInfoModel>(criteria);

        internal async Task<IBalanceInfoModel> GetMemberBalanceInfoModel(ICriteria criteria, string agIP)
        {
            var tasks = new List<Task>();

            var memberOutstandingCriteria = new SelectMemberOutstandingCriteria(((AccountInfoCriteriaBase)criteria).SearchAccountId);
            var memberBalanceCriteria = new SelectMemberBalanceCriteria(((AccountInfoCriteriaBase)criteria).SearchAccountId);
            var memberBalanceInfoDTOTask = dynamicRepository.GetAsync<MemberBalanceInfoDTO>(criteria);

            tasks.Add(dynamicRepository.ExecuteAsync(memberOutstandingCriteria));
            tasks.Add(dynamicRepository.ExecuteAsync(memberBalanceCriteria, agIP));
            tasks.Add(memberBalanceInfoDTOTask);

            await Task.WhenAll(tasks);

            return new MemberBalanceInfoModel(
                await memberBalanceInfoDTOTask,
                memberBalanceCriteria.Balance,
                memberBalanceCriteria.PendingTransfer3rd,
                memberOutstandingCriteria.Outstanding,
                memberBalanceCriteria.NetPosition);
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

            if (IsAutoTransferAffect(param.Currency))
            {
                var autoTransferStatus = await GetAutoTransferJobStatus(param.AGIP);
                if (autoTransferStatus)
                {
                    return Result.CreateErrorResult(1, Resources.ADBTChangeFailed_AutoJobRunning);
                }
            }

            var command = new UpdateAllDayBalanceTransferCommand(
                param.SearchAccountId,
                param.ActorId,
                param.Language,
                param.IP,
                param.StatusValue);
            await dynamicRepository.ExecuteAsync(command);
            var result = new Result(int.Parse(command.ErrCode), command.ErrMsg);

            if (result.IsSuccess)
            {
                TurnOffAutoTransfer(param);
            }

            return result;
        }

        public async Task<bool> CheckFirstLoginByQRCode(int custId)
            => await dynamicRepository.GetAsync<bool>(
                new SelectFirstLoginByQRCodeCriteria(custId));

        public async Task<bool> CheckMultipleCustomesBelonging(int actorId, int subAccountId, string custIds, bool isInternal)
        {
            var criteria = new CheckMultipleCustomesBelongingCriteria(actorId, subAccountId, custIds);

            await dynamicRepository.ExecuteAsync(criteria);

            return criteria.RoleId > 0 && (isInternal || criteria.IsActive);
        }

        public async Task<bool> IsAllDayBalanceTransfer(int custId)
        {
            var transferModel = await dynamicRepository.GetAsync<TransferModel>(new SelectIsAllDayBalTransferCriteria(custId));

            return transferModel != null && transferModel.IsAllDayBalTransfer;
        }

        public async Task<DateTime> GetLastTxn(string username, int uplineId, bool isInternal)
        {
            var lastTxn = await dynamicRepository.GetAsync<DateTime?>(
                new SelectLastTxnCriteria(username, uplineId, isInternal));

            return lastTxn ?? DateTime.MinValue;
        }

        private void TurnOffAutoTransfer(UpdateAllDayBalanceTransferParam param)
        {
            if (!param.StatusValue)
            {
                return;
            }

            var turnOffCriteria = new TurnOffAutoTransferCriteria
            {
                UplineId = param.SearchAccountId,
                Creator = param.ActorId,
                IsAllDownline = true,
                IP = param.IP
            };

            var affects = dynamicRepository.Fetch<AffectedDownlinesInfoModel>(turnOffCriteria);

            if (!affects.Any())
            {
                return;
            }

            var rm = Resources.ResourceManager;
            foreach (var row in affects)
            {
                var uplineId = row.CustId;

                var toastMessageCriteria = new InsertToastMessageCriteria(
                    uplineId,
                    param.ActorId,
                    GetToastMessageContent(rm, English),
                    GetToastMessageContent(rm, ChineseSimplified),
                    GetToastMessageContent(rm, ChineseTraditional),
                    GetToastMessageContent(rm, Thai),
                    GetToastMessageContent(rm, Vietnamese));
                dynamicRepository.Execute(toastMessageCriteria);

                var uplineName = row.Username;
                var downlines = row.Names;
                var personalMessageCriteria = new InsertPersonalMessageCriteria(
                    GetPersonalMessage(rm, English, downlines),
                    GetPersonalMessage(rm, ChineseTraditional, downlines),
                    GetPersonalMessage(rm, ChineseSimplified, downlines),
                    GetPersonalMessage(rm, Thai, downlines),
                    GetPersonalMessage(rm, Vietnamese, downlines));

                dynamicRepository.Execute(personalMessageCriteria);

                if (personalMessageCriteria.MessageIDOutput != 0)
                {
                    dynamicRepository.Execute(new AssignPersonalMessageCriteria(personalMessageCriteria.MessageIDOutput, uplineId, uplineName));
                }
            }
        }

        private static string GetToastMessageContent(ResourceManager rm, string languae)
        {
            var culture = CultureInfo.GetCultureInfo(languae);

            var content = new ToastContentModel
            {
                Title = rm.GetString("ADBTChangeToastTitle", culture),
                Body = rm.GetString("ADBTChangeToastMessage", culture)
            };

            return JsonConvert.SerializeObject(content, Formatting.None);
        }

        private static string GetPersonalMessage(ResourceManager rm, string languae, string downlines)
        {
            var culture = CultureInfo.GetCultureInfo(languae);

            return string.Format(
                rm.GetString("ADBTConditionChange_PersonalMessage", culture),
                downlines);
        }

        private async Task<bool> GetAutoTransferJobStatus(string agIP)
        {
            var criteria = new GetAutoTransferJobStatusCriteria();
            await dynamicRepository.ExecuteAsync(criteria, agIP);

            return criteria.IsAutoJobRunning;
        }

        private bool IsAutoTransferAffect(string currency)
        {
            var s = AppSettings.GetConfigValue("OfferAutoTransfer.Currencies");
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            if (s == "*")
            {
                return true;
            }

            return s.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Any(c => string.Equals(currency, c.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}