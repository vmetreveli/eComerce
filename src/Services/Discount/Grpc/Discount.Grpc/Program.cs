using Discount.Grpc.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace Discount.Grpc;

public class Program
{
    public static void Main(string[] args) =>
        CreateHostBuilder(args)
            .Build()
            .MigrateDatabase<Program>()
            .Run();

    // Additional configuration is required to successfully run gRPC on macOS.
    // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
#if DEBUG


                webBuilder.ConfigureKestrel(options =>
                {
                    //  int port;
                    // var scope= options.ApplicationServices.CreateScope();
                    // var services=scope.ServiceProvider;
                    // var configuration = services.GetRequiredService<IConfiguration>();
                    // port = int.Parse(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
                    //For MacOs
                    //HTTP/2 over TLS is not supported on macOS due to missing ALPN support.
                    // Setup a HTTP/2 endpoint without TLS.
                    options.ListenLocalhost(3001, o => o.Protocols =
                        HttpProtocols.Http2);
                });
#endif
                webBuilder.UseStartup<Startup>();
            });
}