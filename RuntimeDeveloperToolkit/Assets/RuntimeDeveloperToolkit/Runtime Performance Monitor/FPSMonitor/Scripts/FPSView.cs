using TMPro;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class FPSView : MonoBehaviour
    {
        public TMP_Text fpsText;
        public TMP_Text frameTimeText;
        public TMP_Text maxFpsText;
        public TMP_Text minFpsText;
        public TMP_Text averageFpsText;
        public TMP_Text low1PerFpsText;
        public TMP_Text low10PerFpsText;

        private readonly string PlaceHolderStr = "--";
        private readonly string MSStr = " ms";
        
        #region SetUp

        internal void SetUp()
        {
            fpsText.text = PlaceHolderStr;
            frameTimeText.text = PlaceHolderStr;
            
            averageFpsText.text = PlaceHolderStr;
            maxFpsText.text = PlaceHolderStr;
            minFpsText.text = PlaceHolderStr;
            low1PerFpsText.text = PlaceHolderStr;
            low10PerFpsText.text = PlaceHolderStr;
        }

        #endregion
        
        internal void UpdateText(FPSSnapshot fpsSnapshot, FPSStats fpsStats)
        {
            fpsText.text = fpsSnapshot.FPS.ToString("N1");
            frameTimeText.text = fpsSnapshot.FrameTime.ToString("N3") + MSStr;
            
            averageFpsText.text = fpsStats.AverageFps.ToString("N1");
            maxFpsText.text = fpsStats.MaxFps.ToString("N1");
            minFpsText.text = fpsStats.MinFps.ToString("N1");
            low1PerFpsText.text = fpsStats.FPSLow1Per.ToString("N1");
            low10PerFpsText.text = fpsStats.FPSLow10Per.ToString("N1");
        }
    }
}