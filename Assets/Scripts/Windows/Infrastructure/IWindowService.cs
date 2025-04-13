using System.Threading.Tasks;
using Windows.Common;

namespace Windows.Infrastructure
{
    public interface IWindowService
    {
        public Task Show<TWindow, TModel, TPresenter>(TModel model)
            where TWindow : IWindow, new()
            where TModel : BaseWindowModel
            where TPresenter : BasePresenter<TModel, TWindow>;


        public Task Show<TWindow, TModel, TPresenter>()
            where TWindow : IWindow, new()
            where TModel : BaseWindowModel, new()
            where TPresenter : BasePresenter<TModel, TWindow>;

        public Task HideTopWindow();
        public void Dispose();
    }
}