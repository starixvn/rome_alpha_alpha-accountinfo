namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;

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

        public override decimal TotalBalance => RoundDownNumber(balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundDownNumber(balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundDownNumber(balanceInfoModel.CustomerOutstanding);

        public decimal NetPosition => RoundDownNumber(balanceInfoModel.NetPosition);

        public decimal GivenCredit => RoundDownNumber(balanceInfoModel.Credit);

        public decimal BetCredit => 
            RoundDownNumber(
                balanceInfoModel.Credit + 
                balanceInfoModel.CustomerCash - 
                balanceInfoModel.CustomerOutstanding + 
                balanceInfoModel.NetPosition);

        public decimal GivenCreditMax => balanceInfoModel.MemMaxCreditMax;

        internal override bool ShowTransferLink => ActorLevel.IsAgentLevel() && base.ShowTransferLink;
    }
}