using UnityEngine;
using RuntimeDeveloperToolkit.Core;
using RuntimeDeveloperToolkit.Core.Scheduling;

public sealed class RuntimeSchedulerTest : MonoBehaviour
{
    private float _frameTimer;
    private float _tenHzTimer;
    private float _oneHzTimer;

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

        runtime.Scheduler.Register(
            OnEveryFrame,
            RuntimeUpdateRate.EveryFrame);

        runtime.Scheduler.Register(
            OnTenHz,
            RuntimeUpdateRate.Hz10);

        runtime.Scheduler.Register(
            OnOneHz,
            RuntimeUpdateRate.Hz1);

        Debug.Log(
            $"Scheduler registrations: {runtime.Scheduler.Count}");
    }

    private void OnEveryFrame(RuntimeUpdateContext context)
    {
        _frameTimer += context.UnscaledDeltaTime;
    }

    private void OnTenHz(RuntimeUpdateContext context)
    {
        _tenHzTimer += context.UnscaledDeltaTime;

        /*Debug.Log(
            $"[Scheduler] 10Hz callback. " +
            $"Elapsed: {_tenHzTimer:F2}s");*/
    }

    private void OnOneHz(RuntimeUpdateContext context)
    {
        _oneHzTimer += context.UnscaledDeltaTime;

        /*Debug.Log(
            $"[Scheduler] 1Hz callback. " +
            $"Elapsed: {_oneHzTimer:F2}s");*/
    }

    private void OnDestroy()
    {
        RuntimeDeveloperToolkitRuntime runtime =
            RuntimeDeveloperToolkitRuntime.Instance;

        if (runtime == null || runtime.Scheduler == null)
        {
            return;
        }

        runtime.Scheduler.Unregister(OnEveryFrame);
        runtime.Scheduler.Unregister(OnTenHz);
        runtime.Scheduler.Unregister(OnOneHz);
    }
} 