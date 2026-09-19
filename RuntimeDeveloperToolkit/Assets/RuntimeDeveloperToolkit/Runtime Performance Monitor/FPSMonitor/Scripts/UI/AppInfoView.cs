using TMPro;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class AppInfoView : MonoBehaviour
    {
        [Header("UI Elements")]
        public TMP_Text appNameTxt;
        public TMP_Text appVersionTxt;
        public TMP_Text unityVersionTxt;
        public TMP_Text scriptingBackendTxt;
        public TMP_Text targetPlatformTxt;
        public TMP_Text developmentBuildTxt;
        public TMP_Text qualityLevelTxt;
        public TMP_Text colorSpaceTxt;
        public TMP_Text vSyncCountTxt;
        public TMP_Text targetFrameRateTxt;
        
        private readonly string PlaceHolderStr = "--";

        internal void SetUp()
        {
            appNameTxt.text = PlaceHolderStr;
            appVersionTxt.text = PlaceHolderStr;
            unityVersionTxt.text = PlaceHolderStr;
            scriptingBackendTxt.text = PlaceHolderStr;
            targetPlatformTxt.text = PlaceHolderStr;
            developmentBuildTxt.text = PlaceHolderStr;
            qualityLevelTxt.text = PlaceHolderStr;
            colorSpaceTxt.text = PlaceHolderStr;
            vSyncCountTxt.text = PlaceHolderStr;
            targetFrameRateTxt.text = PlaceHolderStr;
        }

        internal void UpdateDataInUi(AppStats appStats)
        {
            appNameTxt.text = appStats.appName;
            appVersionTxt.text = appStats.appVersion;
            unityVersionTxt.text = appStats.unityVersion;
            scriptingBackendTxt.text = appStats.scriptingBackend;
            targetPlatformTxt.text = appStats.targetPlatform;
            developmentBuildTxt.text = appStats.isDevelopmentBuild ? "Yes" : "No";
            qualityLevelTxt.text = appStats.qualityLevel;
            colorSpaceTxt.text = appStats.colorSpace;
            targetFrameRateTxt.text = appStats.targetFrameRate.ToString();

            if (appStats.vSyncCount == 0)
            {
                vSyncCountTxt.text = "Off";
            }
            else if (appStats.vSyncCount == 1)
            {
                vSyncCountTxt.text = "VSync every frame";
            }
            else if (appStats.vSyncCount == 2)
            {
                vSyncCountTxt.text = "VSync every 2 frames";
            }
        }
    }
}