namespace Alpha.AccountInfo.DataAccess
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public interface SearchAccountDataAccess
    {
        CustomerStatusModel GetCustomerAllStatus(SelectCustomerAllStatusModelCriteria criteria);

        DownlineListModel GetDownlineList(SelectDownlineListCriteria criteria);

        IEnumerable<OnlineUserModel> GetOnlineList(SelectOnlineListCriteria criteria);

        Result UpdateGivenCredit(UpdateGivenCreditCommand command);

        Result UpdateCustomerStatus(UpdateCustomerStatusCommand command);
    }

    public class SearchAccountDataAccessImpl : SearchAccountDataAccess
    {
        private readonly int failErrorCode = 100;
        private readonly IDynamicRepository dynamicRepository;

        public SearchAccountDataAccessImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public CustomerStatusModel GetCustomerAllStatus(SelectCustomerAllStatusModelCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var objectDb = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var customerStatus = objectDb.Read<NonProductStatusModel>().FirstOrDefault();
                    var nonDCSProduct = objectDb.Read<NonDCSProductStatusModel>().FirstOrDefault();
                    var dcsProducts = objectDb.Read<DCSProductStatusModel>();

                    return new CustomerStatusModel(
                        customerStatus,
                        nonDCSProduct,
                        dcsProducts
                            .GroupBy(product => product.ProductId)
                            .Select(group => group.First()));
                }
            }
        }

        public DownlineListModel GetDownlineList(SelectDownlineListCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var objectDb = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var downlines = objectDb.Read<DownlineDto>();
                    var downlineListModel = objectDb.Read<DownlineListModel>().FirstOrDefault();

                    if (downlineListModel != null)
                    {
                        return new DownlineListModel(downlines, downlineListModel);
                    }

                    return downlineListModel;
                }
            }
        }

        public IEnumerable<OnlineUserModel> GetOnlineList(SelectOnlineListCriteria criteria)
            => dynamicRepository.Fetch<OnlineUserModel>(criteria);

        public Result UpdateGivenCredit(UpdateGivenCreditCommand command)
        {
            dynamicRepository.Execute(command);

            return command.CreditProblem
                ? Result.CreateErrorResult(failErrorCode, command.ErrMsg)
                : Result.CreateSuccessResult();
        }

        public Result UpdateCustomerStatus(UpdateCustomerStatusCommand command)
        {
            dynamicRepository.Execute(command);

            return new Result(command.ErrCode, command.ErrMsg);
        }
    }
}