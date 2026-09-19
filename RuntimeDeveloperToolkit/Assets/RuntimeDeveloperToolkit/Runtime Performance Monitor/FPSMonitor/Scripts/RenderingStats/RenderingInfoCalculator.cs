using UnityEngine;
using Unity.Profiling;

namespace RuntimePerformanceMonitor
{
    public class RenderingInfoCalculator : MonoBehaviour
    {
        private RendererStats rendererStats;

        private ProfilerRecorder drawCalls;
        private ProfilerRecorder setPassCalls;
        private ProfilerRecorder batches;
        private ProfilerRecorder triangles;
        private ProfilerRecorder vertices;
        private ProfilerRecorder shadowCasters;

        #region Unity Functions

        private void OnEnable()
        {
            drawCalls = ProfilerRecorder.StartNew(
                ProfilerCategory.Render,
                "Draw Calls Count"
            );

            setPassCalls = ProfilerRecorder.StartNew(
                ProfilerCategory.Render,
                "SetPass Calls Count"
            );

            batches = ProfilerRecorder.StartNew(
                ProfilerCategory.Render,
                "Total Batches Count"
            );

            triangles = ProfilerRecorder.StartNew(
                ProfilerCategory.Render,
                "Triangles Count"
            );

            vertices = ProfilerRecorder.StartNew(
                ProfilerCategory.Render,
                "Vertices Count"
            );

            shadowCasters = ProfilerRecorder.StartNew(
                ProfilerCategory.Render,
                "Shadow Casters Count"
            );
        }

        private void OnDisable()
        {
            drawCalls.Dispose();
            setPassCalls.Dispose();
            batches.Dispose();
            triangles.Dispose();
            vertices.Dispose();
            shadowCasters.Dispose();
        }

        #endregion

        #region SetUp

        internal void SetUp()
        {
            rendererStats = new RendererStats();
        }

        #endregion

        #region Calculations

        internal RendererStats Calculate()
        {
            rendererStats.drawCallCount = (int) drawCalls.LastValue;
            rendererStats.setPassCallCount = (int) setPassCalls.LastValue;
            rendererStats.batchesCount = (int) batches.LastValue;
            rendererStats.trianglesCount = (int) triangles.LastValue;
            rendererStats.verticesCount = (int) vertices.LastValue;
            rendererStats.shadowCasterCount = (int) shadowCasters.LastValue;

            rendererStats.camerasCount = Camera.allCamerasCount;
            rendererStats.lightsCount = FindObjectsByType<Light>(FindObjectsSortMode.None).Length;
            rendererStats.renderersCount = FindObjectsByType<Renderer>(FindObjectsSortMode.None).Length;
            rendererStats.skinnedMeshesCount = FindObjectsByType<SkinnedMeshRenderer>(FindObjectsSortMode.None).Length;

            return rendererStats;
        }

        #endregion
    }
}