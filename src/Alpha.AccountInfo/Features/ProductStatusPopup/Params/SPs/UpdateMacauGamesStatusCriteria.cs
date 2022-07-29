namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using System.Collections.Generic;
    using Params.ProductStatus;

    public class UpdateMacauGamesStatusCriteria : UpdateDCSProductStatusCriteria
    {
        private const int MacauGamesStatusSettingTypeId = 21;

        public short SettingType => MacauGamesStatusSettingTypeId;

        public UpdateMacauGamesStatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            IEnumerable<BetTypeStatusParam> productStatusParams) : base(custId, subAccId, userId, ip, productStatusParams)
        {
        }
    }
}
