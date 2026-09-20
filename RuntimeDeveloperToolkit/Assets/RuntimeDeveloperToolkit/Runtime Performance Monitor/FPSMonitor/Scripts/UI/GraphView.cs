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

        private float minValueMul = 0.75f;
        private float maxValueMul = 1.25f;
        
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
        }
    }
}