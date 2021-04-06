using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace tomi.arcade.game.gol.client
{
    public static class BuilderExtensions
    {
        public static void AddGameOfLifeServiceClient(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddGrpcClient<game.gol.proto.GameOfLifeService.GameOfLifeServiceClient>("gameoflife", (provider, options) =>
            {
                var config = provider.GetService<IConfiguration>();
                UriInfo settings = config.GetSection("arcadeServer").Get<UriInfo>();

                options.Address = settings.ToUri();
            })
            .ConfigureChannel((provider, options) =>
            {
                // configure channel to use grpc-web handler
                options.HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
            });
        }
    }
}
