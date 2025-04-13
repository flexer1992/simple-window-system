using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Infrastructure
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class WindowsContainer : MonoBehaviour
    {
        [SerializeField] private Canvas targetCanvas;
        [SerializeField] private CanvasScaler canvasScaler;
        [SerializeField] private RectTransform windowsRoot;

        private readonly Stack<IPresenter> _openedWindows = new(10);

        public async Task ShowWindow(IPresenter windowPresenter)
        {
            var window = windowPresenter.Window;
            SetWindowRoot(window, windowsRoot);
            await OpenStackedWindow(windowPresenter);
        }

        private async Task OpenStackedWindow(IPresenter windowPresenter)
        {
            await CheckTopStackedWindow();
            _openedWindows.Push(windowPresenter);
            // build first window state
            await windowPresenter.ConstructWindow();
        }

        private async Task CheckTopStackedWindow()
        {
            var presenter = _openedWindows.Count > 0 ? _openedWindows.Peek() : null;

            if (presenter != null)
            {
                await presenter.DeconstructWindow();
            }
        }

        private void SetWindowRoot(IWindow window, RectTransform root)
        {
            window.Transform.SetParent(root, worldPositionStays: false);

            var rect = window.Rect;

            // Растянуть окно на весь родительский контейнер
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.pivot = new Vector2(0.5f, 0.5f);

            window.Transform.localScale = Vector3.one;
            rect.localPosition = Vector3.zero;
            window.Transform.SetAsLastSibling();
        }

        private void Reset()
        {
            targetCanvas = GetComponent<Canvas>();
            canvasScaler = GetComponent<CanvasScaler>();
            targetCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1080, 1920);
            canvasScaler.referencePixelsPerUnit = 1.0f;
            canvasScaler.matchWidthOrHeight = 1;
        }

        public async Task HideTopWindow()
        {
#if DEBUG
            if (_openedWindows.Count <= 0)
            {
                Debug.LogError("[WindowService] Cannot hide top window: no opened windows.");
                return;
            }
#endif

            var topWindow = _openedWindows.Pop();
            await topWindow.DeconstructWindow();
            topWindow.Dispose();

            if (_openedWindows.Count > 0)
            {
                var windowForOpen = _openedWindows.Peek();
                await windowForOpen.ConstructWindow();
            }
        }
    }
}