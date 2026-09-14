using UnityEngine;
using Toolkit = RuntimeDeveloperToolkit.RuntimeDeveloperToolkit;
using RuntimeDeveloperToolkit.UI.Services;
using RuntimeDeveloperToolkit.UI.Windows;

public sealed class RuntimeWindowSystemTest : MonoBehaviour
{
    private void Start()
    {
        if (!Toolkit.IsInitialized)
        {
            Debug.LogError(
                "Toolkit is not initialized.");

            return;
        }

        if (!Toolkit.Services.TryGet<RuntimeUIService>(
                "ui",
                out RuntimeUIService ui))
        {
            Debug.LogError(
                "UI service was not found.");

            return;
        }
        
        CreateAndShowWindow("test_window 1", new Vector2(0, 0), ui);
        CreateAndShowWindow("test_window 2", new Vector2(100, 100), ui);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Toolkit.Services.TryGet<RuntimeUIService>(
                "ui",
                out RuntimeUIService uiService);

            if (uiService != null)
                uiService.Windows.FocusWindow("test_window 1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Toolkit.Services.TryGet<RuntimeUIService>(
                "ui",
                out RuntimeUIService uiService);

            if (uiService != null)
                uiService.Windows.FocusWindow("test_window 2");
        }
    }

    private void CreateAndShowWindow(string windowId, Vector2 windowPos, RuntimeUIService ui)
    {
        TestWindow window =
            new TestWindow(windowId, windowId);

        bool registered =
            ui.Windows.Register(window);

        Debug.Log(
            $"Window registered: {registered}");

        Debug.Log(
            $"Window count: {ui.Windows.Count}");

        ui.Windows.Show(windowId);

        Debug.Log(
            $"Window visible: {window.IsVisible}");

        window.SetPosition(windowPos);
    }

    private sealed class TestWindow : RuntimeWindow
    {
        private readonly string _id;
        private readonly string _title;
        
        public override string Id => _id;

        public override string Title => _title;

        public TestWindow(string id, string title)
        {
            _id = id;
            _title = id;
        }
    }
}