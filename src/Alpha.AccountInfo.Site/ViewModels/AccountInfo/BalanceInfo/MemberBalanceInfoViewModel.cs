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

        public decimal TotalBalance => RoundNumber(model.CustomerCash);

        public decimal YesterdayBalance => RoundNumber(model.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(model.CustomerOutstanding);

        public decimal NetPosition => RoundNumber(model.NetPosition);

        public decimal GivenCredit => RoundNumber(model.Credit);

        public decimal BetCredit => RoundNumber(GivenCredit + model.CustomerCash - Outstanding + NetPosition);

        public bool ShowTransferLink =>
            Enumeration.FromValue<UserLevel>(message.ActorRoleId).IsAgentLevel() 
            && !message.AllDayBalTransfer
            && message.AllowTransfer;

        // Show TransferLink On TotalBalance or YesterdayBalance
        public bool AllowedTotalBalanceTransfer => message.AllowedTotalBalanceTransfer;

        public string TransferLink => $"{OldMainPath}_Transfer/Agent/MemberTransfer.aspx?custid={model.CustId}&CallBack=false";

        public string BetListLink => message.ViewReports && Outstanding > 0 ?
            $@"{BetListPath}RunningBetList/Index?CustID={model.CustId}
                &amp;CustName=ACGUUS3838033&amp;GetAll=True" : string.Empty;

        public string GivenCreditLimit => $"<={MemMaxCredit_Max}";

        private decimal MemMaxCredit_Max => model.MemMaxCredit_Max;
    }
}