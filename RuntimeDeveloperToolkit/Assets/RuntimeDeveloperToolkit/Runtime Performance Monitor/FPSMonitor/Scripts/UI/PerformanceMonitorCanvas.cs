using System;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimePerformanceMonitor
{
    public enum MonitorTabs
    {
        Unknown = -1,
        Graph,
        GPU,
        System,
        App
    }
    
    public class PerformanceMonitorCanvas : MonoBehaviour
    {
        [Header("Container")]
        public GameObject containerGo;
        
        [Header("Visibility Button")]
        public Image visibilityBtmImg;
        public Sprite spriteVisible;
        public Sprite spriteHidden;
        
        [Header("Tabs")] 
        public MonitorCanvasTabButton buttonGraph;
        public MonitorCanvasTabButton buttonGpu;
        public MonitorCanvasTabButton buttonSystem;
        public MonitorCanvasTabButton buttonApp;

        [Header("Tab Content Canvases")]
        public Canvas canvasGraph;
        public Canvas canvasGpu;
        public Canvas canvasSystem;
        public Canvas canvasApp;
        
        [Header("Graphs")]
        public GraphView fpsGraph;
        public GraphView frameTimeGraph;
        
        private MonitorTabs activeTab = MonitorTabs.Unknown;
        private MonitorTabs previousActiveTab = MonitorTabs.Unknown;
        private bool isVisible = true;
        
        private FPSView fpsView;
        private RenderingInfoView renderingInfoView;
        private SystemInfoView systemInfoView;
        private AppInfoView appInfoView;

        internal static Action<MonitorTabs> OnClickTab;

        #region Unity Functions

        private void OnEnable()
        {
            OnClickTab += OnClickTabPreformed;
        }

        private void OnDisable()
        {
            OnClickTab -= OnClickTabPreformed;
        }

        #endregion
        
        #region SetUp

        internal void SetUp()
        {
            fpsView = GetComponentInChildren<FPSView>();
            renderingInfoView = GetComponentInChildren<RenderingInfoView>();
            systemInfoView = GetComponentInChildren<SystemInfoView>();
            appInfoView = GetComponentInChildren<AppInfoView>();
            
            fpsView.SetUp();
            renderingInfoView.SetUp();
            systemInfoView.SetUp();
            appInfoView.SetUp();

            activeTab = MonitorTabs.Unknown;
            previousActiveTab = MonitorTabs.Unknown;
            DisableAllTabs();
            
            isVisible = true;
            SetVisibility();
            
            OnClickTab?.Invoke(MonitorTabs.Graph);
        }

        #endregion

        #region Visibility

        public void OnClickVisibilityButton()
        {
            isVisible = !isVisible;
            SetVisibility();
        }

        private void SetVisibility()
        {
            containerGo.SetActive(isVisible);
            visibilityBtmImg.sprite = isVisible ? spriteHidden : spriteVisible;
        }

        #endregion
        
        #region Fps View

        internal void UpdateFpsView(FPSSnapshot fpsSnapshot, FPSStats fpsStats)
        {
            fpsView.UpdateText(fpsSnapshot, fpsStats);
        }

        #endregion
        
        #region Tab

        private void OnClickTabPreformed(MonitorTabs tabClicked)
        {
            ActivateTab(tabClicked);
            DeactivatePreviousTab();
        }

        private void ActivateTab(MonitorTabs tab)
        {
            if (tab == activeTab || tab == MonitorTabs.Unknown)
            {
                return;
            }

            previousActiveTab = activeTab;
            activeTab = tab;
            
            switch (tab)
            {
                case MonitorTabs.Graph:
                    buttonGraph.SetButtonState(true);
                    canvasGraph.enabled = true;
                    break;
                case MonitorTabs.GPU:
                    buttonGpu.SetButtonState(true);
                    canvasGpu.enabled = true;
                    break;
                case MonitorTabs.System:
                    buttonSystem.SetButtonState(true);
                    canvasSystem.enabled = true;
                    break;
                case MonitorTabs.App:
                    buttonApp.SetButtonState(true);
                    canvasApp.enabled = true;
                    break;
            }
        }

        private void DeactivatePreviousTab()
        {
            if (previousActiveTab == MonitorTabs.Unknown)
            {
                return;
            }
            
            switch (previousActiveTab)
            {
                case MonitorTabs.Graph:
                    buttonGraph.SetButtonState(false);
                    canvasGraph.enabled = false;
                    break;
                case MonitorTabs.GPU:
                    buttonGpu.SetButtonState(false);
                    canvasGpu.enabled = false;
                    break;
                case MonitorTabs.System:
                    buttonSystem.SetButtonState(false);
                    canvasSystem.enabled = false;
                    break;
                case MonitorTabs.App:
                    buttonApp.SetButtonState(false);
                    canvasApp.enabled = false;
                    break;
            }
        }

        private void DisableAllTabs()
        {
            canvasGraph.enabled = false;
            canvasGpu.enabled = false;
            canvasSystem.enabled = false;
            canvasApp.enabled = false;
        }
        
        #endregion
        
        #region Graph

        internal void UpdateGraph(float[] fpsDataPts, float[] frameTimeDataPts, FPSStats fpsStats)
        {
            fpsGraph.UpdateGraph(fpsDataPts, fpsStats.MinFps, fpsStats.MaxFps, 0, "");
            frameTimeGraph.UpdateGraph(frameTimeDataPts, fpsStats.MinFrameTime, fpsStats.MaxFrameTime, 4, "\nms");
        }

        #endregion

        #region Rendering Stats

        internal void UpdateRenderingStats(RendererStats rendererStats)
        {
            renderingInfoView.UpdateDataInUi(rendererStats);
        }

        #endregion

        #region System Stats

        internal void UpdateSystemStats(SystemStats systemStats)
        {
            systemInfoView.UpdateDataInUi(systemStats);
        }

        #endregion

        #region App Stats

        internal void UpdateAppStats(AppStats appStats)
        {
            appInfoView.UpdateDataInUi(appStats);
        }

        #endregion
    }
}