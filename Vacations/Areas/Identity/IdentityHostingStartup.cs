using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Vacations.Areas.Identity.IdentityHostingStartup))]

namespace Vacations.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((_, _) =>
            {
            });
        }
    }
}