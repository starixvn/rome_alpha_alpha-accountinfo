namespace Alpha.AccountInfo.Tests.Services
{
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Alpha.AccountInfo.Models.ProductStatus;
    using Alpha.AccountInfo.Services.ProductStatus;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Services", "ProductProfileServiceImpl")]
    public class ProductProfileServiceTests
    {
        private readonly IDynamicRepository productProfileDataAccess;

        public ProductProfileServiceTests()
        {
            productProfileDataAccess = Substitute.For<IDynamicRepository>();
        }        
    }
}
