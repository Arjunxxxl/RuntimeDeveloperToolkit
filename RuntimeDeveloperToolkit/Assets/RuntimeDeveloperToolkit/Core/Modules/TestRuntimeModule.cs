using UnityEngine;
using RuntimeDeveloperToolkit.Core.Modules;

public sealed class TestRuntimeModule : RuntimeModule
{
    public override string Id => "test";

    public override string DisplayName => "Test Module";

    protected override void OnInitialize()
    {
        Debug.Log("[Test Module] Initialized.");
    }

    protected override void OnEnable()
    {
        Debug.Log("[Test Module] Enabled.");
    }

    protected override void OnDisable()
    {
        Debug.Log("[Test Module] Disabled.");
    }

    protected override void OnDispose()
    {
        Debug.Log("[Test Module] Disposed.");
    }
}