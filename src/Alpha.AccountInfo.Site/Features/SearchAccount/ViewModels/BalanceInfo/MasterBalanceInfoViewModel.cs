namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;

    public class MasterBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly MasterBalanceInfoModel balanceInfoModel;

        public MasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            IBalanceInfoModel balanceInfoModel,
            CustomerInfoModel customerInfoModel,
            Func<string, string, string> getMicroSiteUrl = null)
            : base(message, customerInfoModel, getMicroSiteUrl)
        {
            this.balanceInfoModel = (MasterBalanceInfoModel)balanceInfoModel;
        }

        public decimal TotalCash => RoundNumber(balanceInfoModel.MasterCash);

        public decimal YesterdayCash => RoundNumber(balanceInfoModel.YesterdayMasterCash);

        public override decimal TotalBalance
            => RoundNumber(balanceInfoModel.MasterCash + balanceInfoModel.AgentCash + balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance
            => RoundNumber(balanceInfoModel.YesterdayMasterCash + balanceInfoModel.YesterdayAgentCash + balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundNumber(balanceInfoModel.MasterPositionTaking);

        public decimal UsedCredit => RoundNumber(balanceInfoModel.MasterUsedCredit);

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public decimal GivenCreditMin => balanceInfoModel.MasterCreditMin;

        public decimal GivenCreditMax => balanceInfoModel.MasterCreditMax;
    }
}