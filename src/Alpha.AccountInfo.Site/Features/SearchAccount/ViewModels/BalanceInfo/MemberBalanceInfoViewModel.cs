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

        public override decimal TotalBalance => RoundNumber(balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundNumber(balanceInfoModel.CustomerOutstanding);

        public decimal NetPosition => RoundNumber(balanceInfoModel.NetPosition);

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public decimal BetCredit => RoundNumber(GivenCredit + balanceInfoModel.CustomerCash - Outstanding + NetPosition);

        public decimal GivenCreditMax => balanceInfoModel.MemMaxCreditMax;

        internal override bool ShowTransferLink => ActorLevel.IsAgentLevel() && base.ShowTransferLink;
    }
}