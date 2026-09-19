using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class SystemStatsCalculator : MonoBehaviour
    {
        private SystemStats systemStats;

        internal void SetUp()
        {
            systemStats = new SystemStats();
            Calculate();
        }

        private void Calculate()
        {
            // Device
            systemStats.deviceModel = SystemInfo.deviceModel;
            systemStats.deviceType = SystemInfo.deviceType;
            systemStats.operatingSystem = SystemInfo.operatingSystem;

            // CPU
            systemStats.processorName = SystemInfo.processorType;
            systemStats.processorCount = SystemInfo.processorCount;
            systemStats.processorFrequency = SystemInfo.processorFrequency;

            // GPU
            systemStats.gpuName = SystemInfo.graphicsDeviceName;
            systemStats.gpuVendor = SystemInfo.graphicsDeviceVendor;
            systemStats.graphicAPI = SystemInfo.graphicsDeviceType.ToString();
            systemStats.graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;

            // Memory
            systemStats.gpuMemory = SystemInfo.graphicsMemorySize;
            systemStats.systemRAM = SystemInfo.systemMemorySize;

            // Display
            systemStats.displayResolutionWidth = Screen.width;
            systemStats.displayResolutionHeight = Screen.height;
            systemStats.refreshRate = (float)Screen.currentResolution.refreshRateRatio.value;
        }

        internal SystemStats GetSystemStats()
        {
            return systemStats;
        }
    }
}