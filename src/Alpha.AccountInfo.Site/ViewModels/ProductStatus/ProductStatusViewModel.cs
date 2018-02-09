namespace Alpha.AccountInfo.Site.ViewModels.ProductStatus
{
    using System.Collections.Generic;
    using AccountInfo.Models;
    using AccountInfo.Models.ProductStatus;

    public class ProductStatusViewModel
    {
        public ProductStatusViewModel(
            int productId,
            int custId,
            string custName,
            UserLevel custRole,
            IEnumerable<ProductStatusModel> productStatus,
            UserLevel loggedInUserRole,
            bool isViewOnly)
        {
            ProductId = productId;
            CustId = custId;
            CustName = custName;
            CustRole = custRole;
            ProductStatus = productStatus;
            IsAgentUpdateMember = custRole.IsMemberLevel() && loggedInUserRole.IsAgentLevel();
            IsDirectDownline = custRole.IsDirectDownlineOf(loggedInUserRole);
            IsViewOnly = isViewOnly;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public string CustName { get; }

        public UserLevel CustRole { get; }

        public IEnumerable<ProductStatusModel> ProductStatus { get; }

        public bool IsAgentUpdateMember { get; }

        public bool IsDirectDownline { get; }

        public bool IsViewOnly { get; }

        public bool IsDisabledEditCustStatus(ProductStatusModel productStatus)
            => !IsDirectDownline
            || productStatus.IsDisabledUpline
            || (!IsAgentUpdateMember && !productStatus.IsDisabled);

        public bool IsEnabledSubmitButton(ProductStatusModel productStatus)
            => IsAgentUpdateMember
            ? !productStatus.IsDisabledUpline
            : !productStatus.IsDisabledUpline && productStatus.IsDisabled;
    }
}