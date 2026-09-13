using UnityEngine;
using UnityEngine.UI;
using RuntimeDeveloperToolkit.UI.Controls;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public abstract class RuntimeWindow : IRuntimeWindow
    {
        private GameObject _rootObject;
        private RectTransform _root;

        private GameObject _titleBarObject;
        private RectTransform _titleBar;

        private GameObject _contentObject;
        private RectTransform _contentRoot;
 
        private GameObject _titleObject;
        private Text _titleText;

        private GameObject _closeButtonObject;
        private Button _closeButton;

        private bool _isInitialized;
        private bool _isVisible;

        public abstract string Id { get; }

        public abstract string Title { get; }

        public bool IsVisible => _isVisible;

        public bool IsInitialized => _isInitialized;

        public RectTransform Root => _root;

        public RectTransform TitleBar => _titleBar;

        public RectTransform ContentRoot => _contentRoot;
 
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

            CreateRoot(parent);
            CreateTitleBar();
            CreateTitle();
            CreateCloseButton();
            CreateContentRoot();

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

            _titleBarObject = null;
            _titleBar = null;

            _contentObject = null;
            _contentRoot = null;

            _titleObject = null;
            _titleText = null;

            _closeButtonObject = null;
            _closeButton = null;

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

        private void CreateRoot(Transform parent)
        {
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
                new Color(
                    0.08f,
                    0.08f,
                    0.08f,
                    0.95f);

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
        }

        private void CreateTitleBar()
        {
            _titleBarObject =
                new GameObject("Title Bar");

            _titleBarObject.transform.SetParent(
                _root,
                false);

            _titleBar =
                _titleBarObject.AddComponent<RectTransform>();

            Image background =
                _titleBarObject.AddComponent<Image>();

            background.color =
                new Color(
                    0.12f,
                    0.12f,
                    0.12f,
                    1f);

            _titleBar.anchorMin =
                new Vector2(0f, 1f);

            _titleBar.anchorMax =
                new Vector2(1f, 1f);

            _titleBar.pivot =
                new Vector2(0.5f, 1f);

            _titleBar.offsetMin =
                new Vector2(0f, -36f);

            _titleBar.offsetMax =
                Vector2.zero;
        }

        private void CreateTitle()
        {
            _titleObject =
                new GameObject("Title");

            _titleObject.transform.SetParent(
                _titleBar,
                false);

            _titleText =
                _titleObject.AddComponent<Text>();

            _titleText.text = Title;

            _titleText.font =
                Resources.GetBuiltinResource<Font>(
                    "LegacyRuntime.ttf");

            _titleText.fontSize = 16;

            _titleText.alignment =
                TextAnchor.MiddleLeft;

            _titleText.color =
                Color.white;

            RectTransform titleRect =
                _titleObject.GetComponent<RectTransform>();

            titleRect.anchorMin =
                new Vector2(0f, 0f);

            titleRect.anchorMax =
                new Vector2(1f, 1f);

            titleRect.offsetMin =
                new Vector2(12f, 0f);

            titleRect.offsetMax =
                new Vector2(-45f, 0f);
        }

        private void CreateCloseButton()
        {
            _closeButtonObject =
                new GameObject("Close Button");

            _closeButtonObject.transform.SetParent(
                _titleBar,
                false);

            Image background =
                _closeButtonObject.AddComponent<Image>();

            background.color =
                new Color(
                    0.2f,
                    0.2f,
                    0.2f,
                    1f);

            _closeButton =
                _closeButtonObject.AddComponent<Button>();

            _closeButton.onClick.AddListener(
                Hide);

            RectTransform buttonRect =
                _closeButtonObject.GetComponent<RectTransform>();

            buttonRect.anchorMin =
                new Vector2(1f, 0.5f);

            buttonRect.anchorMax =
                new Vector2(1f, 0.5f);

            buttonRect.pivot =
                new Vector2(1f, 0.5f);

            buttonRect.sizeDelta =
                new Vector2(36f, 36f);

            buttonRect.anchoredPosition =
                new Vector2(-2f, 0f);

            GameObject textObject =
                new GameObject("Text");

            textObject.transform.SetParent(
                _closeButtonObject.transform,
                false);

            Text text =
                textObject.AddComponent<Text>();

            text.text = "X";

            text.font =
                Resources.GetBuiltinResource<Font>(
                    "LegacyRuntime.ttf");

            text.fontSize = 16;

            text.alignment =
                TextAnchor.MiddleCenter;

            text.color =
                Color.white;

            RectTransform textRect =
                textObject.GetComponent<RectTransform>();

            textRect.anchorMin =
                Vector2.zero;

            textRect.anchorMax =
                Vector2.one;

            textRect.offsetMin =
                Vector2.zero;

            textRect.offsetMax =
                Vector2.zero;
        }

        private void CreateContentRoot()
        {
            _contentObject =
                new GameObject("Content");

            _contentObject.transform.SetParent(
                _root,
                false);

            _contentRoot =
                _contentObject.AddComponent<RectTransform>();

            _contentRoot.anchorMin =
                Vector2.zero;

            _contentRoot.anchorMax =
                Vector2.one;

            _contentRoot.offsetMin =
                new Vector2(
                    10f,
                    10f);

            _contentRoot.offsetMax =
                new Vector2(
                    -10f,
                    -46f);
            
            RuntimeUILabel testLabel =
                new RuntimeUILabel(_contentRoot);

            testLabel.Text = "Runtime Developer Toolkit";
            testLabel.SetFontSize(18);
            testLabel.SetSize(new Vector2(300f, 40f)); 
            
            RuntimeUIButton testButton =
                new RuntimeUIButton(_contentRoot);

            testButton.Text = "Test Button";
            testButton.SetSize(new Vector2(200f, 50f));
            testButton.SetPosition(new Vector2(0f, -60f));

            testButton.SetOnClick(() =>
            {
                Debug.Log("RuntimeUIButton clicked!");
            });
            
            RuntimeUIToggle testToggle =
                new RuntimeUIToggle(_contentRoot);

            testToggle.Text = "Enable Test Feature";
            testToggle.SetSize(new Vector2(250f, 40f));
            testToggle.SetPosition(new Vector2(0f, -120f));

            testToggle.SetOnValueChanged(value =>
            {
                Debug.Log("Toggle value: " + value);
            });
            
            RuntimeUISlider testSlider =
                new RuntimeUISlider(_contentRoot);

            testSlider.SetSize(new Vector2(300f, 40f));
            testSlider.SetPosition(new Vector2(0f, -180f));

            testSlider.SetRange(0f, 100f);
            testSlider.Value = 50f;

            testSlider.SetOnValueChanged(value =>
            {
                Debug.Log("Slider value: " + value);
            });
        }
    }
}