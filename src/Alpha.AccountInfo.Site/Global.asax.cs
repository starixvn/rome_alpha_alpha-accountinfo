namespace Alpha.AccountInfo.Site
{
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using Alpha.AccountInfo.Site.Utilities;
    using Alpha.Auth.Filters;
    using AlphaPlus.Logging;
    using AlphaPlus.Logging.Web;
    using AlphaPlus.User.Models;
    using App_Start;
    using Configurations;
    using Fanex.Caching;
    using Fanex.Data;
    using Fanex.Data.SqlServer;
    using Fanex.Logging.Extensions.Web;
    using Fanex.Logging.Extensions.Database;
    using Fanex.RemotingRef;
    using ONEbook.UM;
    using ONEbook.UM.AspNetMVC.Filters;
    using ONEbook.UM.Configurations;
    using ONEbook.UM.NETFramework;
    using ONEbook.UM.Services;
    using ONEbook.UM.Transport;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Web;
    using Web.Filters;
    using Web.Helpers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AlphaPlus.Configurations.ConfigurationManager.Init(new ConfigurationHelper());
            UserSession.Init(new Web.Helpers.HttpContextHelper());
            LoggingConfig.ExceptionHandler = new ExceptionHandlerAdapter();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            LoggingConfig.Init(getDbExceptionInfo: DatabaseInfoGetters.GetDbExceptionInfo);
            DependencyConfig.Init();

            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer(resourcePath: AppSettings.StoredProcedurePath)
                .UseShardDbSettingProvider()                
                .Run();

            ShareSession.Init();

            var umConfig = new UMClientConfiguration().Initialize();
            UMClientManager
                .UseConfig(umConfig)
                .UseCacheService(new UMCacheService(new CacheService()))
                .UseRestClient(new UMRestClient(umConfig))
                .UseHttpContext(new ONEbook.UM.Helpers.HttpContextHelper());
            GlobalFilters.Filters.Add(new CheckUMFilterAttribute(
                new MaintenanceService(UMClientManager.RestClient, UMClientManager.CacheService, UMClientManager.UMClientConfigs, UMClientManager.HttpContext), null));

            GlobalFilters.Filters.Add(new CheckBelongingFilter());
            GlobalFilters.Filters.Add(new NoCacheFilter());
            GlobalFilters.Filters.Add(new AfterLoginFlowFilter());
            GlobalFilters.Filters.Add(new ProtectedModeFilter());

            AppSettings.Verify();

            ModelBinders.Binders.DefaultBinder = new QueryEntityModelBinder();

            StaticResources.Init(GetBuildVersion());

            RefDataTypeContainer
                .ConnectTo(new Uri(AppSettings.RemoteRefURL))
                .FromApplication("Alpha.AccountInfo.Site");
        }

        protected void Application_Error()
        {
            var currentCustomer = UserSession.Current;

            Exception exception = Server.GetLastError().GetBaseException();

            ExceptionHandler.Handle(
                exception,
                new Dictionary<string, object>
                {
                    ["CustId"] = currentCustomer?.CustId,
                    ["CustName"] = currentCustomer?.UserName,
                    ["SubAccountName"] = currentCustomer?.SubUserName,
                    ["AdminName"] = currentCustomer?.AdminName
                },
                TraceEventType.Error);
        }

        private static string GetBuildVersion()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return $"{versionInfo.FileVersion}.{AppSettings.JsVersion}";
        }
    }
}