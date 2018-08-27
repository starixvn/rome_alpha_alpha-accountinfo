namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using Alpha.AccountInfo.Services.ProductStatus;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;

    public class AccountInfoServiceImpl : AccountInfoService
    {
        private readonly IDynamicRepository repository;
        private readonly AccountInfoDataAccess accountInfoDataAccess;
        private readonly RnGCasinoService rnGCasinoService;
        private readonly IDictionary<UserLevel, Func<ICriteria, IBalanceInfoModel>> getBalanceInfoModelByLevel;

        public AccountInfoServiceImpl(
        IDynamicRepository dynamicRepository, 
        AccountInfoDataAccess accountInfoDataAccess,
        RnGCasinoService rnGCasinoService) : this(dynamicRepository, accountInfoDataAccess, rnGCasinoService, null)
        {
        }

        internal AccountInfoServiceImpl(
           IDynamicRepository dynamicRepository,
           AccountInfoDataAccess accountInfoDataAccess,
           RnGCasinoService rnGCasinoService,
           IDictionary<UserLevel, Func<ICriteria, IBalanceInfoModel>> getBalanceInfoModelByLevel)
        {
            repository = dynamicRepository;
            this.accountInfoDataAccess = accountInfoDataAccess;
            this.rnGCasinoService = rnGCasinoService;

            this.getBalanceInfoModelByLevel = getBalanceInfoModelByLevel ??
                new Dictionary<UserLevel, Func<ICriteria, IBalanceInfoModel>>
                {
                    {UserLevel.Master,  GetMasterBalanceInfoModel},
                    {UserLevel.Agent,  GetAgentBalanceInfoModel},
                    {UserLevel.Member,  GetMemberBalanceInfoModel}
                };
        }

        public IEnumerable<UsernameSuggestion> GetUsernamesSuggestion(int actorId, string suggestionQuery, bool isInternal)
            => repository.Fetch<UsernameSuggestion>(new SelectAutoSuggestion(actorId, suggestionQuery, isInternal));

        public IBalanceInfoModel GetBalanceInfoModel(int custId, byte roleId)
            => getBalanceInfoModelByLevel[Enumeration.FromValue<UserLevel>(roleId)](new SelectBalanceInfoCriteria(custId, roleId));

        public CustomerAllStatusModel GetCustomerAllStatusModel(int custId)
            => accountInfoDataAccess.GetCustomerAllStatusModel(
                new SelectCustomerAllStatusModelCriteria(custId));

        public CustomerInfoModel GetCustomerInfoModel(string username, int uplineId)
            =>  repository.Get<CustomerInfoModel>(new SelectCustomerInfoCriteria(username, uplineId));

        public DownlineListModel GetDownlineListModel(int custId)
            =>  accountInfoDataAccess.GetDownlineListModel(new SelectDownlineListCriteria(custId));

        public IEnumerable<OnlineUserModel> GetOnlineList(int custid)
            => accountInfoDataAccess.GetOnlineList(new SelectOnlineListCriteria(custid));

        public Result UpdateGivenCredit(UpdateGivenCreditParam param)
            => accountInfoDataAccess.UpdateGivenCredit(
                new UpdateGivenCreditCriteria(
                    param.SearchAccountId,
                    param.RoleId,
                    param.CreditAmount,
                    param.ActorId,
                    param.ActorRoleId,
                    param.SubAccountId,
                    param.Language,
                    param.IP));

        public Result UpdateOutright(UpdateOutrightParam param)
            => accountInfoDataAccess.ExecuteNonQuery(
               new UpdateOutrightCriteria(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.DisableOutright,
                   param.Language,
                   param.IP));

        public Result UpdateClosed(UpdateClosedParam param)
        {
            var result = accountInfoDataAccess.ExecuteNonQuery(
                new UpdateClosedCriteria(
                    param.SearchAccountId,
                    param.ActorId,
                    param.SubAccountId,
                    param.Closed,
                    param.Language,
                    param.IP));

            if(result.IsSuccess && param.SyncedCasino)
            {
                rnGCasinoService.Update3rdRnGCasinoClosed(
                    param.SearchAccountId,
                    param.Closed,
                    param.IP,
                    param.ActorId,
                    param.ActorName);
            }

            return result;
        }

        public Result UpdateSuspended(UpdateSuspendedParam param)
        {
            var result = accountInfoDataAccess.ExecuteNonQuery(
                new UpdateSuspendedCriteria(
                    param.SearchAccountId,
                    param.ActorId,
                    param.SubAccountId,
                    param.Suspended,
                    param.Language,
                    param.IP));

            if (result.IsSuccess && param.SyncedCasino)
            {
                rnGCasinoService.Update3rdRnGCasinoSuspended(
                    param.SearchAccountId,
                    param.Suspended,
                    param.IP,
                    param.ActorId,
                    param.ActorName);
            }

            return result;
        }

        public Result UpdateAutoMasterPT(UpdateAutoMasterPTParam param)
            => accountInfoDataAccess.ExecuteNonQuery(
               new UpdateAutoMasterPTCriteria(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.DisableAutoMasterPT,
                   param.Language,
                   param.IP));

        public Result UpdateDoubleComm(UpdateDoubleCommParam param)
            => accountInfoDataAccess.ExecuteNonQuery(
                new UpdateDoubleCommCriteria(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.DoubleComm,
                   param.Language,
                   param.IP));

        internal IBalanceInfoModel GetMasterBalanceInfoModel(ICriteria criteria)
          => repository.Get<MasterBalanceInfoModel>(criteria);

        internal IBalanceInfoModel GetAgentBalanceInfoModel(ICriteria criteria)
          => repository.Get<AgentBalanceInfoModel>(criteria);

        internal IBalanceInfoModel GetMemberBalanceInfoModel(ICriteria criteria)
         => repository.Get<MemberBalanceInfoModel>(criteria);
    }
}
