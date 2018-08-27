namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class MasterBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly MasterBalanceInfoModel balanceInfoModel;

        public MasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            IBalanceInfoModel balanceInfoModel,
            CustomerInfoModel customerInfoModel) : base(message, customerInfoModel)
        {
            this.balanceInfoModel = (MasterBalanceInfoModel)balanceInfoModel;
        }

        public decimal TotalCash => RoundNumber(balanceInfoModel.MasterCash);

        public decimal YesterdayCash => RoundNumber(balanceInfoModel.Yesterday_MasterCash);

        public override decimal TotalBalance
            => RoundNumber(balanceInfoModel.MasterCash + balanceInfoModel.AgentCash + balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance 
            => RoundNumber(balanceInfoModel.Yesterday_MasterCash + balanceInfoModel.Yesterday_AgentCash + balanceInfoModel.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(balanceInfoModel.MasterPositionTaking);

        public decimal UsedCredit => RoundNumber(balanceInfoModel.MasterUsedCredit);

        public string GivenCreditLimit => $">={MasterCredit_Min} {Resources.And} <={MasterCredit_Max}";

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public string OutstandingLink => message.ViewReports && Outstanding > 0 ?
               $@"{ReportPath}outstanding/Master?CustId={balanceInfoModel.CustId}&CustName={message.UserName}
                &CustLevelId=3&BreadcrumbLevelJson={BreadcrumbLevelJson}" : string.Empty;

        protected override bool ShowTransferLink => !message.AllDayBalTransfer && message.AllowTransfer;

        private decimal MasterCredit_Min => balanceInfoModel.MasterCredit_Min;

        private decimal MasterCredit_Max => balanceInfoModel.MasterCredit_Max;

        protected override string BuildTransferLink(decimal transferValue) =>
            $@"{OldMainPath}_Transfer/Super/MasterTransfer.aspx?custid={balanceInfoModel.CustId}&status=1
            &CallBack=false&username={message.UserName}&isyesterday=1&amt={transferValue}";
    }
}