namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Logging.Extensions.Web;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static Alpha.AccountInfo.Constants.AccountInfoDataKey;

    public class UpdateCustomerStatusCriteria : CriteriaBase
    {
        private readonly IDictionary<string, string> CustomerStatusSettingKeys =
         new Dictionary<string, string>
         {
                { StatusMapping.DisableAutoMasterPT, "Acc_CustomerUpdMaster_DisableAutoMPT" },
                { StatusMapping.Closed, "Acc_CustomerUpdClosed" },
                { StatusMapping.DoubleComm, "Acc_CustomerUpdDoubleCommLosing" },
                { StatusMapping.DisableOutright, "Acc_CustomerUpdDisableOutright" },
                { StatusMapping.Suspended, "Acc_CustomerUpdSuspended" },
         };

        public UpdateCustomerStatusCriteria(
            int searchAccountId,
            int actorId,
            int subAccountId,
            string language,
            string ip, 
            string statusName)
        {
            SearchAccountId = searchAccountId;
            ActorId = actorId;
            SubAccountId = subAccountId;
            Language = language;
            IP = ip;
            StatusName = statusName;
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        [SpParam(SqlParamName = "UserId")]
        public int ActorId { get; }

        [SpParam(SqlParamName = "SubAccId")]
        public int SubAccountId { get; }

        public string Language { get; }

        public string IP { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public string StatusName { get; }

        public override string GetSettingKey()
        {
            try
            {
                return CustomerStatusSettingKeys[StatusName];
            }
            catch (Exception exception)
            {
                ExceptionHandler.Handle(exception, TraceEventType.Error);
                return string.Empty;
            }
        }

        public override bool IsValid() => SearchAccountId > 0 && ActorId > 0;
    }
}
