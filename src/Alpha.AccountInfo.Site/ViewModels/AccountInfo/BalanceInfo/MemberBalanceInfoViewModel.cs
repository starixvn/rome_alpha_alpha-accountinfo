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

        public decimal TotalBalance => model.CustomerCash;

        public decimal YesterdayBalance => model.Yesterday_CustomerCash;

        public decimal Outstanding => model.CustomerOutstanding;

        public decimal NetPostion => model.NetPosition;

        public decimal GivenCredit => model.Credit;

        public decimal BetCredit => GivenCredit + model.CustomerCash - Outstanding + NetPostion;

        public bool ShowTransferLink =>
            Enumeration.FromValue<UserLevel>(message.ActorId).IsAgentLevel() 
            && !message.AllDayBalTransfer
            && message.AllowTransfer;

        // Show TransferLink On TotalBalance or YesterdayBalance
        public bool AllowedTotalBalanceTransfer => message.AllowedTotalBalanceTransfer;

        public string TransferLink => $"../_Transfer/Agent/MemberTransfer.aspx?custid={model.CustId}&CallBack=false";

        public bool ShowBetListLink => message.ViewReports && Outstanding > 0;
        
        public string BetListLink => $"/site-betlists/(S(0q5s4rp4cfwvxiwmxikqhavv))/RunningBetList/Index?CustID={model.CustId}&amp;CustName=ACGUUS3838033&amp;GetAll=True";

        public string GivenCreditLimit => $"<={MemMaxCredit_Max}";

        private decimal MemMaxCredit_Max => model.MemMaxCredit_Max;
    }
}