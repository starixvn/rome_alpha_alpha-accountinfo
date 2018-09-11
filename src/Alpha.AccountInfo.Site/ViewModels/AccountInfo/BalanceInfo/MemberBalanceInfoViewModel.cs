namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using System;

    public class MemberBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly MemberBalanceInfoModel balanceInfoModel;

        public MemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            IBalanceInfoModel balanceInfoModel,
            CustomerInfoModel customerInfoModel,
            Func<string, string, string> getMicroSiteUrl = null) 
            : base(message, customerInfoModel, getMicroSiteUrl)
        {
            this.balanceInfoModel = (MemberBalanceInfoModel)balanceInfoModel;
        }

        public override decimal TotalBalance => RoundNumber(balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(balanceInfoModel.Yesterday_CustomerCash);

        public override decimal Outstanding => RoundNumber(balanceInfoModel.CustomerOutstanding);

        public decimal NetPosition => RoundNumber(balanceInfoModel.NetPosition);

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public decimal BetCredit => RoundNumber(GivenCredit + balanceInfoModel.CustomerCash - Outstanding + NetPosition);

        public string GivenCreditLimit => $"<={MemMaxCredit_Max}";

        internal override bool ShowTransferLink => ActorLevel.IsAgentLevel() && base.ShowTransferLink;

        private decimal MemMaxCredit_Max => balanceInfoModel.MemMaxCredit_Max;
    }
}