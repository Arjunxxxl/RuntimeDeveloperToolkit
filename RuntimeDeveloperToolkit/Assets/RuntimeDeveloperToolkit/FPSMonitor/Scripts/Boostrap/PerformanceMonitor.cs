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
        
        private int HistorySize = 300;
        private readonly float StartDelay = 0.1f;
        private readonly float Delay_RenderingStats = 1.0f;

        public static PerformanceMonitor Instance;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if(Instance != this)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
            
            fpsCalculator = new FPSCalculator();
            fpsHistory = new FPSHistory();
            systemStatsCalculator = new SystemStatsCalculator();
            appStatsCalculator = new AppStatsCalculator();
            renderingInfoCalculator = GetComponentInChildren<RenderingInfoCalculator>();
            performanceMonitorCanvas = GetComponentInChildren<PerformanceMonitorCanvas>();

            if (HistorySize < 50)
            {
                HistorySize = 50;
            }
        }

        private void Start()
        {
            MonitorRunning = false;

            fpsCalculator.SetUp(fpsRate);
            float fpsCalcDelay = fpsCalculator.GetFpsCalcDelay();
            
            systemStatsCalculator.SetUp();
            appStatsCalculator.SetUp();
            performanceMonitorCanvas.SetUp(fpsCalcDelay);
            StartCoroutine(StartFpsMonitor());
        }

        private IEnumerator StartFpsMonitor()
        {
            yield return new WaitForSecondsRealtime(StartDelay);
            
            fpsHistory.SetUp(HistorySize);
            renderingInfoCalculator.SetUp();
            
            SystemStats systemStats = systemStatsCalculator.GetSystemStats();
            performanceMonitorCanvas.UpdateSystemStats(systemStats);
            
            AppStats appStats = appStatsCalculator.GetAppStats();
            performanceMonitorCanvas.UpdateAppStats(appStats);
            
            LastCaptureTime_RenderingStats = Time.unscaledTime;
            
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
                int historySize = fpsHistory.GetHistorySize();
                int writeIndex = fpsHistory.GetWriteIndex();
                int snapshotCount = fpsHistory.GetSnapshotCount();
                performanceMonitorCanvas.UpdateGraph(history, fpsStats, historySize, writeIndex, snapshotCount);
            }
        }

        private void Monitor_RenderingStats()
        {
            if (Time.unscaledTime > LastCaptureTime_RenderingStats + Delay_RenderingStats)
            {
                RendererStats rendererStats = renderingInfoCalculator.Calculate();
                performanceMonitorCanvas.UpdateRenderingStats(rendererStats);
                
                LastCaptureTime_RenderingStats = Time.unscaledTime; 
            }
        }
    }
}