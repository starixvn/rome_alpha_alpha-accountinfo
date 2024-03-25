namespace Alpha.AccountInfo.Site.Features.ProductStatusPopup
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AccountInfo.Models.ProductStatus;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.Requests;
    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.ViewModels;
    using Alpha.Products.Core.Shared;
    using MediatR;
    using Params.ProductStatus;
    using Services.ProductStatus;

    public class ProductStatusHandler :
        IAsyncRequestHandler<ProductStatusQuery, ProductStatusViewModel>,
        IAsyncRequestHandler<ProductStatusCommand, IEnumerable<Result>>
    {
        private readonly ProductStatusService statusService;

        public ProductStatusHandler(ProductStatusService statusService)
        {
            this.statusService = statusService;
        }

        public async Task<ProductStatusViewModel> Handle(ProductStatusQuery message)
        {
            var model = await statusService.GetProductStatusAsync(new ProductStatusAuthorizationParam(
                message.IsGroup,
                message.ProductId,
                message.CustId,
                message.CustRole,
                message.UplineId,
                message.UplineLevel)).ConfigureAwait(false);

            return ToProductStatusViewModel(message, model);
        }

        public async Task<IEnumerable<Result>> Handle(ProductStatusCommand message)
        {
            var commandParam = ToUpdateProductStatusParam(message);

            return await statusService.UpdateProductStatusAsync(commandParam).ConfigureAwait(false);
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
                message.IsViewOnly,
                message.IsGroup);

        private static ProductStatusCommandParam ToUpdateProductStatusParam(
            ProductStatusCommand message)
            => new ProductStatusCommandParam(
                false,
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
                message.BetTypeStatus.Select(status => new BetTypeStatusParam(status.SportType, status.BetType, status.IsDisabled)),
                message.Language);
    }
}