namespace Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers
{
    using System;
    using System.Web.Mvc;
    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.Requests;
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using Alpha.AccountInfo.Site.Features.Shared.Models;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class QueryEntityModelBinderTests : QueryEntityModelBinder
    {
        protected override object CreateInstanceOfAssignableBaseRequest(Type modelType)
        {
            return "CreateInstanceOfAssignableBaseRequest";
        }

        protected override object CallBaseCreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return "CallBaseCreateModel";
        }

        [Fact]
        public void CreateModel_ConstructorParamIsAssignableFromBaseRequest_CallCreateInstanceOfAssignableBaseRequest()
        {
            // Arrange
            var productStatusQueryType = typeof(ProductStatusQuery);

            // Act
            var createdInstance = CreateModel(null, null, productStatusQueryType);

            // Assert
            Assert.Equal("CreateInstanceOfAssignableBaseRequest", createdInstance);
        }

        [Fact]
        public void CreateModel_ConstructorParamIsNotAssignableFromBaseRequest_CallBaseCreateModel()
        {
            // Arrange
            var userType = typeof(User);

            // Act
            var createdInstance = CreateModel(null, null, userType);

            // Assert
            Assert.Equal("CallBaseCreateModel", createdInstance);
        }
    }
}