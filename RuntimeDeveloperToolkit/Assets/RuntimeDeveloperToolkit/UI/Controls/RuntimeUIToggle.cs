using System;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeDeveloperToolkit.UI.Controls
{
    public sealed class RuntimeUIToggle
    {
        private readonly GameObject _gameObject;
        private Toggle _toggle;
        private Text _label;

        public GameObject GameObject => _gameObject;
        public RectTransform RectTransform { get; }
        public Toggle ToggleComponent => _toggle;

        public string Text
        {
            get => _label.text;
            set => _label.text = value;
        }

        public bool Value
        {
            get => _toggle.isOn;
            set => _toggle.isOn = value;
        }

        public bool IsInteractable
        {
            get => _toggle.interactable;
            set => _toggle.interactable = value;
        }

        public bool IsActive
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }

        public RuntimeUIToggle(Transform parent)
        {
            _gameObject = new GameObject("Toggle");

            RectTransform = _gameObject.AddComponent<RectTransform>();
            RectTransform.SetParent(parent, false);

            CreateToggle();
            CreateLabel();
        }

        private void CreateToggle()
        {
            _toggle = _gameObject.AddComponent<Toggle>();

            GameObject backgroundObject =
                new GameObject("Background");

            RectTransform backgroundTransform =
                backgroundObject.AddComponent<RectTransform>();

            backgroundTransform.SetParent(
                _gameObject.transform,
                false);

            backgroundTransform.anchorMin = new Vector2(0f, 0.5f);
            backgroundTransform.anchorMax = new Vector2(0f, 0.5f);
            backgroundTransform.pivot = new Vector2(0f, 0.5f);
            backgroundTransform.sizeDelta = new Vector2(20f, 20f);

            Image background =
                backgroundObject.AddComponent<Image>();

            background.color =
                new Color(0.25f, 0.25f, 0.25f, 1f);

            GameObject checkmarkObject =
                new GameObject("Checkmark");

            RectTransform checkmarkTransform =
                checkmarkObject.AddComponent<RectTransform>();

            checkmarkTransform.SetParent(
                backgroundTransform,
                false);

            checkmarkTransform.anchorMin = Vector2.zero;
            checkmarkTransform.anchorMax = Vector2.one;
            checkmarkTransform.offsetMin = Vector2.zero;
            checkmarkTransform.offsetMax = Vector2.zero;

            Image checkmark =
                checkmarkObject.AddComponent<Image>();

            checkmark.color =
                new Color(0.2f, 0.8f, 0.2f, 1f);

            _toggle.targetGraphic = background;
            _toggle.graphic = checkmark;
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
                new Vector2(0f, 0.5f);

            labelTransform.anchorMax =
                new Vector2(0f, 0.5f);

            labelTransform.pivot =
                new Vector2(0f, 0.5f);

            labelTransform.anchoredPosition =
                new Vector2(30f, 0f);

            labelTransform.sizeDelta =
                new Vector2(200f, 30f);

            _label = labelObject.AddComponent<Text>();

            _label.font =
                Resources.GetBuiltinResource<Font>(
                    "LegacyRuntime.ttf");

            _label.fontSize = 14;
            _label.color = Color.white;
            _label.alignment = TextAnchor.MiddleLeft;
        }

        public void SetValue(bool value)
        {
            _toggle.isOn = value;
        }

        public void SetOnValueChanged(Action<bool> callback)
        {
            _toggle.onValueChanged.RemoveAllListeners();

            if (callback == null)
                return;

            _toggle.onValueChanged.AddListener(
                value => callback(value));
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
                UnityEngine.Object.Destroy(_gameObject);
            }
        }
    }
}