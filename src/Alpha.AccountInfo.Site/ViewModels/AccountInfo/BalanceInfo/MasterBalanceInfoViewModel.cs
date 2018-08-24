namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
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

        public override decimal TotalBalance => RoundNumber(model.MasterCash + model.AgentCash + model.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(model.Yesterday_MasterCash + model.Yesterday_AgentCash + model.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(model.MasterPositionTaking);

        public decimal UsedCredit => RoundNumber(model.MasterUsedCredit);

        public string GivenCreditLimit => $">={MasterCredit_Min} {Resources.And} <={MasterCredit_Max}";

        public decimal GivenCredit => RoundNumber(model.Credit);

        public string OutstandingLink => message.ViewReports && Outstanding > 0 ?
               $@"{ReportPath}outstanding/Master?CustId={model.CustId}&CustName={message.UserName}
                &CustLevelId=3&BreadcrumbLevelJson={BreadcrumbLevelJson}" : string.Empty;

        protected override bool ShowTransferLink => !message.AllDayBalTransfer && message.AllowTransfer;

        private decimal MasterCredit_Min => model.MasterCredit_Min;

        private decimal MasterCredit_Max => model.MasterCredit_Max;

        protected override string BuildTransferLink(decimal transferValue) =>
            $@"{OldMainPath}_Transfer/Super/MasterTransfer.aspx?custid={model.CustId}&status=1
            &CallBack=false&username={message.UserName}&isyesterday=1&amt={transferValue}";
    }
}