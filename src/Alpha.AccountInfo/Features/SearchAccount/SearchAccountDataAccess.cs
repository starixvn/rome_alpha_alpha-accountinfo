namespace Alpha.AccountInfo.DataAccess
{
    using System.Data;
    using System.Linq;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Fanex.Data;

    public interface SearchAccountDataAccess
    {
        CustomerStatusModel GetCustomerAllStatus(SelectCustomerAllStatusModelCriteria criteria);

        DownlineListModel GetDownlineList(SelectDownlineListCriteria criteria);

        Result UpdateCustomerStatus(UpdateCustomerStatusCommand command);
    }

    public class SearchAccountDataAccessImpl : SearchAccountDataAccess
    {
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

        public Result UpdateCustomerStatus(UpdateCustomerStatusCommand command)
        {
            using (var objectDb = ObjectDbFactory.CreateInstance(command.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(command.GetSpParams());

                var outputParams = objectDb.GetOuputParams();

                return new Result(
                    int.Parse(outputParams["errcode"].ToString()), 
                    outputParams["errmsg"]?.ToString());
            }
        }
    }
}