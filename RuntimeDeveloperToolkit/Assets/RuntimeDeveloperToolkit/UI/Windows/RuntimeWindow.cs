using UnityEngine;
using UnityEngine.UI;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public abstract class RuntimeWindow : IRuntimeWindow
    {
        private GameObject _rootObject;
        private RectTransform _root;
        private bool _isInitialized;
        private bool _isVisible;

        public abstract string Id { get; }

        public abstract string Title { get; }

        public bool IsVisible => _isVisible;

        public bool IsInitialized => _isInitialized;

        public RectTransform Root => _root;

        public void Initialize(Transform parent)
        {
            if (_isInitialized)
            {
                return;
            }

            if (parent == null)
            {
                Debug.LogError(
                    $"Cannot initialize window '{Id}' " +
                    "because the parent is null.");

                return;
            }

            _rootObject =
                new GameObject(Title);

            _rootObject.transform.SetParent(
                parent,
                false);

            _root =
                _rootObject.AddComponent<RectTransform>();

            Image background =
                _rootObject.AddComponent<Image>();

            background.color =
                new Color(0.08f, 0.08f, 0.08f, 0.95f);

            _root.anchorMin =
                new Vector2(0.5f, 0.5f);

            _root.anchorMax =
                new Vector2(0.5f, 0.5f);

            _root.pivot =
                new Vector2(0.5f, 0.5f);

            _root.sizeDelta =
                new Vector2(500f, 300f);

            _root.anchoredPosition =
                Vector2.zero;

            _isInitialized = true;

            OnInitialize();

            Hide();
        }

        public void Show()
        {
            if (!_isInitialized)
            {
                return;
            }

            _isVisible = true;

            _rootObject.SetActive(true);

            OnShow();
        }

        public void Hide()
        {
            if (!_isInitialized)
            {
                return;
            }

            _isVisible = false;

            _rootObject.SetActive(false);

            OnHide();
        }
 
        public void SetPosition(Vector2 position)
        {
            if (!_isInitialized)
            {
                return;
            }

            _root.anchoredPosition = position;
        }

        public void Dispose()
        {
            if (!_isInitialized)
            {
                return;
            }

            OnDispose();

            if (_rootObject != null)
            {
                Object.Destroy(_rootObject);
            }

            _rootObject = null;
            _root = null;

            _isVisible = false;
            _isInitialized = false;
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        protected virtual void OnDispose()
        {
        }
    }
}