namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
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

        public decimal TotalBalance => RoundNumber(model.AgentCash + model.CustomerCash);

        public decimal YesterdayBalance => RoundNumber(model.Yesterday_AgentCash + model.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(model.AgentPositionTaking);

        public decimal MemberMaxCredit => RoundNumber(model.MemberMaxCredit);

        public decimal UsedCredit => RoundNumber(model.AgentUsedCredit);

        public decimal GivenCredit => RoundNumber(model.Credit);

        public string GivenCreditLimit => $">={AgentCredit_Min} and <={AgentCredit_Max}";

        public bool ShowTransferLink => !message.AllDayBalTransfer && !IsSuperLevel && message.AllowTransfer;

        // Show TransferLink On TotalBalance or YesterdayBalance
        public bool AllowedTotalBalanceTransfer => message.AllowedTotalBalanceTransfer;

        public string TransferLink
        {
            get
            {
                var atm = message.AllowedTotalBalanceTransfer ? TotalBalance : YesterdayBalance;
                return $"{OldMainPath}_Transfer/Master/AgentTransfer.aspx?amt={atm}&custid={model.CustId}&status=1&CallBack=false&username={message.UserName}&isyesterday=1";
            }
        }
        public string MemberMaxCreditLimit => IsSuperLevel ? string.Empty : $">={MemMaxCredit_Min} and <={GivenCredit}";

        public bool ShowOutstandingLink => message.ViewReports && Outstanding > 0;

        public string OutstandingLink => $"{ReportPath}outstanding/Agent?CustId={model.CustId}&CustName={message.UserName}&CustLevelId=2&BreadcrumbLevelJson={BreadcrumbLevelJson}";

        private decimal MemMaxCredit_Min => model.MemMaxCredit_Min;

        private decimal AgentCredit_Min => model.AgentCredit_Min;

        private decimal AgentCredit_Max => model.AgentCredit_Max;

        private bool IsSuperLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId).IsSuperLevel();
    }
}