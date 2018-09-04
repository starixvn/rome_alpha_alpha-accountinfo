namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Shared;

    public class MemberBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly MemberBalanceInfoModel balanceInfoModel;

        public MemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            IBalanceInfoModel balanceInfoModel,
            CustomerInfoModel customerInfoModel) : base(message, customerInfoModel)
        {
            this.balanceInfoModel = (MemberBalanceInfoModel)balanceInfoModel;
        }

        public override decimal TotalBalance => RoundNumber(balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(balanceInfoModel.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(balanceInfoModel.CustomerOutstanding);

        public decimal NetPosition => RoundNumber(balanceInfoModel.NetPosition);

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public decimal BetCredit => RoundNumber(GivenCredit + balanceInfoModel.CustomerCash - Outstanding + NetPosition);

        public string BetListLink => message.ViewReports && Outstanding > 0 ?
            $@"{BetListsUrl}RunningBetList/Index?CustID={balanceInfoModel.CustId}
            &amp;CustName=ACGUUS3838033&amp;GetAll=True" : string.Empty;

        public string GivenCreditLimit => $"<={MemMaxCredit_Max}";

        private decimal MemMaxCredit_Max => balanceInfoModel.MemMaxCredit_Max;

        private UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId);

        protected override bool ShowTransferLink 
            => ActorLevel.IsAgentLevel()&& !message.AllDayBalTransfer && message.AllowTransfer;

        protected override string BuildTransferLink(decimal transferValue) =>
           $"{OldMainUrl}_Transfer/Agent/MemberTransfer.aspx?custid={balanceInfoModel.CustId}&CallBack=false";
    }
}