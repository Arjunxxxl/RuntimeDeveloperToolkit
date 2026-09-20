using System.Collections.Generic;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class FPSHistory : MonoBehaviour
    {
        private FPSSnapshot[] fpsSnapshotHistory;
        private FPSSnapshot[] sortedFpsSnapshotHistory;
          
        private int historySize;
        private int writeIndex = 0;
        private int snapshotCount = 0;
        
        // FPS Stats Data
        private FPSStats curFpsStats;

        #region Set Up

        internal void SetUp(int _historySize)
        {
            historySize = _historySize;
            fpsSnapshotHistory = new FPSSnapshot[_historySize];
            sortedFpsSnapshotHistory = new FPSSnapshot[_historySize];
            writeIndex = 0;
            snapshotCount = 0;
        }

        #endregion

        #region Adding Snapshot

        internal FPSStats AddFPSSnapshot(FPSSnapshot fpsSnapshot)
        {
            fpsSnapshotHistory[writeIndex] = fpsSnapshot;
            writeIndex++;

            if (writeIndex >= historySize)
            {
                writeIndex = 0;
            }

            if (snapshotCount < historySize)
            {
                snapshotCount++;
            }

            CalcFpsStats();

            return curFpsStats;
        }

        #endregion

        #region Calc Stats

        private void CalcFpsStats()
        {
            if (snapshotCount == 0)
            {
                return;
            }
            
            float minFps = float.MaxValue;
            float maxFps = float.MinValue;
            float minFrameTime = float.MaxValue;
            float maxFrameTime = float.MinValue;
            float totalFrameTime = 0;
            
            int totalSnapShots = snapshotCount;
            sortedFpsSnapshotHistory = new FPSSnapshot[totalSnapShots];
            
            for (int idx = 0; idx < totalSnapShots; idx++)
            {
                int index = (writeIndex - totalSnapShots + idx + historySize)
                            % historySize;
                
                FPSSnapshot fpsSnapshot = fpsSnapshotHistory[index];
                sortedFpsSnapshotHistory[idx] = fpsSnapshot;
                
                totalFrameTime += fpsSnapshot.FrameTime;

                if (fpsSnapshot.FPS < minFps)
                {
                    minFps = fpsSnapshot.FPS;
                }

                if (fpsSnapshot.FPS > maxFps)
                {
                    maxFps = fpsSnapshot.FPS;
                }

                if (fpsSnapshot.FrameTime < minFrameTime)
                {
                    minFrameTime = fpsSnapshot.FrameTime;
                }

                if (fpsSnapshot.FrameTime > maxFrameTime)
                {
                    maxFrameTime = fpsSnapshot.FrameTime;
                }
            }
            
            float averageFPS = totalFrameTime > 0f ? totalSnapShots / totalFrameTime : 0f;
            
            System.Array.Sort(
                sortedFpsSnapshotHistory,
                0,
                totalSnapShots,
                Comparer<FPSSnapshot>.Create(
                    (a, b) => b.FrameTime.CompareTo(a.FrameTime)
                )
            );
            
            int countLow1Per = Mathf.Max(1, Mathf.CeilToInt(totalSnapShots * 0.01f));
            int countLow10Per = Mathf.Max(1, Mathf.CeilToInt(totalSnapShots * 0.10f));

            countLow1Per = Mathf.Min(countLow1Per, totalSnapShots);
            countLow10Per = Mathf.Min(countLow10Per, totalSnapShots);
            
            float averageFrameTimeLow1Per = 0.0f;
            float averageFrameTimeLow10Per = 0.0f;

            for (int idx = 0; idx < countLow10Per; idx++)
            {
                float frameTime = sortedFpsSnapshotHistory[idx].FrameTime;

                averageFrameTimeLow10Per += frameTime;

                if (idx < countLow1Per)
                {
                    averageFrameTimeLow1Per += frameTime;
                }
            }
            
            float averageFPSLow1Per = averageFrameTimeLow1Per > 0f ? countLow1Per / averageFrameTimeLow1Per : 0f;
            float averageFPSLow10Per = averageFrameTimeLow10Per > 0f ? countLow10Per / averageFrameTimeLow10Per : 0f;

            curFpsStats.AverageFps = averageFPS;
            curFpsStats.MaxFps = maxFps;
            curFpsStats.MinFps = minFps;
            curFpsStats.MinFrameTime = minFrameTime;
            curFpsStats.MaxFrameTime = maxFrameTime;
            curFpsStats.FPSLow1Per = averageFPSLow1Per;
            curFpsStats.FPSLow10Per = averageFPSLow10Per;
        }

        #endregion

        #region Getter

        internal FPSStats GetFPSStats() => curFpsStats;
        internal FPSSnapshot[] GetFPSSnapShotHistory() => fpsSnapshotHistory;
        internal int GetHistorySize() => historySize;
        internal int GetWriteIndex() => writeIndex;
        internal int GetSnapshotCount() => snapshotCount;

        #endregion
    }
}