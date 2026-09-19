using System.Collections;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class PerformanceMonitor : MonoBehaviour
    {
        public FPSRate fpsRate = FPSRate.Hz_25;

        private bool MonitorRunning = false;
        
        private FPSCalculator fpsCalculator;
        private FPSHistory fpsHistory;
        private RenderingInfoCalculator renderingInfoCalculator;
        private SystemStatsCalculator systemStatsCalculator;
        private AppStatsCalculator appStatsCalculator;
        private PerformanceMonitorCanvas performanceMonitorCanvas;

        private float LastCaptureTime_RenderingStats = 0.0f;
        
        private readonly int HistorySize = 100;
        private readonly float StartDelay = 0.1f;
        private readonly float Delay_RenderingStats = 1.0f;
        
        private void Awake()
        {
            fpsCalculator = GetComponentInChildren<FPSCalculator>();
            fpsHistory = GetComponentInChildren<FPSHistory>();
            renderingInfoCalculator = GetComponentInChildren<RenderingInfoCalculator>();
            systemStatsCalculator = GetComponentInChildren<SystemStatsCalculator>();
            appStatsCalculator = GetComponentInChildren<AppStatsCalculator>();
            performanceMonitorCanvas = GetComponentInChildren<PerformanceMonitorCanvas>();
        }

        private void Start()
        {
            MonitorRunning = false;

            systemStatsCalculator.SetUp();
            appStatsCalculator.SetUp();
            performanceMonitorCanvas.SetUp();
            StartCoroutine(StartFpsMonitor());
        }

        private IEnumerator StartFpsMonitor()
        {
            yield return new WaitForSecondsRealtime(StartDelay);
            
            fpsCalculator.SetUp(fpsRate);
            fpsHistory.SetUp(HistorySize);
            renderingInfoCalculator.SetUp();
            
            SystemStats systemStats = systemStatsCalculator.GetSystemStats();
            performanceMonitorCanvas.UpdateSystemStats(systemStats);
            
            AppStats appStats = appStatsCalculator.GetAppStats();
            performanceMonitorCanvas.UpdateAppStats(appStats);
            
            LastCaptureTime_RenderingStats = Time.unscaledTime + Delay_RenderingStats;
            
            MonitorRunning = true;
        }

        private void Update()
        {
            if (MonitorRunning)
            {
                Monitor_FPS();
                Monitor_RenderingStats();
            }
        }

        private void Monitor_FPS()
        {
            (FPSSnapshot, bool) fpsSnapshotRes = fpsCalculator.CalcFPS(Time.unscaledDeltaTime);
            FPSSnapshot fpsSnapshot = fpsSnapshotRes.Item1;
            bool isSuccess = fpsSnapshotRes.Item2;

            if (isSuccess)
            {
                FPSStats fpsStats = fpsHistory.AddFPSSnapshot(fpsSnapshot);
                performanceMonitorCanvas.UpdateFpsView(fpsSnapshot, fpsStats);

                FPSSnapshot[] history = fpsHistory.GetFPSSnapShotHistory();
                float[] fpsDataPts = new float[history.Length];
                float[] frameTimeDataPts = new float[history.Length];
                for (int idx = 0; idx < history.Length; idx++)
                {
                    fpsDataPts[idx] = history[idx].FPS;
                    frameTimeDataPts[idx] = history[idx].FrameTime;
                }
                performanceMonitorCanvas.UpdateGraph(fpsDataPts, frameTimeDataPts, fpsStats);
            }
        }

        private void Monitor_RenderingStats()
        {
            if (Time.unscaledTime > LastCaptureTime_RenderingStats + Delay_RenderingStats)
            {
                RendererStats rendererStats = renderingInfoCalculator.Calculate();
                performanceMonitorCanvas.UpdateRenderingStats(rendererStats);
                
                LastCaptureTime_RenderingStats = Time.unscaledTime + Delay_RenderingStats;
            }
        }
    }
}