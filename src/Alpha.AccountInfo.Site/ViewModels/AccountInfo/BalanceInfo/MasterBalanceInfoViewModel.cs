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

        public string GivenCreditLimit => $">={MasterCredit_Min} and <={MasterCredit_Max}";

        public decimal GivenCredit => RoundNumber(model.Credit);

        public string OutstandingLink => message.ViewReports && Outstanding > 0 ?
               $@"{ReportPath}outstanding/Master?CustId={model.CustId}&CustName={message.UserName}
                &CustLevelId=3&BreadcrumbLevelJson={BreadcrumbLevelJson}" : string.Empty;
       
        public string TransferLinkOnTotalBalance =>
           ShowTransferLink && message.AllowedTotalBalanceTransfer ? BuildTransferLink(TotalBalance) : string.Empty;

        public string TransferLinkOnYesterdayBalance =>
            ShowTransferLink && !message.AllowedTotalBalanceTransfer ? BuildTransferLink(YesterdayBalance) : string.Empty;

        private bool ShowTransferLink => !message.AllDayBalTransfer && message.AllowTransfer;

        private decimal MasterCredit_Min => model.MasterCredit_Min;

        private decimal MasterCredit_Max => model.MasterCredit_Max;

        private string BuildTransferLink(decimal transferValue) =>
            $@"{OldMainPath}_Transfer/Super/MasterTransfer.aspx?custid={model.CustId}&status=1
            &CallBack=false&username={message.UserName}&isyesterday=1&amt={transferValue}";
    }
}