using UnityEngine;
using Toolkit = RuntimeDeveloperToolkit.RuntimeDeveloperToolkit;
using RuntimeDeveloperToolkit.UI.Services;

public sealed class RuntimeUIServiceTest : MonoBehaviour
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
                "UI service was not registered.");

            return;
        }

        Debug.Log(
            $"UI service initialized: " +
            $"{ui.IsInitialized}");

        Debug.Log(
            $"Canvas exists: " +
            $"{ui.Canvas != null}");

        Debug.Log(
            $"Canvas render mode: " +
            $"{ui.Canvas.renderMode}");
    }
}