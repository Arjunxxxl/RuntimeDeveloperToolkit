using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public sealed class RuntimeWindowFocusHandler : MonoBehaviour, IPointerDownHandler
    {
        private RuntimeWindow _window;

        public void Initialize(RuntimeWindow window)
        {
            _window = window;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_window == null)
                return;

            RuntimeWindowManager manager = _window.Manager;

            if (manager == null)
                return;

            manager.FocusWindow(_window.Id);
        }
    }
}