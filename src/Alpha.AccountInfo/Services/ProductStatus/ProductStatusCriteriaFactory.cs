namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria.ProductStatus;
    using Fanex.Data.Repository;
    using Params.ProductStatus;

    public interface ProductStatusCriteriaFactory
    {
        ICriteria CreateSelectCriteria(int productId, int customerId, byte roleId);

        NonQueryProductStatusCriteria CreateUpdateCriteria(ProductStatusCommandParam param);

        IEnumerable<NonQueryProductStatusCriteria> CreateUpdateKenoStatusCriteria(ProductStatusCommandParam param);

        UpdateCasinoStatusCriteria CreateUpdateCasinoStatusCriteria(ProductStatusCommandParam param);
    }

    public class ProductStatusCriteriaFactoryImpl : ProductStatusCriteriaFactory
    {
        public ICriteria CreateSelectCriteria(int productId, int customerId, byte roleId)
        {
            switch (productId)
            {
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

        public NonQueryProductStatusCriteria CreateUpdateCriteria(ProductStatusCommandParam param)
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

        public IEnumerable<NonQueryProductStatusCriteria> CreateUpdateKenoStatusCriteria(ProductStatusCommandParam param)
        {
            foreach (var item in param.ProductStatusParams)
            {
                yield return new UpdateKenoStatusCriteria(
                    param.CustId,
                    param.SubAccId,
                    param.ActorId,
                    param.IP,
                    param.LangKey,
                    item.BetType,
                    item.IsDisabled,
                    param.UplineId);
            }
        }

        public UpdateCasinoStatusCriteria CreateUpdateCasinoStatusCriteria(ProductStatusCommandParam param)
        {
            var disable = param.ProductStatusParams.First().IsDisabled;

            return new UpdateCasinoStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.ActorId,
                param.ActorName,
                param.IP,
                param.LangKey,
                disable);
        }

        private static NonQueryProductStatusCriteria CreateUpdateColossusBetStatusCriteria(ProductStatusCommandParam param)
        {
            var disable = param.ProductStatusParams.First().IsDisabled;

            return new UpdateColossusBetStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.ActorId,
                param.IP,
                param.LangKey,
                disable);
        }

        private static NonQueryProductStatusCriteria CreateUpdateNG2StatusCriteria(ProductStatusCommandParam param)
            => new UpdateNG2StatusCriteria(
                param.CustId,
                param.SubAccId,
                param.ActorId,
                param.IP,
                param.ProductStatusParams);

        private static NonQueryProductStatusCriteria CreateUpdateDCSProductStatusCriteria(ProductStatusCommandParam param)
            => new UpdateDCSProductStatusCriteria(
                param.CustId,
                param.SubAccId,
                param.ActorId,
                param.IP,
                param.ProductStatusParams);
    }
}
