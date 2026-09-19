namespace RuntimePerformanceMonitor
{
    public struct AppStats
    {
        public string appName;
        public string appVersion;

        public string unityVersion;
        public string scriptingBackend;
        
        public string targetPlatform;
        public bool isDevelopmentBuild;

        public string qualityLevel;
        public string colorSpace;
        public int vSyncCount;
        public int targetFrameRate;
    }
}