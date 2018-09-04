namespace Alpha.AccountInfo.Services.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using Alpha.AccountInfo.Services.ProductStatus;
    using Alpha.Products.Core.Shared;
    using Fanex.Data.Repository;
    using System;
    using System.Collections;
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
            => repository.Fetch<UsernameSuggestion>(new SelectAutoSuggestionCriteria(actorId, suggestionQuery, isInternal));

        public CustomerInfoModel GetCustomerInfoModel(string username, int uplineId)
            =>  repository.Get<CustomerInfoModel>(new SelectCustomerInfoCriteria(username, uplineId));

        public IBalanceInfoModel GetBalanceInfoModel(int custId, byte roleId)
           => getBalanceInfoModelByLevel[Enumeration.FromValue<UserLevel>(roleId)](new SelectBalanceInfoCriteria(custId, roleId));

        public CustomerAllStatusModel GetCustomerAllStatusModel(int custId)
          => accountInfoDataAccess.GetCustomerAllStatus(new SelectCustomerAllStatusModelCriteria(custId));

        public DownlineListModel GetDownlineListModel(int custId)
            =>  accountInfoDataAccess.GetDownlineList(new SelectDownlineListCriteria(custId));

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

        public Result UpdateCustomerStatus(UpdateCustomerStatusParam param)
        {
            var criteria = new UpdateCustomerStatusCriteria(
                   param.SearchAccountId,
                   param.ActorId,
                   param.SubAccountId,
                   param.Language,
                   param.IP,
                   param.StatusName);

            var result = accountInfoDataAccess.ExecuteNonQuery(
                criteria.GetSettingKey(),
                criteria.AddSpParams(new Hashtable { { param.StatusName, param.StatusValue } }));

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
          => repository.Get<MasterBalanceInfoModel>(criteria);

        internal IBalanceInfoModel GetAgentBalanceInfoModel(ICriteria criteria)
          => repository.Get<AgentBalanceInfoModel>(criteria);

        internal IBalanceInfoModel GetMemberBalanceInfoModel(ICriteria criteria)
         => repository.Get<MemberBalanceInfoModel>(criteria);
    }
}
