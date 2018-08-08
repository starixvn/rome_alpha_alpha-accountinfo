namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class MasterBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        public MasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            BalanceInfoModel model,
            CustomerInfoModel customerInfoModel) : base(message, model, customerInfoModel)
        {
        }

        public decimal TotalCash => RoundNumber(model.MasterCash);

        public decimal YesterdayCash => RoundNumber(model.Yesterday_MasterCash);

        public decimal TotalBalance => RoundNumber(model.MasterCash + model.AgentCash + model.CustomerCash);

        public decimal YesterdayBalance => RoundNumber(model.Yesterday_MasterCash + model.Yesterday_AgentCash + model.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(model.MasterPositionTaking);

        public decimal UsedCredit => RoundNumber(model.MasterUsedCredit);

        public decimal GivenCredit => RoundNumber(model.Credit);

        public bool ShowTransferLink => !message.AllDayBalTransfer && message.AllowTransfer;

        // Show TransferLink On TotalBalance or YesterdayBalance
        public bool AllowedTotalBalanceTransfer => message.AllowedTotalBalanceTransfer;

        public string TransferLink
        {
            get
            {
                var atm = message.AllowedTotalBalanceTransfer ? TotalBalance : YesterdayBalance;
                return $"{OldMainPath}_Transfer/Super/MasterTransfer.aspx?amt={atm}&custid={model.CustId}&status=1&CallBack=false&username={message.UserName}&isyesterday=1";
            }
        }

        public bool ShowOutstandingLink => message.ViewReports && Outstanding > 0;

        public string OutstandingLink => $"{ReportPath}outstanding/Master?CustId={model.CustId}&CustName={message.UserName}&CustLevelId=3&BreadcrumbLevelJson={BreadcrumbLevelJson}";

        public string GivenCreditLimit => $">={MasterCredit_Min} and <={MasterCredit_Max}";

        private decimal MasterCredit_Min => model.MasterCredit_Min;

        private decimal MasterCredit_Max => model.MasterCredit_Max;
    }
}