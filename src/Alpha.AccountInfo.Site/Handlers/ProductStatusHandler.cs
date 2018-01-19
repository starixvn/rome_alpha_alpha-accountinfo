namespace Alpha.AccountInfo.Site.Handlers
{
    using System.Threading.Tasks;
    using AccountInfo.Models;
    using AppResources;
    using Enumerations;
    using MediatR;
    using Requests.ProductStatus;
    using Services.ProductStatus;
    using ViewModels.ProductStatus;

    public class ProductStatusHandler :
        IAsyncRequestHandler<ProductStatusQuery, ProductStatusViewModel>,
        IAsyncRequestHandler<ProductStatusCommand, Result>
    {
        private readonly IUser user;
        private readonly ProductStatusService service;

        public ProductStatusHandler(IUser user, ProductStatusService service)
        {
            this.user = user;
            this.service = service;
        }

        public async Task<ProductStatusViewModel> Handle(ProductStatusQuery message)
        {
            var loggedInUserRole = Enumeration.FromValue<UserLevel>(user.LevelId);
            var isReadOnly = user.IsInternal || !user.CanUpdateAccount;

            var model = await service.GetProductStatusAsync(message.ProductId, message.CustId, message.CustRoleId);

            return new ProductStatusViewModel(
                message.ProductId,
                message.CustId,
                message.CustName,
                Enumeration.FromValue<UserLevel>(message.CustRoleId),
                model,
                loggedInUserRole,
                isReadOnly);
        }

        public async Task<Result> Handle(ProductStatusCommand message)
        {
            var isReadOnly = user.IsInternal || !user.CanUpdateAccount;

            if (isReadOnly)
            {
                return Result.CreateForbiddenResult();
            }

            return Result.CreateSuccessResult(ProductStatus.StatusUpdatedSuccessfully);
        }
    }
}