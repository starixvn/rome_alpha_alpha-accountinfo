namespace Alpha.AccountInfo.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
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
            var model = await statusService.GetProductStatusAsync(
                message.ProductId,
                message.CustId,
                message.CustRole,
                message.UplineId,
                message.UplineLevel);

            return ToProductStatusViewModel(message, model);
        }

        public async Task<Result> Handle(ProductStatusCommand message)
        {
            var commandParam = ToUpdateProductStatusParam(message);

            return await statusService.UpdateProductStatusAsync(commandParam);
        }

        private static ProductStatusViewModel ToProductStatusViewModel(
            ProductStatusQuery message,
            IEnumerable<ProductStatusModel> productStatus)
            => new ProductStatusViewModel(
                message.ProductId,
                message.CustId,
                message.CustName,
                Enumeration.FromValue<UserLevel>(message.CustRoleId),
                productStatus,
                message.UplineLevel,
                message.IsViewOnly);

        private static ProductStatusCommandParam ToUpdateProductStatusParam(
            ProductStatusCommand message)
            => new ProductStatusCommandParam(
                message.ProductId,
                message.CustId,
                message.CustRole,
                message.UplineId,
                message.UplineLevel,
                message.ActorId,
                message.ActorName,
                message.SubAccId,
                message.LangKey,
                message.IP,
                message.BetTypeStatus.Select(status => new BetTypeStatusParam(status.SportType, status.BetType, status.IsDisabled)));
    }
}