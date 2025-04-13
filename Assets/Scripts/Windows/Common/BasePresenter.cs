using System.Threading.Tasks;
using Windows.Infrastructure;

namespace Windows.Common
{
    public class BasePresenter<TModel, TWindow> : IPresenter where TModel : BaseWindowModel where TWindow : IWindow
    {
        protected readonly TModel Model;
        protected readonly TWindow WindowInstance;
        protected readonly IWindowCloseMediator WindowCloseMediator;

        public IWindow Window => WindowInstance;

        public BasePresenter(TModel model, TWindow windowInstance, IWindowCloseMediator windowCloseMediator)
        {
            Model = model;
            WindowInstance = windowInstance;
            WindowCloseMediator = windowCloseMediator;
        }

        public virtual async Task<IPresenter> ConstructWindow()
        {
            await WindowInstance.Construct();
            return this;
        }

        public virtual async Task DeconstructWindow()
        {
            await WindowInstance.Deconstruct();
        }

        public void Dispose()
        {
            Model.Dispose();
            WindowInstance.Dispose();
        }
    }
}