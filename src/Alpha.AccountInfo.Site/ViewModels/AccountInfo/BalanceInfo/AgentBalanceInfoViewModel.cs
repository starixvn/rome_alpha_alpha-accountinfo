namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Helpers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class AgentBalanceInfoViewModel
    {
        private readonly BalanceInfoModel model;
        private readonly CustomerInfoQuery message;

        public AgentBalanceInfoViewModel(CustomerInfoQuery message, BalanceInfoModel model)
        {
            this.model = model;
            this.message = message;
        }

        public decimal TotalCash => model.AgentCash;

        public decimal YesterdayCash => model.Yesterday_AgentCash;

        public decimal TotalBalance => model.AgentCash + model.CustomerCash;

        public decimal YesterdayBalance => model.Yesterday_AgentCash + model.Yesterday_CustomerCash;

        public decimal Outstanding => model.AgentPositionTaking;

        public decimal MemberMaxCredit => model.MemberMaxCredit;

        public decimal UsedCredit => model.AgentUsedCredit;

        public decimal GivenCredit => model.Credit;

        public string GivenCreditLimit => $">={AgentCredit_Min} and <={AgentCredit_Max}";

        public bool ShowTransferLink => !message.AllDayBalTransfer && !IsSuperLevel && message.AllowTransfer;

        // Show TransferLink On TotalBalance or YesterdayBalance
        public bool AllowedTotalBalanceTransfer => message.AllowedTotalBalanceTransfer;

        public string TransferLink
        {
            get
            {
                var atm = message.AllowedTotalBalanceTransfer ? TotalBalance : YesterdayBalance;
                return $"{OldMainPath}_Transfer/Master/AgentTransfer.aspx?amt={atm}&custid={model.CustId}&status=1&CallBack=false&username={message.UserName}&isyesterday=1";
            }
        }
        public string MemberMaxCreditLimit => IsSuperLevel ? string.Empty : $">={MemMaxCredit_Min} and <={GivenCredit}";

        public bool ShowOutstandingLink => message.ViewReports && Outstanding > 0;

        public string OutstandingLink =>
            $"{OldMainPath}outstanding/Master?CustId={model.CustId}&CustName={message.UserName}&CustLevelId=2&BreadcrumbLevelJson="; //BuildBreadcrumbLevelJson()

        private decimal MemMaxCredit_Min => model.MemMaxCredit_Min;

        private decimal AgentCredit_Min => model.AgentCredit_Min;

        private decimal AgentCredit_Max => model.AgentCredit_Max;

        private bool IsSuperLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId).IsSuperLevel();

        private static string OldMainPath => UrlHelper.MapOldMainPath("~/");
    }
}