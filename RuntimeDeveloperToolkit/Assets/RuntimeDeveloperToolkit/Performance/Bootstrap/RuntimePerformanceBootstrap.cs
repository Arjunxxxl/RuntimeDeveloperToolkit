using RuntimeDeveloperToolkit.Core;
using RuntimeDeveloperToolkit.Performance.Services;
using UnityEngine;

namespace RuntimeDeveloperToolkit.Performance.Bootstrap
{
    internal static class RuntimePerformanceBootstrap
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

            RuntimePerformanceService service =
                new RuntimePerformanceService();

            runtime.RegisterService(service);
        }
    }
}