using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using tomi.arcade.game.gol.proto;

namespace tomi.arcade.server
{
    internal class GameOfLifeService : game.gol.proto.GameOfLifeService.GameOfLifeServiceBase
    {
        private readonly game.gol.proto.GameOfLifeService.GameOfLifeServiceClient _client;
        private readonly ILogger<GameOfLifeService> _logger;

        public GameOfLifeService(game.gol.proto.GameOfLifeService.GameOfLifeServiceClient client, ILogger<GameOfLifeService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public override async Task GetState(GameOfLifeRequest request, IServerStreamWriter<GameOfLifeResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Getting game state.");

            try
            {
                var response = _client.GetState(request, headers: context.RequestHeaders, cancellationToken: context.CancellationToken);
                while (await response.ResponseStream.MoveNext(context.CancellationToken))
                {
                    System.Console.WriteLine($"NextState: {response.ResponseStream.Current.GameState.Count}");

                    await responseStream.WriteAsync(response.ResponseStream.Current);
                }
            }
            catch (RpcException rpcException)
            {
                // cancelled, so...            
            }
            catch (OperationCanceledException)
            {
                // cancelled, so...
            }
        }
    }
}
