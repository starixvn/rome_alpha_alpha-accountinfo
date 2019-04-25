namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnsuspendDownlinesHandler :
        IAsyncRequestHandler<UnsuspendViewListRequest, UnsuspendListViewModel>,
        IAsyncRequestHandler<UnsuspendSingleRequest, UnsuspendInfoViewModel>,
        IAsyncRequestHandler<UnsuspendMultipleRequest, IEnumerable<UnsuspendInfoMultipleViewModel>>,
        IAsyncRequestHandler<UnsuspendSingleCommand, Result>,
        IAsyncRequestHandler<UnsuspendMultipleCommand, IEnumerable<Result>>
    {
        private readonly UnsuspendDownlinesService unsuspendDownlinesService;
        private readonly SearchAccountService accountInfoService;

        public UnsuspendDownlinesHandler(
            UnsuspendDownlinesService unsuspendDownlinesService,
            SearchAccountService accountInfoService)
        {
            this.unsuspendDownlinesService = unsuspendDownlinesService;
            this.accountInfoService = accountInfoService;
        }

        public async Task<UnsuspendInfoViewModel> Handle(UnsuspendSingleRequest message)
            => new UnsuspendInfoViewModel(
                message.SearchAccountId,
                await unsuspendDownlinesService.GetUnsuspendInfoModel(message.SearchAccountId)
                .ConfigureAwait(false));

        public async Task<UnsuspendListViewModel> Handle(UnsuspendViewListRequest message)
             => new UnsuspendListViewModel(
                 message.SearchAccountId,
                 await unsuspendDownlinesService.GetUnsuspendViewListModel(
                     message.SearchAccountId,
                     message.IsAffected,
                     message.DownlineRoleId,
                     message.PageSize,
                     message.PageIndex).ConfigureAwait(false));

        public async Task<IEnumerable<UnsuspendInfoMultipleViewModel>> Handle(UnsuspendMultipleRequest message)
        {
            var model = await unsuspendDownlinesService.GetUnsuspendMultipleModel(message.CustIds, message.RoleId).ConfigureAwait(false);

            return model.GroupBy(item => item.CustId).Select(group =>
            {
                var firstItem = group.FirstOrDefault();
                return new UnsuspendInfoMultipleViewModel(
                     group.Key,
                     message.RoleId,
                     firstItem.Username,
                     firstItem.Suspended,
                     message.RoleId,
                     group.OrderByDescending(level=> level.RoleId));
            }).OrderBy(group => group.Username);
        }

        public Task<Result> Handle(UnsuspendSingleCommand message)
          => accountInfoService.UpdateCustomerStatus(
              new UpdateCustomerStatusParam(
                  message.SearchAccountId,
                  message.ActorId,
                  message.SubAccountId,
                  message.Language,
                  message.IP,
                  false,
                  StatusNames.Suspended,
                  message.ActorName,
                  false));

        public async Task<IEnumerable<Result>> Handle(UnsuspendMultipleCommand message)
        {
            var results = await Task.WhenAll(message.UnsuspendList.Select(
                customer => accountInfoService.UpdateCustomerStatus(
                     new UpdateCustomerStatusParam(
                         customer.CustId,
                         message.ActorId,
                         message.SubAccountId,
                         message.Language,
                         message.IP,
                         customer.Suspended,
                         StatusNames.Suspended,
                         message.ActorName,
                         false)))).ConfigureAwait(false);

            return results.Where(result => !result.IsSuccess);
        }
    }
}