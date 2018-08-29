namespace Alpha.AccountInfo.Site
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Site.Integration;
    using Alpha.Auth.Filters;
    using Alpha.Products.AccountInfo;
    using Alpha.Products.Core.Configuration;
    using Alpha.Products.Core.Services;
    using Alpha.RemotingRef;
    using App_Start;
    using CasinoLibrary.Model.UserException;
    using Configurations;
    using Fanex.Data;
    using Fanex.Data.CrossCutting.WrappingException;
    using Fanex.Data.OldConnectionStringProvider;
    using Fanex.Logging.Extensions.Database;
    using Fanex.Logging.Extensions.Web;
    using Fanex.RemotingRef;
    using Helpers;
    using ONEbook.UM.Filters;
    using Sunplus.Agent.Model.Entities;
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
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyConfig.Init();

            DbSettingProviderManager
                .StartNewSession()
                .Use(new OldConnectionStringProvider())
                .UseDefaultDbSettingProvider(resourcePath: AppSettings.StoredProcedurePath, enableWatching: true)
                .Use((settingKey, commandTimeout) => new WrappingExceptionObjectDb(settingKey, commandTimeout))
                .Run();

            ShareSession.Init();

            GlobalFilters.Filters.Add(new CheckUMFilter());
            //GlobalFilters.Filters.Add(new CheckBelongingFilter());
            GlobalFilters.Filters.Add(new NoCacheFilter());

            AppSettings.Verify();

            StartConfiguration();

            ModelBinders.Binders.DefaultBinder = new QueryEntityModelBinder();

            StaticResources.Init(GetBuildVersion());

            var loggingConfiguration = new LoggingConfiguration().UseGetDbExceptionInfo(DatabaseInfoGetters.GetDbExceptionInfo);

            ExceptionHandler.UseConfig(loggingConfiguration);

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
                exception.GetType() == typeof(ErrorException) ? TraceEventType.Information : TraceEventType.Error);
        }

        private static string GetBuildVersion()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return versionInfo.FileVersion;
        }

        private static void StartConfiguration()
        {
            SimpleServiceLocator
            .Instance
            .Register<IConfigurationLoader>(new SearchAccountConfigurationLoader(new ProductJsonConverter()))
            .Register<Func<int, string>>(CacheRef.GetProductName)
            .Register<Func<string, string>>(Resources.ResourceManager.GetString)
            .Register<Func<string, string, string>>(MicroSites.GetMicroSiteUrl);
        }
    }
}