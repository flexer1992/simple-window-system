using UnityEngine;
using UnityEngine.UI;

namespace Windows.Infrastructure
{
    public class CanvasAspectSetter : MonoBehaviour
    {
        private readonly float _targetAspect = 9.0f / 16.0f;

        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            var canvasAspect = canvas.pixelRect.width / canvas.pixelRect.height;
            var canvasScaler = GetComponent<CanvasScaler>();

            if (canvasAspect > _targetAspect)
            {
                canvasScaler.matchWidthOrHeight = 1;
            }
            else
            {
                canvasScaler.matchWidthOrHeight = 0;
            }
        }
    }
}