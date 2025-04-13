using Windows.Common;

namespace Windows.Infrastructure
{
    public interface IModelsFactory
    {
        TModel Create<TModel>() where TModel : BaseWindowModel, new();
    }
}