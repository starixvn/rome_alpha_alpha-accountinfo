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

        public decimal TotalBalance => model.CustomerCash;

        public decimal YesterdayBalance => model.Yesterday_CustomerCash;

        public decimal Outstanding => model.CustomerOutstanding;

        public decimal NetPosition => model.NetPosition;

        public decimal GivenCredit => model.Credit;

        public decimal BetCredit => GivenCredit + model.CustomerCash - Outstanding + NetPosition;

        public bool ShowTransferLink =>
            Enumeration.FromValue<UserLevel>(message.ActorRoleId).IsAgentLevel() 
            && !message.AllDayBalTransfer
            && message.AllowTransfer;

        // Show TransferLink On TotalBalance or YesterdayBalance
        public bool AllowedTotalBalanceTransfer => message.AllowedTotalBalanceTransfer;

        public string TransferLink => $"{OldMainPath}_Transfer/Agent/MemberTransfer.aspx?custid={model.CustId}&CallBack=false";

        public bool ShowBetListLink => message.ViewReports && Outstanding > 0;
        
        public string BetListLink => $"{BetListPath}RunningBetList/Index?CustID={model.CustId}&amp;CustName=ACGUUS3838033&amp;GetAll=True";

        public string GivenCreditLimit => $"<={MemMaxCredit_Max}";

        private decimal MemMaxCredit_Max => model.MemMaxCredit_Max;
    }
}