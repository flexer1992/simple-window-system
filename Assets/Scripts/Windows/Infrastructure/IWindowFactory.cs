namespace Windows.Infrastructure
{
    public interface IWindowFactory
    {
        IWindow Create<T>() where T : IWindow;
    }
}