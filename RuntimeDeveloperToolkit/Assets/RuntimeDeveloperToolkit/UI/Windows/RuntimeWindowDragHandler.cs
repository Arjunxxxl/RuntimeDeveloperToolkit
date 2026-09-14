using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public sealed class RuntimeWindowDragHandler :
        MonoBehaviour,
        IBeginDragHandler,
        IDragHandler
    {
        private RectTransform _windowRoot;
        private RectTransform _canvasRect;
        private Vector2 _pointerOffset;
        
        private float _visibleMargin = 40f;

        public void Initialize(
            RectTransform windowRoot,
            RectTransform canvasRect)
        {
            _windowRoot = windowRoot;
            _canvasRect = canvasRect;
        }
 
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_windowRoot == null || _canvasRect == null)
                return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPointerPosition);

            _pointerOffset =
                _windowRoot.anchoredPosition -
                localPointerPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_windowRoot == null || _canvasRect == null)
                return;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvasRect,
                    eventData.position,
                    eventData.pressEventCamera,
                    out Vector2 localPointerPosition))
            {
                return;
            }

            Vector2 targetPosition =
                localPointerPosition + _pointerOffset;

            targetPosition = ConstrainPosition(targetPosition);

            _windowRoot.anchoredPosition = targetPosition;
        }
        
        private Vector2 ConstrainPosition(Vector2 position)
        {
            Vector2 canvasSize = _canvasRect.rect.size;
            Vector2 windowSize = _windowRoot.rect.size;

            float halfCanvasWidth = canvasSize.x * 0.5f;
            float halfCanvasHeight = canvasSize.y * 0.5f;

            float halfWindowWidth = windowSize.x * 0.5f;
            float halfWindowHeight = windowSize.y * 0.5f;

            float minX =
                -halfCanvasWidth +
                _visibleMargin -
                halfWindowWidth;

            float maxX =
                halfCanvasWidth -
                _visibleMargin +
                halfWindowWidth;

            float minY =
                -halfCanvasHeight +
                _visibleMargin -
                halfWindowHeight;

            float maxY =
                halfCanvasHeight -
                _visibleMargin +
                halfWindowHeight;

            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);

            return position;
        }
    }
}