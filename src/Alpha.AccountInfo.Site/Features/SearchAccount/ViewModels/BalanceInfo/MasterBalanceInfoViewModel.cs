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

        public decimal TotalCash => RoundDownNumber(balanceInfoModel.MasterCash);

        public decimal YesterdayCash => RoundDownNumber(balanceInfoModel.YesterdayMasterCash);

        public override decimal TotalBalance
            => RoundDownNumber(
                balanceInfoModel.MasterCash + 
                balanceInfoModel.AgentCash + 
                balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance
            => RoundDownNumber(
                balanceInfoModel.YesterdayMasterCash + 
                balanceInfoModel.YesterdayAgentCash + 
                balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundDownNumber(balanceInfoModel.MasterPositionTaking);

        public decimal UsedCredit => RoundDownNumber(balanceInfoModel.MasterUsedCredit);

        public decimal GivenCredit => RoundDownNumber(balanceInfoModel.Credit);

        public decimal GivenCreditMin => balanceInfoModel.MasterCreditMin;

        public decimal GivenCreditMax => balanceInfoModel.MasterCreditMax;
    }
}