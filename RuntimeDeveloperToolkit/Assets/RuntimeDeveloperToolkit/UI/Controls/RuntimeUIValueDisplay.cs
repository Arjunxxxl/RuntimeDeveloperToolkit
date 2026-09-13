using UnityEngine;
using UnityEngine.UI;

namespace RuntimeDeveloperToolkit.UI.Controls
{
    public sealed class RuntimeUIValueDisplay
    {
        private readonly GameObject _gameObject;
        private Text _labelText;
        private Text _valueText;

        public GameObject GameObject => _gameObject;
        public RectTransform RectTransform { get; }

        public string Label
        {
            get => _labelText.text;
            set => _labelText.text = value;
        }

        public string Value
        {
            get => _valueText.text;
            set => _valueText.text = value;
        }

        public bool IsActive
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }

        public RuntimeUIValueDisplay(Transform parent)
        {
            _gameObject = new GameObject("Value Display");

            RectTransform = _gameObject.AddComponent<RectTransform>();
            RectTransform.SetParent(parent, false);

            CreateLabel();
            CreateValue();
        }

        private void CreateLabel()
        {
            GameObject labelObject =
                new GameObject("Label");

            RectTransform labelTransform =
                labelObject.AddComponent<RectTransform>();

            labelTransform.SetParent(
                _gameObject.transform,
                false);

            labelTransform.anchorMin =
                new Vector2(0f, 0f);

            labelTransform.anchorMax =
                new Vector2(0.5f, 1f);

            labelTransform.offsetMin =
                new Vector2(0f, 0f);

            labelTransform.offsetMax =
                new Vector2(0f, 0f);

            _labelText =
                labelObject.AddComponent<Text>();

            _labelText.font =
                Resources.GetBuiltinResource<Font>(
                    "LegacyRuntime.ttf");

            _labelText.fontSize = 14;
            _labelText.color = Color.white;
            _labelText.alignment =
                TextAnchor.MiddleLeft;
        }

        private void CreateValue()
        {
            GameObject valueObject =
                new GameObject("Value");

            RectTransform valueTransform =
                valueObject.AddComponent<RectTransform>();

            valueTransform.SetParent(
                _gameObject.transform,
                false);

            valueTransform.anchorMin =
                new Vector2(0.5f, 0f);

            valueTransform.anchorMax =
                new Vector2(1f, 1f);

            valueTransform.offsetMin =
                new Vector2(0f, 0f);

            valueTransform.offsetMax =
                new Vector2(0f, 0f);

            _valueText =
                valueObject.AddComponent<Text>();

            _valueText.font =
                Resources.GetBuiltinResource<Font>(
                    "LegacyRuntime.ttf");

            _valueText.fontSize = 14;
            _valueText.color = Color.white;
            _valueText.alignment =
                TextAnchor.MiddleRight;
        }

        public void SetValue(string value)
        {
            _valueText.text = value;
        }

        public void SetLabel(string label)
        {
            _labelText.text = label;
        }

        public void SetLabelColor(Color color)
        {
            _labelText.color = color;
        }

        public void SetValueColor(Color color)
        {
            _valueText.color = color;
        }

        public void SetFontSize(int size)
        {
            int clampedSize = Mathf.Max(1, size);

            _labelText.fontSize = clampedSize;
            _valueText.fontSize = clampedSize;
        }

        public void SetSize(Vector2 size)
        {
            RectTransform.sizeDelta = size;
        }

        public void SetPosition(Vector2 position)
        {
            RectTransform.anchoredPosition = position;
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