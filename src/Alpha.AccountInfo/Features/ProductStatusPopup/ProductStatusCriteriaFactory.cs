namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.AccountInfo.Features.ProductStatusPopup;
    using Alpha.Products.Core.Shared;
    using Criteria.ProductStatus;
    using Fanex.Data.Repository;
    using Params.ProductStatus;

    public interface ProductStatusCriteriaFactory
    {
        ICriteria CreateSelectCriteria(int productId, int customerId, byte roleId);

        NonQueryProductStatusCriteria CreateUpdateCriteria(ProductStatusCommandParam param);
    }

    public class ProductStatusCriteriaFactoryImpl : ProductStatusCriteriaFactory
    {
        public ICriteria CreateSelectCriteria(int productId, int customerId, byte roleId)
        {
            switch (productId)
            {
                case ProductIdConstants.SabaVirtualSports:
                    return new SelectDCSProductStatusFor4Criteria(productId, customerId);

                case ProductIdConstants.Cashout:
                    return new SelectCashoutStatusCriteria(productId, customerId, roleId);
                case ProductIdConstants.RNGSlot:
                    return new SelectRNGSlotStatusCriteria(productId, customerId, roleId);
                default:
                    return new SelectDCSProductStatusCriteria(productId, customerId, roleId);
            }
        }

        public NonQueryProductStatusCriteria CreateUpdateCriteria(ProductStatusCommandParam param)
        {
            switch (param.ProductId)
            {
                case ProductIdConstants.SabaVirtualSports:
                    return new UpdateProductStatusForSettingType4Criteria(param.CustId, param.SubAccId, param.ActorId, param.IP, param.ProductStatusParams);

                case ProductIdConstants.Cashout:
                    return new UpdateProductStatusForSettingType4Criteria(param.CustId, param.SubAccId, param.ActorId, param.IP, param.ProductStatusParams);
                case ProductIdConstants.RNGSlot:
                    return CreateUpdateMacauGamesStatusCriteria(param);
                default:
                    return CreateUpdateDCSProductStatusCriteria(param);
            }
        }

        private static NonQueryProductStatusCriteria CreateUpdateDCSProductStatusCriteria(ProductStatusCommandParam param)
            => new UpdateDCSProductStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.ActorId,
                param.IP,
                param.ProductStatusParams);

        private static NonQueryProductStatusCriteria CreateUpdateMacauGamesStatusCriteria(ProductStatusCommandParam param)
            => new UpdateMacauGamesStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.ActorId,
                param.IP,
                param.ProductStatusParams);

    }
}
