namespace Windows.Infrastructure
{
    public interface IPathWindowProvider
    {
        public string Get<T>() where T : IWindow;
    }
}