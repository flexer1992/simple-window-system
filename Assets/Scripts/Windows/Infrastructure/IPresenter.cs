using System.Threading.Tasks;

namespace Windows.Infrastructure
{
    public interface IPresenter
    {
        public IWindow Window { get; }
        public Task<IPresenter> ConstructWindow();
        public Task DeconstructWindow();
        public void Dispose();
    }
}