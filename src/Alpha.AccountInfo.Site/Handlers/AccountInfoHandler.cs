namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.AccountInfo.Constants;
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using Alpha.Products.Core.Models;
    using MediatR;
    using Requests.ProductStatus;
    using Services.AccountInfo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.AccountInfo;

    public class AccountInfoHandler :
        IAsyncRequestHandler<AccountInfoQuery, AccountInfoViewModel>,
        IRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>,
        IRequestHandler<CustomerInfoQuery, CustomerInfoViewModel>,
        IRequestHandler<DownlineListQuery, DownlineListViewModel>,
        IRequestHandler<CustomerAllStatusQuery, CustomererAllStatusViewModel>,
        IRequestHandler<OnlineListQuery, IEnumerable<OnlineUserModel>>,
        IAsyncRequestHandler<GivenCreditQuery, GivenCreditViewModel>,
        IRequestHandler<GivenCreditCommand, Result>,
        IRequestHandler<OutrightCommand, Result>,
        IRequestHandler<ClosedCommand, Result>,
        IRequestHandler<SuspendedCommand, Result>,
        IRequestHandler<AutoMasterPTCommand, Result>,
        IRequestHandler<DoubleCommCommand, Result> 
    {
        private readonly AccountInfoService accountInfoService;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;

        public AccountInfoHandler(AccountInfoService accountInfoService) : this(accountInfoService, null)
        {
        }

        internal AccountInfoHandler(
            AccountInfoService accountInfoService,
             IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel)
        {
            this.accountInfoService = accountInfoService;

            this.getBalanceInfoViewModelByLevel = getBalanceInfoViewModelByLevel ??
                new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>>
                {
                    {UserLevel.Master, BuildMasterBalanceInfoViewModel},
                    {UserLevel.Agent, BuildAgentBalanceInfoViewModel},
                    {UserLevel.Member, BuildMemberBalanceInfoViewModel}
                };
        }

        public async Task<AccountInfoViewModel> Handle(AccountInfoQuery message)
            => new AccountInfoViewModel(message);

        public IEnumerable<UsernameSuggestion> Handle(UserNameSuggestionQuery message)
            => accountInfoService.GetUsernamesSuggestion(message.ActorId, message.SuggestionQuery, message.IsInternal);

        public CustomerInfoViewModel Handle(CustomerInfoQuery message)
        {
            var customerInfo = accountInfoService.GetCustomerInfoModel(message.UserName, message.ActorId);
            var balanceInfo = getBalanceInfoViewModelByLevel[Enumeration.FromValue<UserLevel>(customerInfo.RoleId)](
                message, 
                customerInfo, 
                accountInfoService.GetBalanceInfoModel(customerInfo.CustId, customerInfo.RoleId));
    
            return new CustomerInfoViewModel(message,customerInfo, balanceInfo);
        }

        public DownlineListViewModel Handle(DownlineListQuery message)
            => new DownlineListViewModel(accountInfoService.GetDownlineListModel(message.CustId));

        public CustomererAllStatusViewModel Handle(CustomerAllStatusQuery message)
        {
            var model = accountInfoService.GetCustomerAllStatusModel(message.SearchAccountId);

            var actor = new Customer(
                message.ActorId,
                message.SubAccountId, 
                message.ActorName, 
                message.ActorRoleId, 
                GetActorProductStatus(message, model.DCSProducts.ToList()));

            var upline = new Customer(
                message.UplientId,
                message.SubAccountId,
                message.UplineName,
                message.UplineRoleId,
                GetUplineProductStatus(model.NonDCSProduct, model.DCSProducts.ToList()));

            var searchAccount = new Customer(
                message.SearchAccountId,
                message.SubAccountId,
                message.SearchAccountName,
                message.SearchAccountRoleId,
                GetSearchAccountProductStatus(model.NonDCSProduct, model.DCSProducts.ToList()), 
                upline);

            var setting = new SettingViewModel(message, model.CustomerStatus.TransferDaily, actor, searchAccount);
            var status = new StatusViewModel(message, model.CustomerStatus, actor, searchAccount);

            return new CustomererAllStatusViewModel(status, setting);
        }

        public IEnumerable<OnlineUserModel> Handle(OnlineListQuery message)
            => accountInfoService.GetOnlineList(message.CustId);

        public async Task<GivenCreditViewModel> Handle(GivenCreditQuery message)
           => new GivenCreditViewModel(message);

        public Result Handle(GivenCreditCommand command)
            => accountInfoService.UpdateGivenCredit(
                new UpdateGivenCreditParam(
                    command.SearchAccountId,
                    command.SearchAccountRoleId,
                    command.CreditAmount,
                    command.ActorId,
                    command.ActorRoleId,
                    command.SubAccountId,
                    command.Language,
                    command.IP));

        public Result Handle(OutrightCommand command)
           => accountInfoService.UpdateOutright(
               new UpdateOutrightParam(
                   command.SearchAccountId,
                   command.ActorId,
                   command.DisableOutright,
                   command.SubAccountId,
                   command.Language,
                   command.IP));

        public Result Handle(ClosedCommand command)
           => accountInfoService.UpdateClosed(
               new UpdateClosedParam(
                   command.SearchAccountId,
                   command.ActorName,
                   command.ActorId,
                   command.Closed,
                   command.SyncedCasino,
                   command.SubAccountId,
                   command.Language,
                   command.IP));

        public Result Handle(SuspendedCommand command)
          => accountInfoService.UpdateSuspended(
              new UpdateSuspendedParam(
                  command.SearchAccountId,
                  command.ActorName,
                  command.ActorId,
                  command.Suspended,
                  command.SyncedCasino,
                  command.SubAccountId,
                  command.Language,
                  command.IP));

        public Result Handle(AutoMasterPTCommand command)
           => accountInfoService.UpdateAutoMasterPT(
               new UpdateAutoMasterPTParam(
                   command.SearchAccountId,
                   command.ActorId,
                   command.DisableAutoMasterPT,
                   command.SubAccountId,
                   command.Language,
                   command.IP));

        public Result Handle(DoubleCommCommand command)
          => accountInfoService.UpdateDoubleComm(
              new UpdateDoubleCommParam(
                  command.SearchAccountId,
                  command.ActorId,
                  command.DoubleComm,
                  command.SubAccountId,
                  command.Language,
                  command.IP));

        private static IEnumerable<CustomerProductStatus> GetSearchAccountProductStatus(
            NonDCSProductModel nonDCSProduct,
            List<DCSProductModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductIdConstants.NumberGame, nonDCSProduct.DisableNumber),
                new CustomerProductStatus(ProductIdConstants.RNGCasino, nonDCSProduct.DisableCasino, synced: nonDCSProduct.SyncCasino),
                new CustomerProductStatus(ProductIdConstants.ESport, nonDCSProduct.DisableEsports),
                new CustomerProductStatus(ProductIdConstants.ColossusBets, nonDCSProduct.DisableColossusBet, hidden: nonDCSProduct.HideColossusBet),
                new CustomerProductStatus(ProductIdConstants.Keno, nonDCSProduct.HideKeno)
            };

            dcsProducts.ForEach(product => {
                customerProductStatus.Add(
                    new CustomerProductStatus(product.ProductId, product.IsDisabled, product.IsHidden, product.IsSync));
            });

            return customerProductStatus;
        }

        private static IEnumerable<CustomerProductStatus> GetUplineProductStatus(
            NonDCSProductModel nonDCSProduct,
            List<DCSProductModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductIdConstants.NumberGame, nonDCSProduct.UplineDisableNumber),
                new CustomerProductStatus(ProductIdConstants.RNGCasino, nonDCSProduct.UplineDisableCS),
                new CustomerProductStatus(ProductIdConstants.ESport, nonDCSProduct.UplineDisableEsports),
                new CustomerProductStatus(ProductIdConstants.ColossusBets, nonDCSProduct.UplineDisableColossusBet, hidden: nonDCSProduct.UplineDisableColossusBet),
                new CustomerProductStatus(ProductIdConstants.Keno, nonDCSProduct.HideKeno)
            };

            dcsProducts.ForEach(product => {
                customerProductStatus.Add(
                    new CustomerProductStatus(product.ProductId, product.IsDisabledUpline, product.IsHiddenUpline, product.IsSyncedUpline));
            });

            return customerProductStatus;
        }

        private static IEnumerable<CustomerProductStatus> GetActorProductStatus(
            CustomerAllStatusQuery message,
            List<DCSProductModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductIdConstants.NumberGame, message.IsNumberGameDisabled),
                new CustomerProductStatus(ProductIdConstants.RNGCasino, message.IsCasinoDisabled, synced: message.IsCasinoSync),
                new CustomerProductStatus(ProductIdConstants.ESport, message.IsDisabledESports),
                new CustomerProductStatus(ProductIdConstants.ColossusBets, message.DisableColossusBets, hidden: message.IsHideColossusBets),
                new CustomerProductStatus(ProductIdConstants.Keno, message.IsHideKeno)
            };

            dcsProducts.ForEach(product =>
            {
                var productId = product.ProductId;
                customerProductStatus.Add(
                    new CustomerProductStatus(
                        productId,
                        message.GetDisabledProduct(productId),
                        message.GetHideProduct(productId),
                        message.GetSyncedProduct(productId)));
            });

            return customerProductStatus;
        }

        internal static IBalanceInfoViewModel BuildMasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new MasterBalanceInfoViewModel(message, balanceInfoModel, customerInfo);

        internal static IBalanceInfoViewModel BuildAgentBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new AgentBalanceInfoViewModel(message, balanceInfoModel, customerInfo);

        internal static IBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfo,
            IBalanceInfoModel balanceInfoModel)
            => new MemberBalanceInfoViewModel(message, balanceInfoModel, customerInfo);
    }
}