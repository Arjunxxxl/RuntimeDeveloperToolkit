using UnityEngine;
using RuntimeDeveloperToolkit.Core;
using RuntimeDeveloperToolkit.UI.Services;

namespace RuntimeDeveloperToolkit.UI.Bootstrap
{
    internal static class RuntimeUIBootstrap
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

            RuntimeUIService service =
                new RuntimeUIService();

            runtime.RegisterService(service);
        }
    }
}