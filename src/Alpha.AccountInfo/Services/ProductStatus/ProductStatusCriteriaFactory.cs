namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System.Collections.Generic;
    using Constants;
    using Criteria.ProductStatus;
    using Fanex.Data.Repository;
    using Params.ProductStatus;

    public interface ProductStatusCriteriaFactory
    {
        ICriteria CreateSelectCriteria(int productId, int customerId, byte roleId);

        NonQueryProductStatusCriteria CreateUpdateCriteria(UpdateProductStatusParam param);

        IEnumerable<NonQueryProductStatusCriteria> CreateUpdateKenoStatusCriteria(UpdateProductStatusParam param);
    }

    public class ProductStatusCriteriaFactoryImpl : ProductStatusCriteriaFactory
    {
        public ICriteria CreateSelectCriteria(int productId, int customerId, byte roleId)
        {
            switch (productId)
            {
                case ProductIdConstants.Racing:
                case ProductIdConstants.RNGCasino:
                case ProductIdConstants.Keno:
                case ProductIdConstants.ColossusBets:
                    return new SelectProductStatusCriteria(productId, customerId);

                case ProductIdConstants.NG2:
                    return new SelectNG2StatusCriteria(productId, customerId);

                default:
                    return new SelectDCSProductStatusCriteria(productId, customerId, roleId);
            }
        }

        public NonQueryProductStatusCriteria CreateUpdateCriteria(UpdateProductStatusParam param)
        {
            switch (param.ProductId)
            {
                case ProductIdConstants.RNGCasino:
                    return CreateUpdateCasinoStatusCriteria(param);

                case ProductIdConstants.ColossusBets:
                    return CreateUpdateColossusBetStatusCriteria(param);

                case ProductIdConstants.NG2:
                    return CreateUpdateNG2StatusCriteria(param);

                default:
                    return CreateUpdateDCSProductStatusCriteria(param);
            }
        }

        public IEnumerable<NonQueryProductStatusCriteria> CreateUpdateKenoStatusCriteria(UpdateProductStatusParam param)
        {
            foreach (var item in param.ProductStatusParams)
            {
                yield return new UpdateKenoStatusCriteria(
                    param.CustId,
                    param.SubAccId,
                    param.UserId,
                    param.IP,
                    param.LangKey,
                    item.BetType,
                    item.IsDisabled,
                    param.UplineId);
            }
        }

        private static NonQueryProductStatusCriteria CreateUpdateCasinoStatusCriteria(UpdateProductStatusParam param)
        {
            var disable = param.ProductStatusParams[0].IsDisabled;

            return new UpdateCasinoStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.UserId,
                param.IP,
                param.LangKey,
                disable);
        }

        private static NonQueryProductStatusCriteria CreateUpdateColossusBetStatusCriteria(UpdateProductStatusParam param)
        {
            var disable = param.ProductStatusParams[0].IsDisabled;

            return new UpdateColossusBetStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.UserId,
                param.IP,
                param.LangKey,
                disable);
        }

        private static NonQueryProductStatusCriteria CreateUpdateNG2StatusCriteria(UpdateProductStatusParam param)
        {
            return new UpdateNG2StatusCriteria(
                param.CustId,
                param.SubAccId,
                param.UserId,
                param.IP,
                param.ProductStatusParams);
        }

        private static NonQueryProductStatusCriteria CreateUpdateDCSProductStatusCriteria(UpdateProductStatusParam param)
        {
            return new UpdateDCSProductStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.UserId,
                param.IP,
                param.ProductStatusParams);
        }
    }
}
