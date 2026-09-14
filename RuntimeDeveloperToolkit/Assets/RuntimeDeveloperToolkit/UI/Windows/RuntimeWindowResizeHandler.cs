using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public enum RuntimeWindowResizeDirection
    {
        Left,
        Right,
        Top,
        Bottom,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
    
    public sealed class RuntimeWindowResizeHandler :
        MonoBehaviour,
        IBeginDragHandler,
        IDragHandler
    {
        [SerializeField]
        private RuntimeWindowResizeDirection _direction = RuntimeWindowResizeDirection.BottomRight;
        
        private RectTransform _windowRoot;
        private RectTransform _canvasRect;

        private Vector2 _initialPointerPosition;
        private Vector2 _initialSize;
        private Vector2 _initialPosition;

        private Vector2 _minimumSize = new Vector2(200f, 120f);
        private Vector2 _maximumSize;

        public void Initialize(
            RectTransform windowRoot,
            RectTransform canvasRect)
        {
            _windowRoot = windowRoot;
            _canvasRect = canvasRect;
        }
        
        public void SetMaximumSize(Vector2 maximumSize)
        {
            _maximumSize = maximumSize;
        }
        
        public void SetDirection(
            RuntimeWindowResizeDirection direction)
        {
            _direction = direction;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_windowRoot == null || _canvasRect == null)
                return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect,
                eventData.position,
                eventData.pressEventCamera,
                out _initialPointerPosition);

            _initialPosition = _windowRoot.anchoredPosition;
            _initialSize = _windowRoot.rect.size;
            
            _maximumSize = _canvasRect.rect.size;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_windowRoot == null || _canvasRect == null)
                return;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvasRect,
                    eventData.position,
                    eventData.pressEventCamera,
                    out Vector2 currentPointerPosition))
            {
                return;
            }

            Vector2 pointerDelta =
                currentPointerPosition - _initialPointerPosition;

            Vector2 newSize = _initialSize;
            Vector2 newPosition = _initialPosition;

            switch (_direction)
            {
                case RuntimeWindowResizeDirection.Left:
                    newSize.x = _initialSize.x - pointerDelta.x;
                    break;

                case RuntimeWindowResizeDirection.Right:
                    newSize.x = _initialSize.x + pointerDelta.x;
                    break;

                case RuntimeWindowResizeDirection.Top:
                    newSize.y = _initialSize.y + pointerDelta.y;
                    break;

                case RuntimeWindowResizeDirection.Bottom:
                    newSize.y = _initialSize.y - pointerDelta.y;
                    break;

                case RuntimeWindowResizeDirection.TopLeft:
                    newSize.x = _initialSize.x - pointerDelta.x;
                    newSize.y = _initialSize.y + pointerDelta.y;
                    break;

                case RuntimeWindowResizeDirection.TopRight:
                    newSize.x = _initialSize.x + pointerDelta.x;
                    newSize.y = _initialSize.y + pointerDelta.y;
                    break;

                case RuntimeWindowResizeDirection.BottomLeft:
                    newSize.x = _initialSize.x - pointerDelta.x;
                    newSize.y = _initialSize.y - pointerDelta.y;
                    break;

                case RuntimeWindowResizeDirection.BottomRight:
                    newSize.x = _initialSize.x + pointerDelta.x;
                    newSize.y = _initialSize.y - pointerDelta.y;
                    break;
            }

            newSize.x = Mathf.Clamp(
                newSize.x,
                _minimumSize.x,
                _maximumSize.x);

            newSize.y = Mathf.Clamp(
                newSize.y,
                _minimumSize.y,
                _maximumSize.y); 

            Vector2 sizeDelta =
                newSize - _initialSize;

            if (_direction == RuntimeWindowResizeDirection.Left ||
                _direction == RuntimeWindowResizeDirection.TopLeft ||
                _direction == RuntimeWindowResizeDirection.BottomLeft)
            {
                newPosition.x =
                    _initialPosition.x -
                    sizeDelta.x * 0.5f;
            }

            if (_direction == RuntimeWindowResizeDirection.Top ||
                _direction == RuntimeWindowResizeDirection.TopLeft ||
                _direction == RuntimeWindowResizeDirection.TopRight)
            {
                newPosition.y =
                    _initialPosition.y +
                    sizeDelta.y * 0.5f;
            }

            _windowRoot.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                newSize.x);

            _windowRoot.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                newSize.y);

            _windowRoot.anchoredPosition =
                newPosition;
        }
    }
}