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

        TestWindow window =
            new TestWindow();

        bool registered =
            ui.Windows.Register(window);

        Debug.Log(
            $"Window registered: {registered}");

        Debug.Log(
            $"Window count: {ui.Windows.Count}");

        ui.Windows.Show("test_window");

        Debug.Log(
            $"Window visible: {window.IsVisible}");

        window.SetPosition(
            new Vector2(0f, 0f));
    }

    private sealed class TestWindow : RuntimeWindow
    {
        public override string Id =>
            "test_window";

        public override string Title =>
            "Test Window";
    }
}