using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class FPSHistory : MonoBehaviour
    {
        private FPSSnapshot[] fpsSnapshotHistory;
        private FPSSnapshot[] sortedFpsSnapshotHistory;
          
        private int historySize;
        private int curSnapshotCount = 0;
        
        // FPS Stats Data
        private FPSStats curFpsStats;

        #region Set Up

        internal void SetUp(int _historySize)
        {
            historySize = _historySize;
            fpsSnapshotHistory = new FPSSnapshot[_historySize];
            sortedFpsSnapshotHistory = new FPSSnapshot[_historySize];
            curSnapshotCount = 0;
        }

        #endregion

        #region Adding Snapshot

        internal FPSStats AddFPSSnapshot(FPSSnapshot fpsSnapshot)
        {
            if (curSnapshotCount < historySize)
            {
                fpsSnapshotHistory[curSnapshotCount] = fpsSnapshot;
                curSnapshotCount++;
            }
            else
            {
                for (int idx = 0; idx < fpsSnapshotHistory.Length - 1; idx++)
                {
                    fpsSnapshotHistory[idx] = fpsSnapshotHistory[idx + 1];
                }
                fpsSnapshotHistory[historySize - 1] = fpsSnapshot;
            }
            
            SortFPSSnapShotHistory();
            CalcFpsStats();
            
            return curFpsStats;
        }

        private void SortFPSSnapShotHistory()
        {
            int totalSnapShots = curSnapshotCount;

            System.Array.Copy(
                fpsSnapshotHistory,
                sortedFpsSnapshotHistory,
                totalSnapShots
            );
            
            sortedFpsSnapshotHistory = RDT_Sort.Sort(sortedFpsSnapshotHistory, 0, totalSnapShots - 1);
            
        }

        #endregion

        #region Calc Stats

        private void CalcFpsStats()
        {
            if (curSnapshotCount == 0)
            {
                return;
            }
            
            float minFps = float.MaxValue;
            float maxFps = float.MinValue;
            float totalFrameTime = 0;
            
            int totalSnapShots = curSnapshotCount;
            
            for (int idx = 0; idx < totalSnapShots; idx++)
            {
                FPSSnapshot fpsSnapshot = fpsSnapshotHistory[idx];
                totalFrameTime += fpsSnapshot.FrameTime;

                if (fpsSnapshot.FPS < minFps)
                {
                    minFps = fpsSnapshot.FPS;
                }

                if (fpsSnapshot.FPS > maxFps)
                {
                    maxFps = fpsSnapshot.FPS;
                }
            }
            
            float averageFPS = totalSnapShots / totalFrameTime;
            
            int countLow1Per = Mathf.CeilToInt(totalSnapShots * 0.01f);
            int countLow10Per = Mathf.CeilToInt(totalSnapShots * 0.1f);
            
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
            
            float averageFPSLow1Per = countLow1Per / averageFrameTimeLow1Per;
            float averageFPSLow10Per = countLow10Per / averageFrameTimeLow10Per;

            curFpsStats.AverageFps = averageFPS;
            curFpsStats.MaxFps = maxFps;
            curFpsStats.MinFps = minFps;
            curFpsStats.FPSLow1Per = averageFPSLow1Per;
            curFpsStats.FPSLow10Per = averageFPSLow10Per;
        }

        #endregion

        #region Getter

        internal FPSStats GetFPSStats() => curFpsStats;
        internal FPSSnapshot[] GetFPSSnapShotHistory() => fpsSnapshotHistory;

        #endregion
    }
}