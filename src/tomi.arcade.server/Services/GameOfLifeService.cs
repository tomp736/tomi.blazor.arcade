using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using tomi.arcade.protos;

namespace tomi.arcade.server
{
    public class GameOfLifeService : protos.GameOfLifeService.GameOfLifeServiceBase
    {
        private readonly protos.GameOfLifeService.GameOfLifeServiceClient _client;
        private readonly ILogger<GameOfLifeService> _logger;

        public GameOfLifeService(protos.GameOfLifeService.GameOfLifeServiceClient client, ILogger<GameOfLifeService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public override async Task GetState(GameStateRequest request, IServerStreamWriter<GameStateResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Getting game state.");

            try
            {
                var response = _client.GetState(request, headers: context.RequestHeaders, cancellationToken: context.CancellationToken);
                while (await response.ResponseStream.MoveNext(context.CancellationToken))
                {
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
