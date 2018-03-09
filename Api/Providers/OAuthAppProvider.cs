using Microsoft.Owin.Security.OAuth;
using ProjetoContext.Application.Usuario;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Api.Providers
{
    public class OAuthAppProvider
        : OAuthAuthorizationServerProvider
    {
        Container _container;
        public OAuthAppProvider(Container container)
        {
            _container = container;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var header = context.OwinContext.Response.Headers.SingleOrDefault(h => h.Key == "Access-Control-Allow-Origin");

            if (header.Equals(default(KeyValuePair<string, string[]>)))
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                UsuarioDto usuario;

                using (AsyncScopedLifestyle.BeginScope(_container))
                {
                    UsuarioService usuarioService = _container.GetInstance<UsuarioService>();
                    usuario = usuarioService.GetBy(context.UserName, context.Password);
                }

                if (usuario != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("id", usuario.Id.ToString()));
                    identity.AddClaim(new Claim("login", usuario.Login));

                    return Task.FromResult(context.Validated(identity));
                }
            }
            catch (ApplicationException ex)
            {
                context.SetError("invalid_grant", ex.Message);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var claim in context.Identity.Claims.Where(claim => claim.Type != ClaimTypes.Role))
                context.AdditionalResponseParameters.Add(claim.Type, claim.Value);

            return Task.FromResult<object>(null);
        }
    }
}