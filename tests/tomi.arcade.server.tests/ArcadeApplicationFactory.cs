using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace tomi.arcade.server.tests
{
    public class ArcadeApplicationFactory : WebApplicationFactory<Startup>
    {
        public game.gol.proto.GameOfLifeService.GameOfLifeServiceClient CreateGrpcClient()
        {
            var channel = this.CreateGrpcChannel();
            return new game.gol.proto.GameOfLifeService.GameOfLifeServiceClient(channel);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {            
            base.ConfigureWebHost(builder);
        }
    }
}
