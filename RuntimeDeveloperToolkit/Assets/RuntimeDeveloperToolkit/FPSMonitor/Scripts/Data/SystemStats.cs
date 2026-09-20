using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public struct SystemStats
    {
        // Device
        public string deviceModel;
        public DeviceType deviceType;
        public string operatingSystem;

        // CPU
        public string processorName;
        public int processorCount;
        public int processorFrequency;

        // GPU
        public string gpuName;
        public string gpuVendor;
        public string graphicAPI;
        public string graphicsDeviceVersion;

        // Memory
        public int gpuMemory;
        public int systemRAM;

        // Display
        public int displayResolutionWidth;
        public int displayResolutionHeight;
        public float refreshRate;
    }
}