using UnityEngine;
using RuntimeDeveloperToolkit.Core;
using RuntimeDeveloperToolkit.Core.Services;

namespace RuntimeDeveloperToolkit.Settings
{
    internal static class RuntimeSettingsBootstrap
    {
        [RuntimeInitializeOnLoadMethod(
            RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            RuntimeDeveloperToolkitRuntime runtime =
                RuntimeDeveloperToolkitRuntime.Instance;

            if (runtime == null ||
                !runtime.IsInitialized)
            {
                return;
            }

            RuntimeSettingsService service =
                new RuntimeSettingsService();

            runtime.RegisterService(service);
        }
    }
}