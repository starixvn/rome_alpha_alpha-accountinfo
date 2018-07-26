namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class MasterBalanceInfoViewModel
    {
        private readonly BalanceInfoModel model;
        private readonly CustomerInfoQuery message;

        public MasterBalanceInfoViewModel(CustomerInfoQuery message, BalanceInfoModel model)
        {
            this.model = model;
            this.message = message;
        }

        public decimal TotalCash => model.MasterCash;

        public decimal YesterdayCash => model.Yesterday_MasterCash;

        public decimal TotalBalance => model.MasterCash + model.AgentCash + model.CustomerCash;

        public decimal YesterdayBalance => model.Yesterday_MasterCash + model.Yesterday_AgentCash + model.Yesterday_CustomerCash;

        public decimal Outstanding => model.MasterPositionTaking;

        public decimal UsedCredit => model.MasterUsedCredit;

        public decimal GivenCredit => model.Credit;

        public bool ShowTransferLink => !message.AllDayBalTransfer && message.AllowTransfer;

        // Show TransferLink On TotalBalance or YesterdayBalance
        public bool AllowedTotalBalanceTransfer => message.AllowedTotalBalanceTransfer;

        public string TransferLink
        {
            get
            {
                var atm = message.AllowedTotalBalanceTransfer ? TotalBalance : YesterdayBalance;
                return $"../_Transfer/Super/MasterTransfer.aspx?amt={atm}&custid={model.CustId}&status=1&CallBack=false&username={message.UserName}&isyesterday=1";
            }
        }

        public bool ShowOutstandingLink => message.ViewReports && Outstanding > 0;

        public string OutstandingLink =>
            $"../outstanding/Super?CustId={model.CustId}&CustName={message.UserName}&CustLevelId=3&BreadcrumbLevelJson="; //BuildBreadcrumbLevelJson()

        public string GivenCreditLimit => $">={MasterCredit_Min} and <={MasterCredit_Max}";

        private decimal MasterCredit_Min => model.MasterCredit_Min;

        private decimal MasterCredit_Max => model.MasterCredit_Max;

    }
}