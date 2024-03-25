namespace Alpha.AccountInfo.Features.ProductStatusPopup.Params.SPs
{
    using Alpha.AccountInfo.Configurations;
    using Fanex.Data.Repository;

    public class CheckProductStatusUMCriteria : CriteriaBase
    {
        public CheckProductStatusUMCriteria(int custId, int? productId = null, byte functionType = 1, int? groupId = null)
            : this(custId.ToString(), productId, functionType, groupId)
        {
        }

        protected CheckProductStatusUMCriteria(string custIds, int? productId = null, byte functionType = 1, int? groupId = null)
        {
            ProductId = productId;
            CustIds = custIds;
            FunctionType = functionType;
            GroupId = groupId;
        }

        public int? ProductId { get; }

        public string CustIds { get; }

        public byte FunctionType { get; }

        public int? GroupId { get; }

        public byte SettingStatus => AppSettings.SettingStatus;

        public override string GetSettingKey()
            => "Acc_CheckUM_MultipleCustProductStatus";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(CustIds);
    }
}