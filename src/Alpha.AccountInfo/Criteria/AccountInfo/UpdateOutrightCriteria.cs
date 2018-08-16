namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class UpdateOutrightCriteria : UpdateCustomerStatusCriteria
    {
        public UpdateOutrightCriteria(
            int searchAccountId,
            int actorId,
            int subAccountId,
            bool disableOutright,
            string language,
            string ip): base(searchAccountId, actorId, subAccountId, language, ip)
        {
            DisableOutright = disableOutright;
        }

        public bool DisableOutright { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdDisableOutright";
    }
}
