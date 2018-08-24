namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

    public class AgentBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        public AgentBalanceInfoViewModel( 
            CustomerInfoQuery message, 
            BalanceInfoModel model,
            CustomerInfoModel customerInfoModel) : base(message, model, customerInfoModel)
        {
        }

        public decimal TotalCash => RoundNumber(model.AgentCash);

        public decimal YesterdayCash => RoundNumber(model.Yesterday_AgentCash);

        public override decimal TotalBalance => RoundNumber(model.AgentCash + model.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(model.Yesterday_AgentCash + model.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(model.AgentPositionTaking);

        public decimal MemberMaxCredit => RoundNumber(model.MemberMaxCredit);

        public decimal UsedCredit => RoundNumber(model.AgentUsedCredit);

        public decimal GivenCredit => RoundNumber(model.Credit);

        public string GivenCreditLimit => $">={AgentCredit_Min} {Resources.And} <={AgentCredit_Max}";

        public string MemberMaxCreditLimit => ActorIsSuperLevel ? string.Empty : $">={MemMaxCredit_Min} {Resources.And} <={GivenCredit}";

        public string OutstandingLink => message.ViewReports && Outstanding > 0 ?
                $@"{ReportPath}outstanding/Agent?CustId={model.CustId}&CustName={message.UserName}
                &CustLevelId=2&BreadcrumbLevelJson={BreadcrumbLevelJson}" : string.Empty;

        protected override bool ShowTransferLink => !message.AllDayBalTransfer && !ActorIsSuperLevel && message.AllowTransfer;

        private decimal MemMaxCredit_Min => model.MemMaxCredit_Min;

        private decimal AgentCredit_Min => model.AgentCredit_Min;

        private decimal AgentCredit_Max => model.AgentCredit_Max;

        private bool ActorIsSuperLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId).IsSuperLevel();

        protected override string BuildTransferLink(decimal transferValue) =>
            $@"{OldMainPath}_Transfer/Master/AgentTransfer.aspx?custid={model.CustId}&status=1
            &CallBack=false&username={message.UserName}&isyesterday=1&&amt={transferValue}";

    }
}