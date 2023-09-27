

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // this validates the incoming token to make sure it is coming from a trused user and validates taht the token is valid to be used with this api (aka audience)

            // adds the authentication services to DI (Dependency Injection) and configures Bearer as the default scheme
            // accepts any access token issued by the identity server
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            // checks for the presence of the scope in the acecss token (as opposed to any access token issued by the identity server)
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            // adds the authentication middleware to the pipeline so authentication will be performed automatically on every call into the host
            app.UseAuthentication();
            // adds the authorization middleware to make sure our API endpoint cannot be acecssed by anonymous clients
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // setup the policy for all API endpoints using the routing system
                endpoints.MapControllers()
                .RequireAuthorization("ApiScope");
            });
        }
    }
}
