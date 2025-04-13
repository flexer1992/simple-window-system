using System;

namespace Windows.Infrastructure
{
    public interface IWindowCloseMediator
    {
        void RequestCloseTopWindow();
        event Action OnCloseTopWindowRequested;
    }
}