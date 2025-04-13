using Windows.Common;

namespace Windows.Infrastructure
{
    public interface IPresentersFactory
    {
        TPresenter Create<TPresenter, TWindow, TModel>()
            where TPresenter : BasePresenter<TModel, TWindow>
            where TModel : BaseWindowModel, new()
            where TWindow : IWindow, new();

        public TPresenter Create<TPresenter, TWindow, TModel>(TModel model)
            where TPresenter : BasePresenter<TModel, TWindow>
            where TModel : BaseWindowModel
            where TWindow : IWindow, new();
    }
}