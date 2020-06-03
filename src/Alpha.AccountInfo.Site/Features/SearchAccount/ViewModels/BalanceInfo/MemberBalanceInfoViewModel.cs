namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;

    public class MemberBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly MemberBalanceInfoModel balanceInfoModel;

        public MemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfoModel,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel,
            Func<string, string, string> getMicroSiteUrl = null)
            : base(message, customerInfoModel, creditInfo, getMicroSiteUrl)
        {
            this.balanceInfoModel = (MemberBalanceInfoModel)balanceInfoModel;
        }

        public override decimal TotalBalance => RoundUpNumber(balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundUpNumber(balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundUpNumber(balanceInfoModel.CustomerOutstanding);

        public decimal NetPosition => RoundDownNumber(balanceInfoModel.NetPosition);

        public override decimal GivenCredit => RoundUpNumber(creditInfo.Credit);

        public decimal BetCredit => RoundDownNumber(creditInfo.Credit + balanceInfoModel.Balance + balanceInfoModel.NetPosition);

        public override decimal GivenCreditMax => creditInfo.MemMaxCreditMax;

        public override decimal GivenCreditMin => 0;

        public override decimal PendingTransfer3rd => balanceInfoModel.PendingTransfer3rd;

        internal override bool ShowTransferLink => ActorLevel.IsAgentLevel() && base.ShowTransferLink;
    }
}