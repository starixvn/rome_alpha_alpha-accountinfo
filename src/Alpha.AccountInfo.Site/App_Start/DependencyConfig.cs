namespace Alpha.AccountInfo.Site.App_Start
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Controllers;
    using Fanex.Data.Repository;
    using Fanex.RemotingRef;
    using MediatR;
    using Models;
    using Services;
    using Services.ProductStatus;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Sunplus.Agent.Model.Base;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Mvc;

    public static class DependencyConfig
    {
        public static void Init()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            RegisterMediator(container);

            RegisterDataAccesses(container);

            RegisterServices(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            RegisterStaticInstances(container);
        }

        private static void RegisterMediator(Container container)
        {
            var assemblies = GetAssemblies();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);
            container.Register(typeof(ICancellableAsyncRequestHandler<>), assemblies);
            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(ICancellableAsyncNotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IPipelineBehavior<,>), assemblies);

            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(ProductStatusController).Assembly;
        }

        private static void RegisterDataAccesses(Container container)
            => container.Register<IDynamicRepository, DynamicRepository>(Lifestyle.Singleton);

        private static void RegisterServices(Container container)
        {
            container.Register<IUserSession, UserSessionExt>(Lifestyle.Singleton);
            container.Register<IUser, User>(Lifestyle.Singleton);
            container.RegisterSingleton<IRemotingRefService, RemotingRefService>();
            container.RegisterSingleton<CacheRefService, CacheRefServiceImpl>();

            container.Register<ProductStatusAuthorizationService>(
                () => new ProductStatusAuthorizationServiceImpl(
                    container.GetInstance<IDynamicRepository>(),
                    UserPermission.HasPermissionOnProduct),
                Lifestyle.Scoped);

            container.Register<ProductStatusCriteriaFactory, ProductStatusCriteriaFactoryImpl>(Lifestyle.Singleton);
            container.Register<IProductProfileService, ProductProfileService>(Lifestyle.Singleton);
            container.Register<ProductStatusService, ProductStatusServiceImpl>(Lifestyle.Scoped);
            container.Register<RnGCasinoService, RnGCasinoServiceImpl>(Lifestyle.Scoped);
            container.Register<AccountInfoService, AccountInfoServiceImpl>(Lifestyle.Scoped);
            container.Register<AccountInfoDataAccess, AccountInfoDataAccessImpl>(Lifestyle.Scoped);
        }

        private static void RegisterStaticInstances(Container container)
        {
        }
    }
}