using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Windows.Infrastructure
{
    public interface IWindow
    {
        public event Action CloseButtonClick;

        public Transform Transform { get; }
        public RectTransform Rect { get; }

        Task Construct();
        Task Deconstruct();
        public void Dispose();
    }
}