using UnityEngine;
using Toolkit = RuntimeDeveloperToolkit.RuntimeDeveloperToolkit;
using RuntimeDeveloperToolkit.Core.Services;

public sealed class RuntimeServiceRegistryTest : MonoBehaviour
{
    private void Start()
    {
        if (!Toolkit.IsInitialized)
        {
            Debug.LogError(
                "Runtime Developer Toolkit is not initialized.");

            return;
        }

        Debug.Log(
            $"Service count: {Toolkit.Services.Count}");

        TestService service =
            new TestService();

        bool registered =
            Toolkit.Services.Register(service);

        Debug.Log(
            $"Service registered: {registered}");

        Debug.Log(
            $"Service count: {Toolkit.Services.Count}");

        Toolkit.Services.InitializeAll();

        Debug.Log(
            $"Service initialized: " +
            $"{service.IsInitialized}");

        if (Toolkit.Services.TryGet<TestService>(
                "test_service",
                out TestService retrievedService))
        {
            Debug.Log(
                $"Service retrieved: " +
                $"{retrievedService == service}");
        }
    }

    private sealed class TestService : IRuntimeService
    {
        public string Id => "test_service";

        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            IsInitialized = true;

            Debug.Log(
                "[Test Service] Initialized.");
        }

        public void Shutdown()
        {
            if (!IsInitialized)
            {
                return;
            }

            IsInitialized = false;

            Debug.Log(
                "[Test Service] Shutdown.");
        }
    }
}