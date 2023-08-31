namespace Alpha.AccountInfo.Site.App_Start
{
    using Alpha.AccountInfo.DataAccess;
    using Alpha.AccountInfo.Features.OpenDownlines;
    using Alpha.AccountInfo.Features.ProtectedStatus;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Features.Shared.Helpers;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.ProfilePicture.Services;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Features.ProductStatusPopup;
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using Alpha.AccountInfo.Site.Features.Shared.Models;
    using Alpha.Products.Core.Services;
    using Alpha.SecurityCode.Services;
    using Alpha.Web.Helpers;
    using AlphaPlus.User.Models;
    using AlphaPlus.Utilities.IP;
    using Fanex.Data.Repository;
    using Fanex.Logging;
    using Fanex.RemotingRef;
    using MediatR;
    using Services.ProductStatus;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Resources;
    using System.Web.Mvc;

    public static class DependencyConfig
    {
        public static void Init()
        {
            RegisterProductConfiguration();

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

            StartConfiguration(container);
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
#pragma warning disable CS0618 // Type or member is obsolete
            container.Register<IUserSession, UserSessionExt>(Lifestyle.Singleton);
#pragma warning restore CS0618 // Type or member is obsolete
            container.Register<IUser, User>(Lifestyle.Singleton);
#pragma warning disable CS0618 // Type or member is obsolete
            container.RegisterSingleton<SecurityCodeChecker, SecurityCodeCheckerImpl>();
#pragma warning restore CS0618 // Type or member is obsolete
            container.RegisterSingleton<IRemotingRefService, RemotingRefService>();
            container.RegisterSingleton<CacheRefService, CacheRefServiceImpl>();
            container.RegisterSingleton<IPService, IPServiceImpl>();
            container.RegisterSingleton(Logger.Log);
            container.RegisterSingleton<ExceptionHandlerWrapper, ExceptionHandlerWrapperImpl>();

            container.Register<ProductStatusAuthorizationService>(
                () => new ProductStatusAuthorizationServiceImpl(
                    container.GetInstance<IDynamicRepository>(),
#pragma warning disable CS0618 // Type or member is obsolete
                    UserPermission.HasPermissionOnProduct),
#pragma warning restore CS0618 // Type or member is obsolete
                Lifestyle.Scoped);

            container.Register<ProductStatusCriteriaFactory, ProductStatusCriteriaFactoryImpl>(Lifestyle.Singleton);
            container.Register<ProductStatusService, ProductStatusServiceImpl>(Lifestyle.Scoped);
            container.Register<SearchAccountService, SearchAccountServiceImpl>(Lifestyle.Scoped);
            container.Register<SearchAccountDataAccess, SearchAccountDataAccessImpl>(Lifestyle.Scoped);
            container.Register<UnsuspendDownlinesService, UnsuspendDownlinesServiceImpl>(Lifestyle.Scoped);
            container.Register<UnsuspendDownlinesDataAccess, UnsuspendDownlinesDataAccessImpl>(Lifestyle.Scoped);
            container.Register<OpenDownlinesService, OpenDownlinesServiceImpl>(Lifestyle.Scoped);
            container.Register<OpenDownlinesDataAccess, OpenDownlinesDataAccessImpl>(Lifestyle.Scoped);

            container.Register<ProfilePictureServices, ProfilePictureServicesImpl>(Lifestyle.Scoped);
            container.Register<ProtectedStatusService, ProtectedStatusServiceImpl>(Lifestyle.Scoped);
            container.Register<KickoutCustomerService, KickoutCustomerServiceImpl>(Lifestyle.Scoped);
        }

        private static void StartConfiguration(Container container)
        {
            SimpleServiceLocator
              .Instance
              .Register<Func<int, string>>(new CacheRefServiceImpl(container.GetInstance<IRemotingRefService>()).GetProductName)
              .Register<Func<string, string>>(Resources.ResourceManager.GetString)
              .Register<Func<string, string, string>>(MicroSites.GetMicroSiteUrl)
              .Register<Func<string, ResourceManager, string>>(ResourcesJsonConverter.ToConvert);
        }

        private static void RegisterProductConfiguration()
        {
            SimpleServiceLocator
              .Instance
              .Register<Func<int>>(AppSettingsHelper.GetInsertProfilePictureSiteMode)
              .Register<Func<bool>>(AppSettingsHelper.GetUpdateCreditReplicateServer)
              .Register<Func<string, bool>>(AppSettingsHelper.HideProductStatusBySites);
        }
    }
}