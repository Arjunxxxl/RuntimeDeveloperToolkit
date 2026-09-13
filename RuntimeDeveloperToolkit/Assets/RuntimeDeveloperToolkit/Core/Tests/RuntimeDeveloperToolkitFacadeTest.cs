using UnityEngine;
using Toolkit = RuntimeDeveloperToolkit.RuntimeDeveloperToolkit;
using RuntimeDeveloperToolkit.Core.Modules;

public sealed class RuntimeDeveloperToolkitFacadeTest : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(
            $"Toolkit initialized: " +
            $"{Toolkit.IsInitialized}");

        Debug.Log(
            $"Toolkit shutting down: " +
            $"{Toolkit.IsShuttingDown}");

        if (!Toolkit.IsInitialized)
        {
            Debug.LogError(
                "Runtime Developer Toolkit is not initialized.");

            return;
        }

        Debug.Log(
            $"Module count: " +
            $"{Toolkit.Modules.Count}");

        Debug.Log(
            $"Scheduler count: " +
            $"{Toolkit.Scheduler.Count}");

        TestModuleRegistration();
    }

    private void TestModuleRegistration()
    {
        TestFacadeModule module =
            new TestFacadeModule();

        bool registered =
            Toolkit.Modules.Register(module);

        Debug.Log(
            $"Facade module registered: {registered}");

        Toolkit.Modules.InitializeAll();
        Toolkit.Modules.EnableAll();

        Debug.Log(
            $"Module initialized: {module.IsInitialized}");

        Debug.Log(
            $"Module enabled: {module.IsEnabled}");
    }

    private sealed class TestFacadeModule : RuntimeModule
    {
        public override string Id =>
            "facade_test";

        public override string DisplayName =>
            "Facade Test Module";
    }
}