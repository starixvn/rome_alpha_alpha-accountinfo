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

        public decimal TotalCash => RoundUpNumber(balanceInfoModel.MasterCash);

        public decimal YesterdayCash => RoundUpNumber(balanceInfoModel.YesterdayMasterCash);

        public override decimal TotalBalance
            => RoundUpNumber(
                balanceInfoModel.MasterCash + 
                balanceInfoModel.AgentCash + 
                balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance
            => RoundUpNumber(
                balanceInfoModel.YesterdayMasterCash + 
                balanceInfoModel.YesterdayAgentCash + 
                balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundUpNumber(balanceInfoModel.MasterPositionTaking);

        public decimal UsedCredit => RoundUpNumber(balanceInfoModel.MasterUsedCredit);

        public decimal GivenCredit => RoundUpNumber(balanceInfoModel.Credit);

        public decimal GivenCreditMin => balanceInfoModel.MasterCreditMin;

        public decimal GivenCreditMax => balanceInfoModel.MasterCreditMax;
    }
}