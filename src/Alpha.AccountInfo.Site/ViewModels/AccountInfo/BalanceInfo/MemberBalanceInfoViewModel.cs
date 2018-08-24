namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class MemberBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        public MemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            BalanceInfoModel model,
            CustomerInfoModel customerInfoModel) : base(message, model, customerInfoModel)
        {
        }

        public override decimal TotalBalance => RoundNumber(model.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(model.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(model.CustomerOutstanding);

        public decimal NetPosition => RoundNumber(model.NetPosition);

        public decimal GivenCredit => RoundNumber(model.Credit);

        public decimal BetCredit => RoundNumber(GivenCredit + model.CustomerCash - Outstanding + NetPosition);

        public string BetListLink => message.ViewReports && Outstanding > 0 ?
            $@"{BetListPath}RunningBetList/Index?CustID={model.CustId}
            &amp;CustName=ACGUUS3838033&amp;GetAll=True" : string.Empty;

        public string GivenCreditLimit => $"<={MemMaxCredit_Max}";

        private decimal MemMaxCredit_Max => model.MemMaxCredit_Max;

        protected override bool ShowTransferLink =>
          Enumeration.FromValue<UserLevel>(message.ActorRoleId).IsAgentLevel()
          && !message.AllDayBalTransfer
          && message.AllowTransfer;

        protected override string BuildTransferLink(decimal transferValue) =>
           $"{OldMainPath}_Transfer/Agent/MemberTransfer.aspx?custid={model.CustId}&CallBack=false";
    }
}