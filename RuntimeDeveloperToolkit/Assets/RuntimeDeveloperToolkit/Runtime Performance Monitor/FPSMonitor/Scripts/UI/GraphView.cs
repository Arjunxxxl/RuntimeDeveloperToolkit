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
        
        internal void UpdateGraph(float[] data, float minVal, float maxVal, int decimalPt, string suffix)
        {
            minValTxt.text = (minVal * minValueMul).ToString("N" + decimalPt) + suffix;
            maxValTxt.text = (minVal * maxValueMul).ToString("N" + decimalPt) + suffix;
            midValTxt.text = ((minVal * minValueMul) + (((maxVal * maxValueMul) - (minVal * minValueMul)) / 2.0f)).ToString("N" + decimalPt) + suffix;
            graph.SetData(data, minVal * minValueMul, maxVal * maxValueMul);
        }
    }
}