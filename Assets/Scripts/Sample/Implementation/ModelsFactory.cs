using System;
using System.Collections.Generic;
using Windows.Common;
using Windows.Infrastructure;
using Sample.Config;
using Sample.Windows;

namespace Sample.Implementation
{
    public class ModelsFactory : IModelsFactory
    {
        private readonly Dictionary<Type, Func<BaseWindowModel>> _modelFactories = new();

        public ModelsFactory(PuzzleConfig puzzleConfig)
        {
            _modelFactories[typeof(PreviewPuzzleModel)] = () => new PreviewPuzzleModel(puzzleConfig);
        }

        public TModel Create<TModel>() where TModel : BaseWindowModel, new()
        {
            if (_modelFactories.TryGetValue(typeof(TModel), out var factory))
            {
                return (TModel)factory();
            }

            throw new InvalidOperationException($"No factory registered for {typeof(TModel)}");
        }
    }
}