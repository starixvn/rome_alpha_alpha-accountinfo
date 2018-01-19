namespace Alpha.AccountInfo.Services.ProductStatus
{
    using Constants;
    using Criteria.ProductStatus;
    using Fanex.Data.Repository;

    public interface ProductStatusCriteriaFactory
    {
        ICriteria CreateSelectCriteria(int productId, int customerId, byte roleId);
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
                    return new SelectNG2ProductStatusCriteria(productId, customerId);

                default:
                    return new SelectDCSProductStatusCriteria(productId, customerId, roleId);
            }
        }
    }
}
