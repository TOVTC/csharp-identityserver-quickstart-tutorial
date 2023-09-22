using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;

namespace MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            // the following implements the authorization code flow with PKCE to the OpenID Connect provider

            // adds authentication services to the DI (dependency injection)
            services.AddAuthentication(options =>
            {
                // set cookies as the default scheme and use them to sign in
                options.DefaultScheme = "Cookies";
                // use the OpenId Connect protocol when we need a user to login
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            // configures the handler that performs the OpenID Connect protocol
            .AddOpenIdConnect("oidc", options =>
            {
                // indicates where the trusted token service is located
                options.Authority = "https://localhost:5001";

                // identify the client via the ClientId and the ClientSecret
                options.ClientId = "mvc";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                // this persists tokens from the IdentityServer int the cookie
                options.SaveTokens = true;

                options.Scope.Add("api1");
                options.Scope.Add("offline_access");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // make sure authentication is executed on each request
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute()
                // this disables anonymous access for the entire application
                // the [Authorize] attribute can be used to specify on a per controller basis
                    .RequireAuthorization();
            });
        }
    }
}