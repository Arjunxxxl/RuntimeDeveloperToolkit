using UnityEngine;
using RuntimeDeveloperToolkit.Core;
using RuntimeDeveloperToolkit.Performance.Data;
using RuntimeDeveloperToolkit.Performance.Services;

namespace RuntimeDeveloperToolkit.Performance.Tests
{
    public sealed class RuntimePerformanceServiceTest : MonoBehaviour
    {
        private RuntimePerformanceService _service;

        private void Start()
        {
            RuntimeDeveloperToolkitRuntime runtime =
                RuntimeDeveloperToolkitRuntime.Instance;

            if (runtime == null)
            {
                Debug.LogError(
                    "[Performance Test] Runtime not found.");

                return;
            }

            if (!runtime.Services.TryGet(
                    "performance",
                    out _service))
            {
                Debug.LogError(
                    "[Performance Test] " +
                    "Performance service not found.");

                return;
            }

            Debug.Log(
                "[Performance Test] " +
                $"Initialized: {_service.IsInitialized}");
        }

        private void Update()
        {
            if (_service == null)
            {
                return;
            }

            RuntimePerformanceData data =
                _service.CurrentData;

            Debug.Log(
                $"[Performance Test] " +
                $"FPS: {data.FPS:F1} | " +
                $"Frame Time: " +
                $"{data.FrameTimeMilliseconds:F2} ms | " +
                $"Average: {data.AverageFPS:F1}");
        }
    }
}