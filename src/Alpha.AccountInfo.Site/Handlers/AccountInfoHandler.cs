namespace Alpha.AccountInfo.Site.Handlers
{
    using Alpha.AccountInfo.Constants;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using Alpha.Products.Core.Models;
    using MediatR;
    using Requests.ProductStatus;
    using Services.AccountInfo;
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
        IRequestHandler<OnlineListQuery, IEnumerable<OnlineListModel>>,
        IRequestHandler<GivenCreditCommand, Result>
    {
        private readonly AccountInfoService accountInfoService;

        public AccountInfoHandler(AccountInfoService accountInfoService)
        {
            this.accountInfoService = accountInfoService;
        }

        public async Task<AccountInfoViewModel> Handle(AccountInfoQuery message)
            => new AccountInfoViewModel(message);

        public IEnumerable<UsernameSuggestion> Handle(UserNameSuggestionQuery message)
            => accountInfoService.GetUsernamesSuggestion(message.ActorId, message.SuggestionQuery, message.IsInternal);

        public CustomerInfoViewModel Handle(CustomerInfoQuery message)
        {
            var custormerinfo = accountInfoService.GetCustomerInfoModelAsync(message.UserName, message.ActorId);
            if (custormerinfo != null)
            {
                var balanceinfo = accountInfoService.GetBalanceInfoModelAsync(custormerinfo.CustId, custormerinfo.RoleId);

                return new CustomerInfoViewModel(message, custormerinfo, balanceinfo);
            }

            return new CustomerInfoViewModel();
        }

        public DownlineListViewModel Handle(DownlineListQuery message)
            => new DownlineListViewModel(accountInfoService.GetDownlineListModelAsync(message.CustId));

        public CustomererAllStatusViewModel Handle(CustomerAllStatusQuery message)
        {
            var model = accountInfoService.GetCustomerAllStatusModelAsync(message.SearchAccountId);

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

            var setting = new SettingViewModel(message, model, actor, searchAccount);
            var status = new StatusViewModel(message, model.CustomerStatus, actor, searchAccount);

            return new CustomererAllStatusViewModel(status, setting);
        }

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

        public IEnumerable<OnlineListModel> Handle(OnlineListQuery message)
            => accountInfoService.GetOnlineList(message.CustId);

        public Result Handle(GivenCreditCommand message)
            => accountInfoService.UpdateGivenCredit(
                new GivenCreditParam(
                    message.SearchAccountId,
                    message.SearchAccountRoleId,
                    message.CreditAmount,
                    message.ActorId,
                    message.ActorRoleId,
                    message.SubAccountId,
                    message.Language,
                    message.IP));
    }
}