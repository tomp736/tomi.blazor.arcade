using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using tomi.arcade.protos;

namespace tomi.arcade.game.client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.BuildGrpcWebHandler();

            await builder.Build().RunAsync();
        }
    }

    public static class BuilderExtensions
    {
        public static void BuildGrpcWebHandler(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddGrpcClient<protos.GameOfLifeService.GameOfLifeServiceClient>("gameoflife", (provider, options) =>
            {
                // var arcadeServerHttp = Environment.GetEnvironmentVariable("ARCADE_SERVER_HTTP");
                var navManager = provider.GetRequiredService<NavigationManager>();
                options.Address = new Uri($"{navManager.BaseUri}:30010");
            })
            .ConfigureChannel((provider, options) =>
            {
                // configure channel to use grpc-web handler
                options.HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
            });
        }
    }
}
