namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.AppResources;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnsuspendDownlinesHandler :
        IAsyncRequestHandler<UnsuspendViewListRequest, UnsuspendListViewModel>,
        IAsyncRequestHandler<UnsuspendSingleRequest, UnsuspendSingleViewModel>,
        IAsyncRequestHandler<UnsuspendMultipleRequest, UnsuspendMultipleViewModel>,
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

        public async Task<UnsuspendSingleViewModel> Handle(UnsuspendSingleRequest message)
            => new UnsuspendSingleViewModel(
                message.Language,
                new UnsuspendSingleDataViewModel (
                message.SearchAccountId,
                await unsuspendDownlinesService.GetUnsuspendInfoModel(message.SearchAccountId)
                .ConfigureAwait(false)));

        public async Task<UnsuspendListViewModel> Handle(UnsuspendViewListRequest message)
             => new UnsuspendListViewModel(
                 message.SearchAccountId,
                 await unsuspendDownlinesService.GetUnsuspendViewListModel(
                     message.SearchAccountId,
                     message.IsAffected,
                     message.DownlineRoleId,
                     message.PageSize,
                     message.PageIndex).ConfigureAwait(false));

        public async Task<UnsuspendMultipleViewModel> Handle(UnsuspendMultipleRequest message)
        {
            //var task = unsuspendDownlinesService.GetUnsuspendMultipleModel(message.CustIds, message.RoleId);
            //var data = task.Result.GroupBy(item => item.CustId).Select(group =>
            //{
            //    var firstItem = group.FirstOrDefault();
            //    return new UnsuspendMultipleDataViewModel(
            //         group.Key,
            //         message.RoleId,
            //         firstItem.Username,
            //         firstItem.Suspended,
            //         message.RoleId,
            //         group.OrderByDescending(level=> level.RoleId));
            //}).OrderBy(group => group.Username);

            //task.Wait();

            return new UnsuspendMultipleViewModel(message.Language, await unsuspendDownlinesService.GetUnsuspendMultipleModel(message.CustIds, message.RoleId).ConfigureAwait(false));
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

        internal async Task<UnsuspendResult> UpdateSuspendAsync(UnsuspendMultipleCommand message, UnsuspendCustomerInfo customer)
        {
            return new UnsuspendResult(
                customer.Username, 
                customer.Suspended,
                await accountInfoService.UpdateCustomerStatus(
                      new UpdateCustomerStatusParam(
                          customer.CustId,
                          message.ActorId,
                          message.SubAccountId,
                          message.Language,
                          message.IP,
                          customer.Suspended,
                          StatusNames.Suspended,
                          message.ActorName,
                          false)).ConfigureAwait(false));
        }

        public async Task<IEnumerable<Result>> Handle(UnsuspendMultipleCommand message)
        {
            var tasks = new List<Task<UnsuspendResult>>();
            foreach (var customer in message.UnsuspendList)
            {
                tasks.Add(UpdateSuspendAsync(message, customer));
            }

            var resultTasks = await Task.WhenAll(tasks).ConfigureAwait(false);

            var unsuspendFailedUsernames = resultTasks.Where(r => !r.Result.IsSuccess && !r.Suspended);
            var suspendFailedUsernames = resultTasks.Where(r => !r.Result.IsSuccess && r.Suspended);

            var results = unsuspendFailedUsernames.Concat(suspendFailedUsernames).Select(r => r.Result).ToList();

            var unsuspendSuccessresult = resultTasks.Where(r => r.Result.IsSuccess && !r.Suspended).Select(r => r.Username).ToList();
            if (unsuspendSuccessresult.Count > 0)
            {
                results.Add(Result.CreateSuccessResult(String.Format(UnsuspendDownlinesResources.UnsuspendedSuccessfully, string.Join(", ", unsuspendSuccessresult))));
            }

            var suspendSuccessresult = resultTasks.Where(r => r.Result.IsSuccess && r.Suspended).Select(r => r.Username).ToList();
            if (suspendSuccessresult.Count > 0)
            {
                results.Add(Result.CreateSuccessResult(String.Format(UnsuspendDownlinesResources.SuspendedSuccessfully, string.Join(", ", suspendSuccessresult))));
            }

            return results;
        }
    }
}