namespace Alpha.AccountInfo.Site.Features.OpenDownlines
{
    using Alpha.AccountInfo.Features.OpenDownlines;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Services.AccountInfo;
    using MediatR;
    using Requests;
    using SearchAccount.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OpenDownlinesHandler :
        IAsyncRequestHandler<OpenViewListRequest, OpenListViewModel>,
        IAsyncRequestHandler<OpenSingleRequest, OpenSingleViewModel>,
        IAsyncRequestHandler<OpenMultipleRequest, OpenMultipleViewModel>,
        IAsyncRequestHandler<OpenSingleCommand, Result>,
        IAsyncRequestHandler<OpenMultipleCommand, OpenMultipleResultViewModel>
    {
        private readonly OpenDownlinesService openDownlinesService;
        private readonly SearchAccountService searchAccountService;

        public OpenDownlinesHandler(
            OpenDownlinesService openDownlinesService,
            SearchAccountService searchAccountService)
        {
            this.openDownlinesService = openDownlinesService;
            this.searchAccountService = searchAccountService;
        }

        public async Task<OpenListViewModel> Handle(OpenViewListRequest message)
            => new OpenListViewModel(
                message.SearchAccountId,
                message.SearchAccountName,
                await openDownlinesService.GetOpenViewListModel(
                    message.SearchAccountId,
                    message.IsAffected,
                    message.DownlineRoleId,
                    message.PageSize,
                    message.PageIndex).ConfigureAwait(false));

        public async Task<OpenSingleViewModel> Handle(OpenSingleRequest message)
            => new OpenSingleViewModel(
                message.Language,
                new OpenSingleDataViewModel(
                    message.SearchAccountId,
                    message.SearchAccountName,
                    await openDownlinesService.GetOpenInfoModel(message.SearchAccountId)
                    .ConfigureAwait(false)));

        public async Task<OpenMultipleViewModel> Handle(OpenMultipleRequest message)
        => new OpenMultipleViewModel(
                message.RoleId,
                message.Language,
                await openDownlinesService.GetOpenMultipleModel(message.CustIds, message.RoleId).ConfigureAwait(false));

        public async Task<Result> Handle(OpenSingleCommand message)
        => await searchAccountService.UpdateCustomerStatus(
              new UpdateCustomerStatusParam(
                  message.SearchAccountId,
                  message.ActorId,
                  message.SubAccountId,
                  message.Language,
                  message.IP,
                  false,
                  StatusNames.Closed,
                  message.ActorName)).ConfigureAwait(false);

        public async Task<OpenMultipleResultViewModel> Handle(OpenMultipleCommand message)
        {
            var custIds = string.Join("^", message.OpenList.Select(opened => opened.SearchAccountId));

            var isValidCheckMultipleCustomesBelonging = await searchAccountService.CheckMultipleCustomesBelonging(
                message.ActorId, message.SubAccountId, custIds, message.IsInternal);

            if (isValidCheckMultipleCustomesBelonging)
            {
                var tasks = new List<Task<OpenResult>>();
                foreach (var customer in message.OpenList)
                {
                    tasks.Add(UpdateCloseAsync(message, customer));
                }

                return new OpenMultipleResultViewModel(await Task.WhenAll(tasks).ConfigureAwait(false));
            }

            return new OpenMultipleResultViewModel(
                new List<OpenResult> {
                    new OpenResult("", true, Result.CreateForbiddenResult())
                });
        }

        internal async Task<OpenResult> UpdateCloseAsync(OpenMultipleCommand message, OpenCustomerInfo customer)
        => new OpenResult(
           customer.Username,
           customer.IsClosed,
           await searchAccountService.UpdateCustomerStatus(
                  new UpdateCustomerStatusParam(
                      customer.SearchAccountId,
                      message.ActorId,
                      message.SubAccountId,
                      message.Language,
                      message.IP,
                      customer.IsClosed,
                      StatusNames.Closed,
                      message.ActorName)).ConfigureAwait(false));
    }
}