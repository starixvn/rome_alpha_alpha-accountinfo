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
            var canView = IsMasterAgentMemberOfViewerUser(customerInformation, param.ViewerId, param.ViewerLevel);

            if (canView && param.ProductId != ProductIdConstants.Keno)
            {
                canView = IsDirectDownlineOfViewerUser(customerInformation, param.ViewerLevel);
            }

            return canView && CanViewProduct(param.ProductId, customerInformation.SyncCasino);
        }

        public bool CanUpdate(ProductStatusAuthorizationParam param)
        {
            var customerInformation = GetCustomerInformation(param.CustId);

            return IsMasterAgentMemberOfViewerUser(customerInformation, param.ViewerId, param.ViewerLevel) &&
                IsDirectDownlineOfViewerUser(customerInformation, param.ViewerLevel) &&
                CanViewProduct(param.ProductId, customerInformation.SyncCasino);
        }

        private static bool IsMasterAgentMemberOfViewerUser(
            CustomerInformation customerInformation,
            int viewerId,
            UserLevel viewerRole)
        {
            bool isValidCustomer = customerInformation != null;

            if (isValidCustomer)
            {
                if (viewerRole.IsSuperLevel())
                {
                    isValidCustomer = viewerId == customerInformation.SuperId;
                }
                else if (viewerRole.IsMasterLevel())
                {
                    isValidCustomer = viewerId == customerInformation.MasterId;
                }
                else if (viewerRole.IsAgentLevel())
                {
                    isValidCustomer = viewerId == customerInformation.AgentId;
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
            UserLevel viewerRole)
        {
            bool isValidRole = customerInformation != null;

            if (isValidRole)
            {
                var custRole = Enumeration.FromValue<UserLevel>(customerInformation.RoleId);

                isValidRole = custRole.IsDirectDownlineOf(viewerRole);
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
