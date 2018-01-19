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
            var customerStatus = GetCustomerStatus(param.CustId);
            var canView = IsValidCustomer(customerStatus, param.ViewerId, param.ViewerLevel);

            if (canView && param.ProductId != ProductIdConstants.Keno)
            {
                canView = IsValidRole(customerStatus, param.ViewerLevel);
            }

            return canView && CanViewProduct(param.ProductId, customerStatus.SyncCasino);
        }

        public bool CanUpdate(ProductStatusAuthorizationParam param)
        {
            var customerStatus = GetCustomerStatus(param.CustId);

            return IsValidCustomer(customerStatus, param.ViewerId, param.ViewerLevel) &&
                IsValidRole(customerStatus, param.ViewerLevel) &&
                CanViewProduct(param.ProductId, customerStatus.SyncCasino);
        }

        private static bool IsValidCustomer(
            CustomerStatus customerStatus,
            int viewerId,
            UserLevel viewerRole)
        {
            bool isValidCustomer = customerStatus != null;

            if (isValidCustomer)
            {
                if (viewerRole.IsSuperLevel())
                {
                    isValidCustomer = viewerId == customerStatus.SuperId;
                }
                else if (viewerRole.IsMasterLevel())
                {
                    isValidCustomer = viewerId == customerStatus.MasterId;
                }
                else if (viewerRole.IsAgentLevel())
                {
                    isValidCustomer = viewerId == customerStatus.AgentId;
                }
                else
                {
                    isValidCustomer = false;
                }
            }

            return isValidCustomer;
        }

        private static bool IsValidRole(
            CustomerStatus customerStatus,
            UserLevel loggedInUserRole)
        {
            bool isValidCustomer = customerStatus != null;

            if (isValidCustomer)
            {
                var custRole = Enumeration.FromValue<UserLevel>(customerStatus.RoleId);

                isValidCustomer = custRole.IsDirectDownlineOf(loggedInUserRole);
            }

            return isValidCustomer;
        }

        private bool CanViewProduct(int productId, bool syncCasino)
        {
            var canView = canViewProduct(productId);

            return canView && productId == ProductIdConstants.RNGCasino ? syncCasino : canView;
        }

        private CustomerStatus GetCustomerStatus(int custId)
        {
            return repository.Get<CustomerStatus>(new SelectCustomerStatusCriteria(custId));
        }
    }
}
