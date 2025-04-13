using System;
using System.Collections.Generic;
using Windows.Common;
using Windows.Infrastructure;
using Sample.Windows;
using Sample.Windows.confirmation;
using UnityEngine;

namespace Sample.Implementation
{
    public class PresentersFactory : IPresentersFactory
    {
        private readonly IWindowCloseMediator _windowCloseMediator;
        private readonly IWindowFactory _windowFactory;
        private readonly IModelsFactory _modelsFactory;


        private readonly Dictionary<Type, Func<BaseWindowModel, IWindow, IWindowCloseMediator, IPresenter>>
            _presenterFactories = new();

        public PresentersFactory(IWindowCloseMediator windowCloseMediator, IWindowFactory windowFactory,
            IModelsFactory modelsFactory, OpenConfirmWindowEventMediator openConfirmWindowEventMediator)
        {
            _windowCloseMediator = windowCloseMediator;
            _windowFactory = windowFactory;
            _modelsFactory = modelsFactory;

            _presenterFactories[typeof(PreviewPuzzlePresenter)] =
                (model, window, mediator) =>
                    new PreviewPuzzlePresenter((PreviewPuzzleModel)model, (PreviewPuzzleWindow)window, mediator,
                        openConfirmWindowEventMediator);

            _presenterFactories[typeof(ConfirmationWindowPresenter)] =
                (model, window, mediator) =>
                    new ConfirmationWindowPresenter((ConfirmationWindowModel)model, (ConfirmationWindowView)window,
                        mediator);
        }

        public TPresenter Create<TPresenter, TWindow, TModel>() where TPresenter : BasePresenter<TModel, TWindow>
            where TWindow : IWindow, new()
            where TModel : BaseWindowModel, new()
        {
            var model = _modelsFactory.Create<TModel>();
            return CreateInternal<TPresenter, TWindow, TModel>(model);
        }

        public TPresenter Create<TPresenter, TWindow, TModel>(TModel model)
            where TPresenter : BasePresenter<TModel, TWindow>
            where TWindow : IWindow, new()
            where TModel : BaseWindowModel
        {
            return CreateInternal<TPresenter, TWindow, TModel>(model);
        }

        private TPresenter CreateInternal<TPresenter, TWindow, TModel>(TModel model)
            where TPresenter : BasePresenter<TModel, TWindow>
            where TWindow : IWindow, new()
            where TModel : BaseWindowModel
        {
            var window = _windowFactory.Create<TWindow>();

            if (_presenterFactories.TryGetValue(typeof(TPresenter), out var factory))
            {
                var presenter = factory(model, window, _windowCloseMediator);
                return (TPresenter)presenter;
            }

            Debug.LogError($"Not found factory for : {typeof(TPresenter).FullName}");

            // Fallback — рефлексия
            return (TPresenter)Activator.CreateInstance(
                typeof(TPresenter),
                model,
                window,
                _windowCloseMediator
            );
        }
    }
}