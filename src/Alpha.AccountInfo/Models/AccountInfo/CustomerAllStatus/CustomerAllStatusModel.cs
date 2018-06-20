namespace Alpha.AccountInfo.Models.AccountInfo
{
    using System.Collections.Generic;

    public class CustomerAllStatusModel
    {
        public CustomerAllStatusModel(
            CustomerStatusModel customerStatus,
            NonDCSProductModel nonDCSProduct,
            IEnumerable<DCSProductModel> dcsProducts)
        {
            CustomerStatus = customerStatus;
            NonDCSProduct = nonDCSProduct;
            DCSProducts = dcsProducts;
        }

        public CustomerStatusModel CustomerStatus { get; }

        public NonDCSProductModel NonDCSProduct { get; }

        public IEnumerable<DCSProductModel> DCSProducts { get; }
    }
}
