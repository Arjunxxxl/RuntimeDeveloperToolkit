using System;
using UnityEngine;
using UnityEngine.UI;
using RuntimeDeveloperToolkit.UI.Themes;

namespace RuntimeDeveloperToolkit.UI.Controls
{
    public sealed class RuntimeUIButton
    {
        private readonly GameObject _gameObject;
        private readonly Button _button;
        private Text _label;

        public GameObject GameObject => _gameObject;
        public RectTransform RectTransform { get; }
        public Button ButtonComponent => _button;

        public string Text
        {
            get => _label.text;
            set => _label.text = value;
        }

        public bool IsInteractable
        {
            get => _button.interactable;
            set => _button.interactable = value;
        }

        public bool IsActive
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }

        public RuntimeUIButton(Transform parent)
        {
            _gameObject = new GameObject("Button");

            RectTransform = _gameObject.AddComponent<RectTransform>();
            RectTransform.SetParent(parent, false);

            _button = _gameObject.AddComponent<Button>();

            CreateBackground();
            CreateLabel();
        }

        private void CreateBackground()
        {
            Image background = _gameObject.AddComponent<Image>();
            background.color = new Color(0.2f, 0.2f, 0.2f, 1f);

            _button.targetGraphic = background;
        }

        private void CreateLabel()
        {
            GameObject labelObject = new GameObject("Label");

            RectTransform labelTransform =
                labelObject.AddComponent<RectTransform>();

            labelTransform.SetParent(_gameObject.transform, false);

            labelTransform.anchorMin = Vector2.zero;
            labelTransform.anchorMax = Vector2.one;
            labelTransform.offsetMin = Vector2.zero;
            labelTransform.offsetMax = Vector2.zero;

            _label = labelObject.AddComponent<Text>();

            _label.font =
                Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

            _label.fontSize = 14;
            _label.color = Color.white;
            _label.alignment = TextAnchor.MiddleCenter;
        }
        
        public void ApplyTheme(RuntimeUIThemeData theme)
        {
            if (theme == null)
                return; 

            Image background = _button.targetGraphic as Image;

            if (background != null)
                background.color = theme.PrimaryColor;

            _label.color = theme.TextColor;
            _label.fontSize = theme.BodyFontSize;
        }

        public void SetText(string text)
        {
            _label.text = text;
        }

        public void SetSize(Vector2 size)
        {
            RectTransform.sizeDelta = size;
        }

        public void SetPosition(Vector2 position)
        {
            RectTransform.anchoredPosition = position;
        }

        public void SetOnClick(Action callback)
        {
            _button.onClick.RemoveAllListeners();

            if (callback == null)
                return;

            _button.onClick.AddListener(() => callback());
        }

        public void Destroy()
        {
            if (_gameObject != null)
            {
                UnityEngine.Object.Destroy(_gameObject);
            }
        }
    }
}