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

        ConcurrentDictionary<System.Guid, GameOfLife> games = new ConcurrentDictionary<System.Guid, GameOfLife>();

        public override async Task<SetGameStateResponse> SetState(SetGameStateRequest request, ServerCallContext context)
        {
            var cancellationToken = context.CancellationToken;

            System.Guid gameId = System.Guid.Parse(request.GameId.Value);
            GameOfLife _gameOfLife = games.GetOrAdd(gameId, (gameId) => new GameOfLife(gameId, request.GameMap.X, request.GameMap.Y));

            _gameOfLife.SetState(request.GameState.ToArray());

            return new SetGameStateResponse();
        }

        public override async Task GetState(GameStateRequest request, IServerStreamWriter<GameStateResponse> responseStream, ServerCallContext context)
        {
            var cancellationToken = context.CancellationToken;

            System.Guid gameId = System.Guid.Parse(request.GameId.Value);
            GameOfLife _gameOfLife = games.GetOrAdd(gameId, (gameId) => new GameOfLife(gameId, request.GameMap.X, request.GameMap.Y));
            GameMap _gameMap = new GameMap()
            {
                X = request.GameMap.X,
                Y = request.GameMap.Y
            };

            Dictionary<int, bool> lastGeneration = null;
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _gameOfLife.SpawnNextGeneration();
                    Dictionary<int, bool> thisGeneration = ToDictionary(_gameOfLife.CurrentGeneration);

                    GameStateResponse gameStateResponse = new GameStateResponse();
                    gameStateResponse.GameMap = _gameMap;
                    if (lastGeneration == null)
                    {
                        for (int i = 0; i < thisGeneration.Count; i++)
                        {
                            var thisCell = thisGeneration[i];
                            if (thisCell)
                            {
                                gameStateResponse.GameState.Add(thisCell ? i : -i);
                            }
                        }
                        lastGeneration = thisGeneration;
                    }
                    else
                    {
                        for (int i = 0; i < thisGeneration.Count; i++)
                        {
                            var lastCell = lastGeneration[i];
                            var thisCell = thisGeneration[i];
                            if (thisCell != lastCell)
                            {
                                gameStateResponse.GameState.Add(thisCell ? i : -i);
                            }
                        }
                        lastGeneration = thisGeneration;
                    }
                    await responseStream.WriteAsync(gameStateResponse);
                    //System.Threading.Thread.Sleep(50);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Subscriber disconnected.");
                    break;
                }
            }
            await base.GetState(request, responseStream, context);
        }

        static Dictionary<int, bool> ToDictionary(bool[,] input)
        {
            Dictionary<int, bool> result = new Dictionary<int, bool>();
            int write = 0;
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (int z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }
            return result;
        }
    }
}
