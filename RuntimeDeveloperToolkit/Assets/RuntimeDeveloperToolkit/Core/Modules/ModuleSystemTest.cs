using UnityEngine;
using RuntimeDeveloperToolkit.Core;

public sealed class ModuleSystemTest : MonoBehaviour
{
    private void Start()
    {
        RuntimeDeveloperToolkitRuntime runtime =
            RuntimeDeveloperToolkitRuntime.Instance;

        if (runtime == null)
        {
            Debug.LogError(
                "Runtime Developer Toolkit runtime was not found.");

            return;
        }

        TestRuntimeModule module = new TestRuntimeModule();

        bool registered = runtime.Modules.Register(module);

        Debug.Log($"Module registered: {registered}");
        Debug.Log($"Module count: {runtime.Modules.Count}");

        runtime.Modules.InitializeAll();
        runtime.Modules.EnableAll();

        Debug.Log(
            $"Module initialized: {module.IsInitialized}");

        Debug.Log(
            $"Module enabled: {module.IsEnabled}");
    } 
}