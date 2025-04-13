using System;
using System.Collections.Generic;
using Windows.Infrastructure;
using Sample.Windows;
using Sample.Windows.confirmation;

namespace Sample.Implementation
{
    public class WindowPathProvider : IPathWindowProvider
    {
        private readonly Dictionary<Type, string> _map = new()
        {
            { typeof(PreviewPuzzleWindow), "windows/PreviewPuzzleWindow" },
            { typeof(ConfirmationWindowView), "windows/ConfirmationWindow" }
        };

        public string Get<T>() where T : IWindow
        {
            if (!_map.TryGetValue(typeof(T), out var path))
            {
                throw new KeyNotFoundException($"Path not found for window type: {typeof(T).Name}");
            }

            return path;
        }
    }
}