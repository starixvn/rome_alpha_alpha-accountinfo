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
    using System.Threading.Tasks;

    public class UnsuspendDownlinesHandler :
        IAsyncRequestHandler<UnsuspendViewListRequest, UnsuspendListViewModel>,
        IAsyncRequestHandler<UnsuspendSingleRequest, UnsuspendSingleViewModel>,
        IAsyncRequestHandler<UnsuspendMultipleRequest, UnsuspendMultipleViewModel>,
        IAsyncRequestHandler<UnsuspendSingleCommand, Result>,
        IAsyncRequestHandler<UnsuspendMultipleCommand, UnsuspendMultipleResultViewModel>
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

        public async Task<UnsuspendSingleViewModel> Handle(UnsuspendSingleRequest message)
            => new UnsuspendSingleViewModel(
                message.Language,
                new UnsuspendSingleDataViewModel (
                message.SearchAccountId,
                message.SearchAccountName,
                await unsuspendDownlinesService.GetUnsuspendInfoModel(message.SearchAccountId)
                .ConfigureAwait(false)));

        public async Task<UnsuspendListViewModel> Handle(UnsuspendViewListRequest message)
             => new UnsuspendListViewModel(
                 message.SearchAccountId,
                 message.SearchAccountName,
                 message.IsAffected,
                 await unsuspendDownlinesService.GetUnsuspendViewListModel(
                     message.SearchAccountId,
                     message.IsAffected,
                     message.DownlineRoleId,
                     message.PageSize,
                     message.PageIndex).ConfigureAwait(false));

        public async Task<UnsuspendMultipleViewModel> Handle(UnsuspendMultipleRequest message)
            => new UnsuspendMultipleViewModel(
                message.RoleId,
                message.Language,
                await unsuspendDownlinesService.GetUnsuspendMultipleModel(message.CustIds, message.RoleId).ConfigureAwait(false));

        public async Task<Result> Handle(UnsuspendSingleCommand message)
          => await accountInfoService.UpdateCustomerStatus(
              new UpdateCustomerStatusParam(
                  message.SearchAccountId,
                  message.ActorId,
                  message.SubAccountId,
                  message.Language,
                  message.IP,
                  false,
                  StatusNames.Suspended,
                  message.ActorName)).ConfigureAwait(false);

        public async Task<UnsuspendMultipleResultViewModel> Handle(UnsuspendMultipleCommand message)
        {
            var tasks = new List<Task<UnsuspendResult>>();
            foreach (var customer in message.UnsuspendList)
            {
                tasks.Add(UpdateSuspendAsync(message, customer));
            }

            return new UnsuspendMultipleResultViewModel(await Task.WhenAll(tasks).ConfigureAwait(false));
        }

        internal async Task<UnsuspendResult> UpdateSuspendAsync(UnsuspendMultipleCommand message, UnsuspendCustomerInfo customer)
            => new UnsuspendResult(
                customer.Username,
                customer.Suspended,
                await accountInfoService.UpdateCustomerStatus(
                      new UpdateCustomerStatusParam(
                          customer.SearchAccountId,
                          message.ActorId,
                          message.SubAccountId,
                          message.Language,
                          message.IP,
                          customer.Suspended,
                          StatusNames.Suspended,
                          message.ActorName)).ConfigureAwait(false));
    }
}