namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System;
    using Constants;
    using Criteria;
    using Enumerations;
    using Fanex.Data.Repository;
    using Models;
    using Params.ProductStatus;

    public interface ProductStatusAuthorizationService
    {
        bool CanView(ProductStatusAuthorizationParam param);

        bool CanUpdate(ProductStatusAuthorizationParam param);
    }

    public class ProductStatusAuthorizationServiceImpl : ProductStatusAuthorizationService
    {
        private readonly IDynamicRepository repository;
        private readonly Func<int, bool> canViewProduct;

        public ProductStatusAuthorizationServiceImpl(
            IDynamicRepository repository,
            Func<int, bool> canViewProduct)
        {
            this.repository = repository;
            this.canViewProduct = canViewProduct;
        }

        public bool CanView(ProductStatusAuthorizationParam param)
        {
            var customerInformation = GetCustomerInformation(param.CustId);
            var canView = IsMasterAgentMemberOfViewerUser(customerInformation, param.UplineId, param.UplineLevel);

            if (canView && param.ProductId != ProductIdConstants.Keno)
            {
                canView = IsDirectDownlineOfViewerUser(customerInformation, param.UplineLevel);
            }

            return canView && CanViewProduct(param.ProductId, customerInformation.SyncCasino);
        }

        public bool CanUpdate(ProductStatusAuthorizationParam param)
        {
            var customerInformation = GetCustomerInformation(param.CustId);

            return IsMasterAgentMemberOfViewerUser(customerInformation, param.UplineId, param.UplineLevel) &&
                IsDirectDownlineOfViewerUser(customerInformation, param.UplineLevel) &&
                CanViewProduct(param.ProductId, customerInformation.SyncCasino);
        }

        private static bool IsMasterAgentMemberOfViewerUser(
            CustomerInformation customerInformation,
            int uplineId,
            UserLevel uplineLevel)
        {
            bool isValidCustomer = customerInformation != null;

            if (isValidCustomer)
            {
                if (uplineLevel.IsSuperLevel())
                {
                    isValidCustomer = uplineId == customerInformation.SuperId;
                }
                else if (uplineLevel.IsMasterLevel())
                {
                    isValidCustomer = uplineId == customerInformation.MasterId;
                }
                else if (uplineLevel.IsAgentLevel())
                {
                    isValidCustomer = uplineId == customerInformation.AgentId;
                }
                else
                {
                    isValidCustomer = false;
                }
            }

            return isValidCustomer;
        }

        private static bool IsDirectDownlineOfViewerUser(
            CustomerInformation customerInformation,
            UserLevel uplineLevel)
        {
            bool isValidRole = customerInformation != null;

            if (isValidRole)
            {
                var custRole = Enumeration.FromValue<UserLevel>(customerInformation.RoleId);

                isValidRole = custRole.IsDirectDownlineOf(uplineLevel);
            }

            return isValidRole;
        }

        private bool CanViewProduct(int productId, bool syncCasino)
        {
            var canView = canViewProduct(productId);

            return canView && productId == ProductIdConstants.RNGCasino ? syncCasino : canView;
        }

        private CustomerInformation GetCustomerInformation(int custId)
            => repository.Get<CustomerInformation>(new SelectCustomerInformationCriteria(custId));
    }
}
