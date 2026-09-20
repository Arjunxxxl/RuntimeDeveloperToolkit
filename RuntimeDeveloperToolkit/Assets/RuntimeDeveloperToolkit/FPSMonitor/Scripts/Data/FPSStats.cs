namespace RuntimePerformanceMonitor
{
    [System.Serializable]
    public struct FPSStats
    {
        public float MaxFps;
        public float MinFps;
        public float MaxFrameTime;
        public float MinFrameTime;
        public float AverageFps;
        public float FPSLow1Per;
        public float FPSLow10Per;
    }
}