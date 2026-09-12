using UnityEngine;

namespace RuntimeDeveloperToolkit.Core
{
    /// <summary>
    /// Automatically bootstraps the Runtime Developer Toolkit.
    /// 
    /// This class is responsible only for creating and maintaining
    /// the toolkit runtime host.
    /// </summary>
    internal static class RuntimeDeveloperToolkitBootstrap
    {
        private const string RuntimeGameObjectName = "[Runtime Developer Toolkit]";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            CreateRuntime();
        }

        private static void CreateRuntime()
        {
            if (RuntimeDeveloperToolkitRuntime.Instance != null)
            {
                return;
            }

            GameObject runtimeObject = new GameObject(RuntimeGameObjectName);

            Object.DontDestroyOnLoad(runtimeObject);

            RuntimeDeveloperToolkitRuntime runtime =
                runtimeObject.AddComponent<RuntimeDeveloperToolkitRuntime>();

            runtime.Initialize();
        }
    }
}