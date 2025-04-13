using System;
using System.Threading.Tasks;
using Windows.Infrastructure;
using UnityEngine;

namespace Windows.Common
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(CanvasRenderer))]
    public class BaseWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private RectTransform windowsRect;
        [SerializeField] private CanvasGroup canvasGroup;

        public event Action CloseButtonClick = delegate { };

        public Transform Transform => transform;
        public RectTransform Rect => windowsRect;

        public virtual Task Construct()
        {
            gameObject?.SetActive(true);
            return Task.CompletedTask;
        }

        public virtual Task Deconstruct()
        {
            gameObject?.SetActive(false);
            return Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }

        protected void RaiseCloseWindowEvent()
        {
            CloseButtonClick();
        }

        private void Reset()
        {
            windowsRect = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            windowsRect.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}