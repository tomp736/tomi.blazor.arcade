using Grpc.Core;
using System;
using System.Threading.Tasks;
using Xunit;

namespace tomi.arcade.server.tests
{
    public class ArcadeApplicationTests : IClassFixture<ArcadeApplicationFactory>
    {
        private ArcadeApplicationFactory _factory;

        public ArcadeApplicationTests(ArcadeApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact(Skip = "TODO")]
        public async Task GameOfLifeClient_GetState()
        {
            var client = _factory.CreateGrpcClient();

            var request = new game.gol.proto.GameOfLifeRequest()
            {
                GameId = Guid.NewGuid().ToString(),
                GameSettings = new game.gol.proto.GameOfLifeSettings()
                {
                    Width = 100,
                    Height = 100,
                    ChunkSize = 0
                }
            };

            var response = client.GetState(request);
            while (await response.ResponseStream.MoveNext())
            {
                var gamestate = response.ResponseStream.Current;
            }
        }
    }
}
