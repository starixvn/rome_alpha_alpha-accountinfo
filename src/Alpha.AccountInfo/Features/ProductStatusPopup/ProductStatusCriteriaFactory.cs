namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Alpha.AccountInfo.Features.ProductStatusPopup;
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

                default:
                    return new SelectDCSProductStatusCriteria(productId, customerId, roleId);
            }
        }

        public NonQueryProductStatusCriteria CreateUpdateCriteria(ProductStatusCommandParam param)
        {
            switch (param.ProductId)
            {
                case ProductIdConstants.SabaVirtualSports:
                    return new UpdateProductStatusForSettingType4Criteria(param.CustId, param.SubAccId, param.ActorId, param.IP, BuildProductStatusXml(param.ProductStatusParams));

                case ProductIdConstants.Cashout:
                    return new UpdateProductStatusForSettingType4Criteria(param.CustId, param.SubAccId, param.ActorId, param.IP, BuildProductStatusXml(param.ProductStatusParams));

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
                BuildProductStatusXml(param.ProductStatusParams));

        private static NonQueryProductStatusCriteria CreateUpdateMacauGamesStatusCriteria(ProductStatusCommandParam param)
            => new UpdateMacauGamesStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.ActorId,
                param.IP,
                BuildProductStatusXml(param.ProductStatusParams));

        private static string BuildProductStatusXml(IEnumerable<BetTypeStatusParam> statusParams)
        {
            if (statusParams != null && statusParams.Any())
            {
                var builder = new StringBuilder();

                foreach (var item in statusParams)
                {
                    builder.Append(BuildProductStatusElement(item));
                }

                return $"<ROOT>{builder.ToString()}</ROOT>";
            }

            return string.Empty;
        }

        private static string BuildProductStatusElement(BetTypeStatusParam param)
            => $"<ProductStatus SportType=\"{param.SportType}\" BetType=\"{param.BetType}\" IsDisabled=\"{Convert.ToInt32(param.IsDisabled)}\"/>";
    }
}