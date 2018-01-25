namespace Alpha.AccountInfo.Site.Handlers
{
    using System.Threading.Tasks;
    using AccountInfo.Models;
    using MediatR;
    using Requests.ProductStatus;
    using Services.ProductStatus;
    using ViewModels.ProductStatus;

    public class ProductStatusHandler :
        IAsyncRequestHandler<ProductStatusQuery, ProductStatusViewModel>,
        IAsyncRequestHandler<ProductStatusCommand, Result>
    {
        private readonly ProductStatusService statusService;
        private readonly ProductStatusAuthorizationService authorizationService;

        public ProductStatusHandler(
            ProductStatusService statusService,
            ProductStatusAuthorizationService authorizationService)
        {
            this.statusService = statusService;
            this.authorizationService = authorizationService;
        }

        public async Task<ProductStatusViewModel> Handle(ProductStatusQuery message)
        {
            if (!authorizationService.CanView(message.ToProductStatusAuthorizationParam()))
            {
                return null;
            }

            var model = await statusService.GetProductStatusAsync(message.ProductId, message.CustId, message.CustRoleId);

            return message.ToProductStatusViewModel(model);
        }

        public async Task<Result> Handle(ProductStatusCommand message)
        {
            if (!authorizationService.CanUpdate(message.ToProductStatusAuthorizationParam()))
            {
                return Result.CreateForbiddenResult();
            }

            return await statusService.UpdateProductStatusAsync(message.ToUpdateProductStatusParam());
        }
    }
}