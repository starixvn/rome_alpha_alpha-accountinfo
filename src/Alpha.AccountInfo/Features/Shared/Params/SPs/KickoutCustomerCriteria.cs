namespace Alpha.AccountInfo.Features.Shared.Params.SPs
{
    using Fanex.Data.Repository;

    public class KickoutCustomerCriteria : NonQueryCommand
    {
        public KickoutCustomerCriteria(KickoutCustomerParam param)
        {
            CustId = param.CustId;
            RoleId = param.CustLevelId;
            ServerId = param.ServerId;
            IP = param.IP;
            SubAccId = param.SubAccId;
            Mode = param.Mode;
        }

        public int CustId { get; }

        public byte RoleId { get; }

        public int ServerId { get; }

        public string IP { get; }

        public int SubAccId { get; }

        public string Mode { get; }

        public override string GetSettingKey()
            => "Age_OnUser_ForceKickedout";

        public override bool IsValid()
            => CustId > 0 && RoleId > 0 && ServerId > 0;
    }
}
