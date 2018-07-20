namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class MemberBalanceInfoViewModel
    {
        private readonly BalanceInfoModel model;
        private readonly MemberBalanceInfoQuery message;

        public MemberBalanceInfoViewModel(MemberBalanceInfoQuery message, BalanceInfoModel model)
        {
            this.model = model;
            this.message = message;
        }

        public decimal TotalCash => model.CustomerCash;

        public decimal YesterdayCash => model.Yesterday_CustomerCash;

        public decimal TotalBalance => TotalCash;

        public decimal YesterdayBalance => YesterdayCash;

        public bool ShowTransferLink =>
            Enumeration.FromValue<UserLevel>(message.ActorId).IsAgentLevel() 
            && !message.AllDayBalTransfer
            && message.AllowTransfer;

        // Show TransferLink On TotalBalance or YesterdayBalance
        public bool ShowTransferLinkOnTotalBalance => message.AllowedTotalBalanceTransfer;

        public bool ShowBetListLink => message.ViewReports && Outstanding > 0;

        public string TransferLink => $"../_Transfer/Agent/MemberTransfer.aspx?custid={model.CustId}&CallBack=false";

        public decimal Outstanding => model.CustomerOutstanding;

        public decimal NetPostion => model.NetPosition;

        public decimal GivenCredit => model.Credit;

        public decimal BetCredit => GivenCredit + TotalCash - Outstanding + NetPostion;

        public decimal MemMaxCredit_Max => model.MemMaxCredit_Max;

        public string GivenCreditLimit => $"<={MemMaxCredit_Max}";
    }
}