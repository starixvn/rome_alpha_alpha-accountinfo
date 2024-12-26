namespace Alpha.AccountInfo.Services.ProductStatus
{
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.Products.Core.Shared;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Params.ProductStatus;
    using System;
    using System.Linq;

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

        public bool CanView(ProductStatusAuthorizationParam param) => CanUpdate(param);

        public bool CanUpdate(ProductStatusAuthorizationParam param)
        {
            var customerInformation = GetCustomerInformation(param.CustId);

            return IsMasterAgentMemberOfViewerUser(customerInformation, param.UplineId, param.UplineLevel) &&
                IsDirectDownlineOfViewerUser(customerInformation, param.UplineLevel) &&
                (param.IsGroup ? param.OfferedProductGroupIds.Contains(param.ProductId) : canViewProduct(param.ProductId));
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

                isValidRole = custRole.IsDirectDownlineOf(uplineLevel)
                    || (uplineLevel.IsSuperLevel() && customerInformation.AgentId == DirectMemberAgentIds.Super)
                    || (uplineLevel.IsMasterLevel() && customerInformation.AgentId == DirectMemberAgentIds.Master);
            }

            return isValidRole;
        }

        private CustomerInformation GetCustomerInformation(int custId)
            => repository.Get<CustomerInformation>(new SelectCustomerInformationCriteria(custId));
    }
}