namespace Alpha.AccountInfo.Site.Handlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AccountInfo.Models;
    using AccountInfo.Models.ProductStatus;
    using Enumerations;
    using MediatR;
    using Newtonsoft.Json;
    using Params.ProductStatus;
    using Requests.ProductStatus;
    using Services.ProductStatus;
    using ViewModels.ProductStatus;

    public class ProductStatusHandler :
        IAsyncRequestHandler<ProductStatusQuery, ProductStatusViewModel>,
        IAsyncRequestHandler<ProductStatusCommand, Result>
    {
        private readonly ProductStatusService statusService;

        public ProductStatusHandler(ProductStatusService statusService)
        {
            this.statusService = statusService;
        }

        public async Task<ProductStatusViewModel> Handle(ProductStatusQuery message)
        {
            var authorizationParam = ToProductStatusAuthorizationParam(message);
            var queryParam = ToProductStatusQueryParam(message);

            var model = await statusService.GetProductStatusAsync(authorizationParam, queryParam);

            return ToProductStatusViewModel(message, model);
        }

        public async Task<Result> Handle(ProductStatusCommand message)
        {
            var authorizationParam = ToProductStatusAuthorizationParam(message);
            var commandParam = ToUpdateProductStatusParam(message);

            return await statusService.UpdateProductStatusAsync(authorizationParam, commandParam);
        }

        private static ProductStatusAuthorizationParam ToProductStatusAuthorizationParam(ProductStatusQuery message)
            => new ProductStatusAuthorizationParam(
                message.ProductId,
                message.CustId,
                message.CustRole,
                message.ViewerId,
                message.ViewerLevel);

        private static ProductStatusQueryParam ToProductStatusQueryParam(ProductStatusQuery message)
            => new ProductStatusQueryParam(message.ProductId, message.CustId, message.CustRoleId);

        private static ProductStatusViewModel ToProductStatusViewModel(
            ProductStatusQuery message,
            IEnumerable<ProductStatusModel> productStatuses)
            => new ProductStatusViewModel(
                message.ProductId,
                message.CustId,
                message.CustName,
                Enumeration.FromValue<UserLevel>(message.CustRoleId),
                productStatuses,
                message.ViewerLevel,
                message.IsViewOnly);

        private static ProductStatusCommandParam ToUpdateProductStatusParam(
            ProductStatusCommand message)
            => new ProductStatusCommandParam(
                message.ProductId,
                message.CustId,
                message.ViewerId,
                message.ViewerUserId,
                message.ViewerSubAccId,
                message.LangKey,
                message.IP,
                JsonConvert.DeserializeObject<ProductStatusParam[]>(message.ProductStatus));
    }
}