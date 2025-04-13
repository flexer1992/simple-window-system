using System;
using System.Threading.Tasks;
using Windows.Common;
using Windows.Infrastructure;
using UnityEngine;

namespace Windows.Implementation
{
    public class WindowService : IWindowService
    {
        private readonly IWindowCloseMediator _windowCloseMediator;
        private readonly IPresentersFactory _presentersFactory;
        private readonly WindowsContainer _windowsContainer;

        public WindowService(IWindowCloseMediator windowCloseMediator, IPresentersFactory presentersFactory,
            WindowsContainer windowsContainer)
        {
            _windowCloseMediator = windowCloseMediator;
            _presentersFactory = presentersFactory;
            _windowsContainer = windowsContainer;

            _windowCloseMediator.OnCloseTopWindowRequested += OnCloseTopWindowRequestedHandler;
        }

        public async Task Show<TWindow, TModel, TPresenter>(TModel model) where TWindow : IWindow, new()
            where TModel : BaseWindowModel
            where TPresenter : BasePresenter<TModel, TWindow>
        {
            var presenter =
                _presentersFactory.Create<TPresenter, TWindow, TModel>(model);
            await _windowsContainer.ShowWindow(presenter);
        }

        public async Task Show<TWindow, TModel, TPresenter>() where TWindow : IWindow, new()
            where TModel : BaseWindowModel, new()
            where TPresenter : BasePresenter<TModel, TWindow>
        {
            var presenter =
                _presentersFactory.Create<TPresenter, TWindow, TModel>();
            await _windowsContainer.ShowWindow(presenter);
        }

        public async Task HideTopWindow()
        {
            await _windowsContainer.HideTopWindow();
        }


        public void Dispose()
        {
            _windowCloseMediator.OnCloseTopWindowRequested -= OnCloseTopWindowRequestedHandler;
        }

        private async void OnCloseTopWindowRequestedHandler()
        {
            try
            {
                await HideTopWindow();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}