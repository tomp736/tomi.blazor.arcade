using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tomi.arcade.protos;

namespace tomi.arcade.game.gol.server
{
    public class GameOfLifeService : protos.GameOfLifeService.GameOfLifeServiceBase
    {
        private readonly ILogger<GameOfLifeService> _logger;
        public GameOfLifeService(ILogger<GameOfLifeService> logger)
        {
            _logger = logger;
        }

        public override async Task GetState(GameStateRequest request, IServerStreamWriter<GameStateResponse> responseStream, ServerCallContext context)
        {
            var cancellationToken = context.CancellationToken;

            Guid gameId = System.Guid.Parse(request.GameId);
            GameOfLife _gameOfLife = new GameOfLife(gameId, request.GameSettings.Width, request.GameSettings.Height);

            await WriteNextGameState(request, responseStream, request.GameSettings.ChunkSize, _gameOfLife.SeedGame(5));

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await WriteNextGameState(request, responseStream, request.GameSettings.ChunkSize, _gameOfLife.SpawnNextGeneration());
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Subscriber disconnected.");
                    break;
                }
            }
        }

        private static async Task WriteNextGameState(GameStateRequest request, IServerStreamWriter<GameStateResponse> responseStream, int chunkSize, IEnumerable<int> nextState)
        {
            GameStateResponse gameStateResponse;
            if (chunkSize == 0)
            {
                gameStateResponse = new GameStateResponse();
                gameStateResponse.GameState.AddRange(nextState);
                await responseStream.WriteAsync(gameStateResponse);
            }
            else
            {
                for (int i = 0; i < nextState.Count(); i += chunkSize)
                {
                    gameStateResponse = new GameStateResponse();
                    gameStateResponse.GameState.AddRange(nextState.Skip(i).Take(chunkSize));
                    await responseStream.WriteAsync(gameStateResponse);
                }
            }
        }
    }
}
