using Microsoft.AspNetCore.Builder;

namespace JavaScriptClient
{
    public class Startup
    {
        // middleware to serve static files from the wwwroot folder
        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}