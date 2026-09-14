using UnityEngine;
using RuntimeDeveloperToolkit.Performance.Services;

namespace RuntimeDeveloperToolkit.Performance.Tests
{
    public sealed class RuntimePerformanceSamplerTest : MonoBehaviour
    {
        private RuntimePerformanceSampler _sampler;

        private void Start()
        {
            _sampler = new RuntimePerformanceSampler();

            TestSample();
            TestReset();
        }

        private void TestSample()
        {
            _sampler.Sample(1f / 60f);
            _sampler.Sample(1f / 61f);
            _sampler.Sample(1f / 62f);
            _sampler.Sample(1f / 63f);

            Debug.Log(
                $"[Performance Test] " +
                $"FPS: {_sampler.FPS:F2}, " +
                $"Frame Time: {_sampler.FrameTimeMilliseconds:F2} ms, " +
                $"Average: {_sampler.AverageFPS:F2}, " +
                $"Min: {_sampler.MinFPS:F2}, " +
                $"Max: {_sampler.MaxFPS:F2}");
            
            Debug.Log(
                $"History Count: {_sampler.History.Count}");

            for (int i = 0; i < _sampler.History.Count; i++)
            {
                Debug.Log(
                    $"History[{i}] = " +
                    $"{_sampler.History.GetFPS(i):F2} FPS");
            }
        }

        private void TestReset()
        {
            _sampler.Reset();

            Debug.Log(
                $"[Performance Test] " +
                $"After Reset - FPS: {_sampler.FPS}, " +
                $"Average: {_sampler.AverageFPS}, " +
                $"Min: {_sampler.MinFPS}, " +
                $"Max: {_sampler.MaxFPS}");
        }
    }
}