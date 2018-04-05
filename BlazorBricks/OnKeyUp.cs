using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBricks
{
    public static class OnKeyUp
    {
        public static Action<string> Action { get; set; }
        public static void Handler(string value)
        {
            Action?.Invoke(value);
        }
    }
}
