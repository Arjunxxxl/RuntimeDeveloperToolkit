using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class AppStatsCalculator
    {
        // App Stats
        private AppStats appStats;

        #region Constructor

        internal AppStatsCalculator()
        {
            
        }

        #endregion

        #region SetUp

        internal void SetUp()
        {
            appStats = new AppStats();
            Calculate();
        }

        #endregion

        #region Data Calculation

        private void Calculate()
        {
            appStats.appName = Application.productName;
            appStats.appVersion = Application.version;

            appStats.unityVersion = Application.unityVersion;
            appStats.targetPlatform = GetPlatformName();

            #if ENABLE_IL2CPP
                appStats.scriptingBackend = "IL2CPP";
            #elif ENABLE_MONO
                appStats.scriptingBackend = "Mono";
            #else
                appStats.scriptingBackend = "Unknown";
            #endif

            // Runtime
            appStats.isDevelopmentBuild = Debug.isDebugBuild;

            // Quality
            appStats.qualityLevel = GetQualityLevel();
            appStats.colorSpace = QualitySettings.activeColorSpace.ToString();
            appStats.vSyncCount = QualitySettings.vSyncCount;
            appStats.targetFrameRate = Application.targetFrameRate;
        }
        
        private string GetPlatformName()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    return "Android";

                case RuntimePlatform.IPhonePlayer:
                    return "iOS";

                case RuntimePlatform.WindowsPlayer:
                    return "Windows";

                case RuntimePlatform.OSXPlayer:
                    return "macOS";

                case RuntimePlatform.LinuxPlayer:
                    return "Linux";

                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";

                case RuntimePlatform.PS5:
                    return "PlayStation 5";

                case RuntimePlatform.XboxOne:
                    return "Xbox";

                default:
                    return Application.platform.ToString();
            }
        }
        
        private string GetQualityLevel()
        {
            int index = QualitySettings.GetQualityLevel();

            if (index < 0 || index >= QualitySettings.names.Length)
                return "Unknown";

            return QualitySettings.names[index];
        }

        #endregion

        #region Getter

        internal AppStats GetAppStats()
        {
            return appStats;
        }

        #endregion
    }
}