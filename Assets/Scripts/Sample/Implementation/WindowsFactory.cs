using Windows.Infrastructure;
using Unity.VisualScripting;
using UnityEngine;

namespace Sample.Implementation
{
    public class WindowsFactory : IWindowFactory
    {
        private readonly IPathWindowProvider _pathWindowProvider;

        public WindowsFactory(IPathWindowProvider pathWindowProvider)
        {
            _pathWindowProvider = pathWindowProvider;
        }

        public IWindow Create<T>() where T : IWindow
        {
            var path = _pathWindowProvider.Get<T>();
            var prefab = Resources.Load(path);

            var instance = Object.Instantiate(prefab);
            var window = instance.GetComponent<T>();

            return window;
        }
    }
}