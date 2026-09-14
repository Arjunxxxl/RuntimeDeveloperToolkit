using System;
using System.Collections;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class FPSMonitor : MonoBehaviour
    {
        public FPSRate fpsRate = FPSRate.EveryFrame;

        private bool FpsMonitorRunning = false;
        
        private FPSCalculator fpsCalculator;
        private FPSHistory fpsHistory;
        private FPSMonitorCanvas fpsMonitorCanvas;
        private Graph fpsGraph;

        private readonly int HistorySize = 100;
        private readonly float StartDelay = 0.1f;
        
        private void Awake()
        {
            fpsCalculator = GetComponentInChildren<FPSCalculator>();
            fpsHistory = GetComponentInChildren<FPSHistory>();
            fpsMonitorCanvas = GetComponentInChildren<FPSMonitorCanvas>();
            fpsGraph = GetComponentInChildren<Graph>();
        }

        private void Start()
        {
            FpsMonitorRunning = false;

            fpsMonitorCanvas.SetUp();
            StartCoroutine(StartFpsMonitor());
        }

        private void Update()
        {
            if (FpsMonitorRunning)
            {
                (FPSSnapshot, bool) fpsSnapshotRes = fpsCalculator.CalcFPS(Time.unscaledDeltaTime);
                FPSSnapshot fpsSnapshot = fpsSnapshotRes.Item1;
                bool isSuccess = fpsSnapshotRes.Item2;

                if (isSuccess)
                {
                    FPSStats fpsStats = fpsHistory.AddFPSSnapshot(fpsSnapshot);
                    fpsMonitorCanvas.UpdateFpsView(fpsSnapshot, fpsStats);

                    FPSSnapshot[] history = fpsHistory.GetFPSSnapShotHistory();
                    float[] dataPts = new float[history.Length];
                    for (int idx = 0; idx < history.Length; idx++)
                    {
                        dataPts[idx] = history[idx].FPS;
                    }
                    fpsGraph.SetData(dataPts, fpsStats.MinFps, fpsStats.MaxFps);
                }
            }
        }

        private IEnumerator StartFpsMonitor()
        {
            yield return new WaitForSecondsRealtime(StartDelay);
            
            fpsCalculator.SetUp(fpsRate);
            fpsHistory.SetUp(HistorySize);
            
            FpsMonitorRunning = true;
        }
    }
}