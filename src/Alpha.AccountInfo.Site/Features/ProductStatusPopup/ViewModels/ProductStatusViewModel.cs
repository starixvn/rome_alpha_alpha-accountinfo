namespace Alpha.AccountInfo.Site.Features.ProductStatusPopup.ViewModels
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.ProductStatusPopup;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Models.ProductStatus;

    public class ProductStatusViewModel
    {
        public ProductStatusViewModel(
            int productId,
            int custId,
            string custName,
            UserLevel custRole,
            IEnumerable<ProductStatusModel> productStatus,
            UserLevel loggedInUserRole,
            bool isViewOnly,
            bool isGroup,
            int agentId)
        {
            ProductId = productId;
            CustId = custId;
            CustName = custName;
            CustRole = custRole;
            ProductStatus = productStatus;
            var isDirectMember = (loggedInUserRole.IsSuperLevel() && agentId == Shared.DirectMemberAgentIds.Super)
                    || (loggedInUserRole.IsMasterLevel() && agentId == Shared.DirectMemberAgentIds.Master);
            IsDirectUplineUpdateMember = custRole.IsMemberLevel() && (loggedInUserRole.IsAgentLevel() || isDirectMember);
            IsDirectDownline = custRole.IsDirectDownlineOf(loggedInUserRole) || isDirectMember;
            IsViewOnly = isViewOnly;
            IsGroup = isGroup;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public string CustName { get; }

        public UserLevel CustRole { get; }

        public IEnumerable<ProductStatusModel> ProductStatus { get; }

        public bool IsDirectUplineUpdateMember { get; }

        public bool IsDirectDownline { get; }

        public bool IsViewOnly { get; }

        public bool IsGroup { get; }

        public bool IsDisabledEditCustStatus(ProductStatusModel productStatus)
        {
            if (ProductId == ProductIdConstants.SabaVirtualSports
                || ProductId == ProductIdConstants.Cashout)
            {
                return !IsDirectDownline;
            }

            return !IsDirectDownline
                || productStatus.IsDisabledUpline
                || (!IsDirectUplineUpdateMember && !productStatus.IsDisabled);
        }

        public bool IsEnabledSubmitButton(ProductStatusModel productStatus)
        {
            if (ProductId == ProductIdConstants.SabaVirtualSports
                || ProductId == ProductIdConstants.Cashout)
            {
                return IsDirectDownline;
            }

            return IsDirectUplineUpdateMember
            ? !productStatus.IsDisabledUpline
            : !productStatus.IsDisabledUpline && productStatus.IsDisabled;
        }

        public bool IsShowMessageWarnning
            => !IsDirectUplineUpdateMember
            && ProductId != ProductIdConstants.SabaVirtualSports
            && ProductId != ProductIdConstants.Cashout;
    }
}