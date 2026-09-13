using RuntimeDeveloperToolkit.Core.Services;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using RuntimeDeveloperToolkit.UI.Windows;
using RuntimeDeveloperToolkit.UI.Themes;

namespace RuntimeDeveloperToolkit.UI.Services
{
    public sealed class RuntimeUIService : IRuntimeService
    {
        private GameObject _rootObject;
        private Canvas _canvas;
        private GameObject _eventSystemObject;
        
        public RuntimeWindowManager Windows { get; private set; }
        
        private RuntimeUITheme _theme;

        public string Id => "ui";

        public bool IsInitialized =>
            _rootObject != null &&
            _canvas != null;

        public Canvas Canvas => _canvas;
        
        public RuntimeUITheme Theme => _theme;

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            CreateRoot();
            CreateCanvas();
            CreateEventSystem();
            
            Windows = new RuntimeWindowManager();

            Windows.Initialize(_canvas.transform);
            
            _theme = new RuntimeUITheme();
            _theme.ThemeChanged += OnThemeChanged;
        }

        public void Shutdown()
        {
            Windows?.DisposeAll();
            
            if (_rootObject != null)
            {
                Object.Destroy(_rootObject);
            }

            if (_eventSystemObject != null)
            {
                Object.Destroy(_eventSystemObject);
            }
            
            if (_theme != null)
            {
                _theme.ThemeChanged -= OnThemeChanged;
                _theme = null;
            }

            Windows = null;
            _rootObject = null;
            _canvas = null;
            _eventSystemObject = null;
        }

        private void CreateRoot()
        {
            _rootObject = new GameObject(
                "[Runtime Developer Toolkit UI]");

            Object.DontDestroyOnLoad(_rootObject);
        }

        private void CreateCanvas()
        {
            GameObject canvasObject =
                new GameObject("Canvas");

            canvasObject.transform.SetParent(
                _rootObject.transform,
                false);

            _canvas =
                canvasObject.AddComponent<Canvas>();

            _canvas.renderMode =
                RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler =
                canvasObject.AddComponent<CanvasScaler>();

            scaler.uiScaleMode =
                CanvasScaler.ScaleMode.ScaleWithScreenSize;

            scaler.referenceResolution =
                new Vector2(1920f, 1080f);

            scaler.screenMatchMode =
                CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

            scaler.matchWidthOrHeight = 0.5f;

            canvasObject.AddComponent<GraphicRaycaster>();
        }
        
        private void CreateEventSystem()
        {
            EventSystem existingEventSystem =
                Object.FindFirstObjectByType<EventSystem>();

            if (existingEventSystem != null)
            {
                return;
            }

            _eventSystemObject =
                new GameObject(
                    "[Runtime Developer Toolkit EventSystem]");

            Object.DontDestroyOnLoad(
                _eventSystemObject);

            _eventSystemObject.AddComponent<EventSystem>();

            _eventSystemObject.AddComponent<InputSystemUIInputModule>();
        }
        
        private void OnThemeChanged(RuntimeUIThemeData theme)
        {
            if (theme == null)
                return;

            if (Windows == null)
                return;

            Windows.ApplyTheme(theme);
        }
    }
}