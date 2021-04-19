namespace Alpha.AccountInfo.Features.OpenDownlines.Params
{
    using SearchAccount.Params.SPs;

    public class SelectCloseInfoCriteria : AccountInfoCriteriaBase
    {
        public SelectCloseInfoCriteria(int searchAccountId) : base(searchAccountId)
        {
        }

        public override string GetSettingKey() => "Acc_Customer_SuspendInfoSel";
    }
}