using RuntimeDeveloperToolkit.Core.Services;
using UnityEngine;
using UnityEngine.UI;
using RuntimeDeveloperToolkit.UI.Windows;

namespace RuntimeDeveloperToolkit.UI.Services
{
    public sealed class RuntimeUIService : IRuntimeService
    {
        private GameObject _rootObject;
        private Canvas _canvas;
        
        public RuntimeWindowManager Windows { get; private set; }

        public string Id => "ui";

        public bool IsInitialized =>
            _rootObject != null &&
            _canvas != null;

        public Canvas Canvas => _canvas;

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            CreateRoot();
            CreateCanvas();
            
            Windows = new RuntimeWindowManager();

            Windows.Initialize(_canvas.transform);
        }

        public void Shutdown()
        {
            Windows?.DisposeAll();
            
            if (_rootObject != null)
            {
                Object.Destroy(_rootObject);
            }

            Windows = null;
            _rootObject = null;
            _canvas = null;
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
    }
}