namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using System.Collections.Generic;
    using AccountInfo.Models;
    using AccountInfo.Models.ProductStatus;
    using Enumerations;
    using Params.ProductStatus;
    using ViewModels.ProductStatus;

    public static class ProductStatusQueryExtensions
    {
        public static ProductStatusAuthorizationParam ToProductStatusAuthorizationParam(
            this ProductStatusQuery query)
        {
            return new ProductStatusAuthorizationParam(
                query.ProductId,
                query.CustId,
                query.CustRole,
                query.ViewerId,
                query.ViewerLevel);
        }

        public static ProductStatusViewModel ToProductStatusViewModel(
            this ProductStatusQuery query, IEnumerable<GroupProductStatus> groupProductStatus)
        {
            return new ProductStatusViewModel(
                query.ProductId,
                query.CustId,
                query.CustName,
                Enumeration.FromValue<UserLevel>(query.CustRoleId),
                groupProductStatus,
                query.ViewerLevel,
                query.IsViewOnly);
        }
    }
}