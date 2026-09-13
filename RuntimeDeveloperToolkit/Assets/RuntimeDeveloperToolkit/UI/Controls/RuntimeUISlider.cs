using System;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeDeveloperToolkit.UI.Controls
{
    public sealed class RuntimeUISlider
    {
        private readonly GameObject _gameObject;
        private Slider _slider;

        public GameObject GameObject => _gameObject;
        public RectTransform RectTransform { get; }
        public Slider SliderComponent => _slider;

        public float Value
        {
            get => _slider.value;
            set => _slider.value = value;
        }

        public float MinValue
        {
            get => _slider.minValue;
            set => _slider.minValue = value;
        }

        public float MaxValue
        {
            get => _slider.maxValue;
            set => _slider.maxValue = value;
        }

        public bool IsInteractable
        {
            get => _slider.interactable;
            set => _slider.interactable = value;
        }

        public bool IsActive
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }

        public RuntimeUISlider(Transform parent)
        {
            _gameObject = new GameObject("Slider");

            RectTransform = _gameObject.AddComponent<RectTransform>();
            RectTransform.SetParent(parent, false);

            CreateSlider();
        }

        private void CreateSlider()
        {
            _slider = _gameObject.AddComponent<Slider>();

            GameObject backgroundObject =
                new GameObject("Background");

            RectTransform backgroundTransform =
                backgroundObject.AddComponent<RectTransform>();

            backgroundTransform.SetParent(
                _gameObject.transform,
                false);

            backgroundTransform.anchorMin = new Vector2(0f, 0.5f);
            backgroundTransform.anchorMax = new Vector2(1f, 0.5f);
            backgroundTransform.pivot = new Vector2(0.5f, 0.5f);

            backgroundTransform.offsetMin =
                new Vector2(0f, -5f);

            backgroundTransform.offsetMax =
                new Vector2(0f, 5f);

            Image background =
                backgroundObject.AddComponent<Image>();

            background.color =
                new Color(0.2f, 0.2f, 0.2f, 1f);

            GameObject fillObject =
                new GameObject("Fill");

            RectTransform fillTransform =
                fillObject.AddComponent<RectTransform>();

            fillTransform.SetParent(
                _gameObject.transform,
                false);

            fillTransform.anchorMin = new Vector2(0f, 0.5f);
            fillTransform.anchorMax = new Vector2(1f, 0.5f);
            fillTransform.pivot = new Vector2(0f, 0.5f);

            fillTransform.offsetMin =
                new Vector2(0f, -5f);

            fillTransform.offsetMax =
                new Vector2(0f, 5f);

            Image fill =
                fillObject.AddComponent<Image>();

            fill.color =
                new Color(0.2f, 0.6f, 1f);

            _slider.fillRect = fillTransform;

            GameObject handleObject =
                new GameObject("Handle");

            RectTransform handleTransform =
                handleObject.AddComponent<RectTransform>();

            handleTransform.SetParent(
                _gameObject.transform,
                false);

            handleTransform.sizeDelta =
                new Vector2(20f, 20f);

            Image handle =
                handleObject.AddComponent<Image>();

            handle.color = Color.white;

            _slider.handleRect = handleTransform;
            _slider.targetGraphic = handle;

            _slider.direction =
                Slider.Direction.LeftToRight;

            _slider.minValue = 0f;
            _slider.maxValue = 1f;
            _slider.value = 0.5f;
        }

        public void SetValue(float value)
        {
            _slider.value = value;
        }

        public void SetRange(float min, float max)
        {
            _slider.minValue = min;
            _slider.maxValue = max;
        }

        public void SetOnValueChanged(Action<float> callback)
        {
            _slider.onValueChanged.RemoveAllListeners();

            if (callback == null)
                return;

            _slider.onValueChanged.AddListener(
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