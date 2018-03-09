using Api.Providers;
using IOCConfig;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Api.Startup))]
namespace Api
{
    public class Startup
    {
        private Container _container;
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureCors(app);
            ConfigureRoutes(config);
            ConfigureIoC(config);
            ConfigureProfiles();
            ConfigureOAuth(app);
            app.UseWebApi(config);
        }

        private static void ConfigureCors(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                Provider = new OAuthAppProvider(_container)
            };

            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }

        public static void ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private void ConfigureIoC(HttpConfiguration config)
        {
            var lifestyle = new AsyncScopedLifestyle();

            _container = new SimpleInjector.Container();
            _container.Options.DefaultScopedLifestyle = lifestyle;
            _container.Options.DefaultLifestyle = lifestyle;
            _container.Bootstrap(lifestyle);
            _container.RegisterWebApiControllers(config);
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);
            _container.Verify();
        }

        public static void ConfigureProfiles()
        {
            AutoMapperConfig.Initialize.Bootstrap();
        }
    }
}
