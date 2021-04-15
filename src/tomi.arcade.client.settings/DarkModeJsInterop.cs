using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace tomi.arcade.client.settings
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class DarkModeJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public DarkModeJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/tomi.arcade.client.settings/darkmode-interop.js").AsTask());
        }

        public async Task SetLightMode()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setLightMode");
        }

        public async Task SetDarkMode()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setDarkMode");
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
