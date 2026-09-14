using UnityEngine;
using RuntimeDeveloperToolkit.Performance.Data;

namespace RuntimeDeveloperToolkit.Performance.Tests
{
    public sealed class RuntimePerformanceHistoryTest : MonoBehaviour
    {
        private void Start()
        {
            RuntimePerformanceHistory history =
                new RuntimePerformanceHistory(5);

            history.Add(60f, 16.67f);
            history.Add(61f, 16.39f);
            history.Add(59f, 16.95f);
            history.Add(60f, 16.67f);
            history.Add(58f, 17.24f);

            PrintHistory(history);

            history.Add(57f, 17.54f);

            Debug.Log(
                "[Performance History Test] " +
                "After exceeding capacity:");

            PrintHistory(history);

            history.Clear();

            Debug.Log(
                $"[Performance History Test] " +
                $"After Clear - Count: {history.Count}");
        }

        private void PrintHistory(
            RuntimePerformanceHistory history)
        {
            for (int i = 0; i < history.Count; i++)
            {
                Debug.Log(
                    $"[{i}] " +
                    $"FPS: {history.GetFPS(i):F2}, " +
                    $"Frame Time: " +
                    $"{history.GetFrameTimeMilliseconds(i):F2} ms");
            }
        }
    }
}