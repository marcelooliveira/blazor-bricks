using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using System;

namespace BlazorBricks
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(configure =>
            {
                // Add any custom services here
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }

    public static class OnKeyUp
    {
        public static Action<string> Action { get; set; }
        public static void Handler(string value)
        {
            Action?.Invoke(value);
        }
    }
}
