using UnityEngine;
using UnityEngine.UI;
using RuntimeDeveloperToolkit.UI.Themes;

namespace RuntimeDeveloperToolkit.UI.Controls
{
    public sealed class RuntimeUILabel
    {
        private readonly GameObject _gameObject;
        private readonly Text _text;

        public GameObject GameObject => _gameObject;
        public RectTransform RectTransform { get; }
        public Text TextComponent => _text;

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        public bool IsActive
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }

        public RuntimeUILabel(Transform parent) 
        {
            _gameObject = new GameObject("Label");

            RectTransform = _gameObject.AddComponent<RectTransform>();
            RectTransform.SetParent(parent, false);

            _text = _gameObject.AddComponent<Text>();

            _text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            _text.fontSize = 14;
            _text.color = Color.white;
            _text.alignment = TextAnchor.MiddleLeft;
            _text.horizontalOverflow = HorizontalWrapMode.Wrap;
            _text.verticalOverflow = VerticalWrapMode.Truncate;
        }
        
        public void ApplyTheme(RuntimeUIThemeData theme)
        {
            if (theme == null)
                return;

            _text.color = theme.TextColor;
            _text.fontSize = theme.BodyFontSize;
        }

        public void SetFontSize(int size)
        {
            _text.fontSize = Mathf.Max(1, size);
        }

        public void SetAlignment(TextAnchor alignment)
        {
            _text.alignment = alignment;
        }

        public void SetColor(Color color)
        {
            _text.color = color;
        }

        public void SetPosition(Vector2 position)
        {
            RectTransform.anchoredPosition = position;
        }

        public void SetSize(Vector2 size)
        {
            RectTransform.sizeDelta = size;
        }

        public void Destroy()
        {
            if (_gameObject != null)
            {
                Object.Destroy(_gameObject);
            }
        }
    }
}