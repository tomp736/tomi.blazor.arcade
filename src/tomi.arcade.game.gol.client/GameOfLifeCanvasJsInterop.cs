using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace tomi.arcade.game.gol.client
{
    public class GameOfLifeCanvasJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public GameOfLifeCanvasJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/tomi.arcade.game.gol.client/js/gameoflife.js").AsTask());
        }

        public async Task InitCanvas(string canvasId, int width, int height)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("GameOfLife.initCanvas", "#gameCanvas", width, height);
        }

        public async Task SetGameState(IEnumerable<int> gameState, string liveColor)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("GameOfLife.setGameState", gameState, liveColor);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
