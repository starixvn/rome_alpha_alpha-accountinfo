namespace Alpha.AccountInfo.Site.Helpers
{
    using System;
    using System.Web.Mvc;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Requests;

    public class QueryEntityModelBinder : DefaultModelBinder
    {
        private static readonly Type[] IUserType = { typeof(IUser) };
        private static readonly Type BaseRequestType = typeof(BaseRequest);

        protected override object CreateModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            Type modelType)
        {
            if (modelType != null
                && BaseRequestType.IsAssignableFrom(modelType)
                && modelType.GetConstructor(IUserType) != null)
            {
                return CreateInstanceOfAssignableBaseRequest(modelType);
            }

            return CallBaseCreateModel(controllerContext, bindingContext, modelType);
        }

        protected virtual object CreateInstanceOfAssignableBaseRequest(Type modelType)
            => Activator.CreateInstance(modelType, DependencyResolver.Current.GetService<IUser>());

        protected virtual object CallBaseCreateModel(
           ControllerContext controllerContext,
           ModelBindingContext bindingContext,
           Type modelType) => base.CreateModel(controllerContext, bindingContext, modelType);
    }
}