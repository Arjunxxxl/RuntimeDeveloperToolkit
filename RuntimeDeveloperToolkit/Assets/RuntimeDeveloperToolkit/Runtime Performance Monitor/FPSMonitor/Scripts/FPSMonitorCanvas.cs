using RuntimePerformanceMonitor;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class FPSMonitorCanvas : MonoBehaviour
    {
        private FPSView fpsView;

        #region SetUp

        internal void SetUp()
        {
            fpsView = GetComponentInChildren<FPSView>();
            fpsView.SetUp();
        }

        #endregion

        #region Fps View

        internal void UpdateFpsView(FPSSnapshot fpsSnapshot, FPSStats fpsStats)
        {
            fpsView.UpdateText(fpsSnapshot, fpsStats);
        }

        #endregion
    }
}