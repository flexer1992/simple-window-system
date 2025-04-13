using System;
using Windows.Infrastructure;

namespace Sample.Implementation
{
    public class WindowCloseMediator : IWindowCloseMediator
    {
        public event Action OnCloseTopWindowRequested = delegate { };

        public void RequestCloseTopWindow()
        {
            OnCloseTopWindowRequested();
        }
    }
}