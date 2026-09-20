using TMPro;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class GraphView : MonoBehaviour
    {
        public Graph graph;
        public TMP_Text minValTxt;
        public TMP_Text maxValTxt;
        public TMP_Text midValTxt;

        private float graphNextUpdateTime = 0.0f;
        private float graphUpdateDelay = 0.0f;

        private readonly float minValueMul = 0.75f;
        private readonly float maxValueMul = 1.25f;
        private readonly float graphUpdateDelayOffset = 0.75f;

        internal void SetUp(float fpsCalcDelay)
        {
            graphUpdateDelay = fpsCalcDelay + graphUpdateDelayOffset;
            graphNextUpdateTime = Time.unscaledTime + graphUpdateDelay;
        }
        
        internal void UpdateGraph(FPSSnapshot[] snapshotHistory,
            FPSStats fpsStats,
            int historySize,
            int writeIndex,
            int snapshotCount,
            bool showFPS,
            bool showFrameTime,
            int decimalPt,
            string suffix)
        {
            if (Time.unscaledTime < graphNextUpdateTime)
            {
                return;
            }
            
            float minVal = 0;
            float maxVal = 0;
            if (showFPS)
            {
                minVal = fpsStats.MinFps;
                maxVal = fpsStats.MaxFps;
            }
            if (showFrameTime)
            {
                minVal = fpsStats.MinFrameTime * 1000f;
                maxVal = fpsStats.MaxFrameTime * 1000f;
            }
            
            minValTxt.text = (minVal * minValueMul).ToString("N" + decimalPt) + suffix;
            maxValTxt.text = (minVal * maxValueMul).ToString("N" + decimalPt) + suffix;
            midValTxt.text = ((minVal * minValueMul) + (((maxVal * maxValueMul) - (minVal * minValueMul)) / 2.0f)).ToString("N" + decimalPt) + suffix;
            graph.SetData(snapshotHistory, historySize, writeIndex, snapshotCount, showFPS, showFrameTime, minVal * minValueMul, maxVal * maxValueMul);
            
            graphNextUpdateTime = Time.unscaledTime + graphUpdateDelay;
        }
    }
}