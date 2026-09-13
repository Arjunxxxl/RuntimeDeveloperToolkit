using UnityEngine;
using Toolkit = RuntimeDeveloperToolkit.RuntimeDeveloperToolkit;
using RuntimeDeveloperToolkit.Settings;

public sealed class RuntimeSettingsIntegrationTest : MonoBehaviour
{
    private void Start()
    {
        if (!Toolkit.IsInitialized)
        {
            Debug.LogError(
                "Toolkit is not initialized.");

            return;
        }

        if (!Toolkit.Services.TryGet<RuntimeSettingsService>(
                "settings",
                out RuntimeSettingsService settings))
        {
            Debug.LogError(
                "Settings service was not registered.");

            return;
        }

        Debug.Log(
            $"Settings service initialized: " +
            $"{settings.IsInitialized}");

        RuntimeSettings performance =
            new RuntimeSettings();

        RuntimeSetting<int> history =
            new RuntimeSetting<int>(
                "history_size",
                120);

        performance.Register(history);

        bool registered =
            settings.RegisterGroup(
                "performance",
                performance);

        Debug.Log(
            $"Performance group registered: {registered}");

        Debug.Log(
            $"Settings group count: " +
            $"{settings.Registry.Count}");

        history.Value = 240;

        Debug.Log(
            $"History value: {history.Value}");

        settings.ResetAll();

        Debug.Log(
            $"History after reset: {history.Value}");
    }
}