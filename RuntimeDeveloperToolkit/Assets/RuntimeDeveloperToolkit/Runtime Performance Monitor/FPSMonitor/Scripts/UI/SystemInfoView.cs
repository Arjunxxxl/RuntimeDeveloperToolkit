using TMPro;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class SystemInfoView : MonoBehaviour
    {
        [Header("UI Elements")] 
        public TMP_Text deviceModelTxt;
        public TMP_Text deviceTypeTxt;
        public TMP_Text operatingSystemTxt;
        public TMP_Text processorNameTxt;
        public TMP_Text processorCountTxt;
        public TMP_Text processorFrequencyTxt;
        public TMP_Text gpuNameTxt;
        public TMP_Text gpuVendorTxt;
        public TMP_Text graphicAPITxt;
        public TMP_Text graphicsDeviceVersionTxt;
        public TMP_Text gpuMemoryTxt;
        public TMP_Text systemRAMTxt;
        public TMP_Text displayResolutionTxt;
        public TMP_Text refreshRateTxt;

        private readonly string PlaceHolderStr = "--";

        internal void SetUp()
        {
            deviceModelTxt.text = PlaceHolderStr;
            deviceTypeTxt.text = PlaceHolderStr;
            operatingSystemTxt.text = PlaceHolderStr;
            processorNameTxt.text = PlaceHolderStr;
            processorCountTxt.text = PlaceHolderStr;
            processorFrequencyTxt.text = PlaceHolderStr;
            gpuNameTxt.text = PlaceHolderStr;
            gpuVendorTxt.text = PlaceHolderStr;
            graphicAPITxt.text = PlaceHolderStr;
            graphicsDeviceVersionTxt.text = PlaceHolderStr;
            gpuMemoryTxt.text = PlaceHolderStr;
            systemRAMTxt.text = PlaceHolderStr;
            displayResolutionTxt.text = PlaceHolderStr;
            refreshRateTxt.text = PlaceHolderStr;
        }

        internal void UpdateDataInUi(SystemStats systemStats)
        {
            deviceModelTxt.text = systemStats.deviceModel;
            deviceTypeTxt.text = systemStats.deviceType.ToString();
            operatingSystemTxt.text = systemStats.operatingSystem;
            processorNameTxt.text = systemStats.processorName;
            processorCountTxt.text = systemStats.processorCount + " cores";
            processorFrequencyTxt.text = RDT_TextFormator.GetFrequencyString(systemStats.processorFrequency);
            gpuNameTxt.text = systemStats.gpuName;
            gpuVendorTxt.text = systemStats.gpuVendor;
            graphicAPITxt.text = systemStats.graphicAPI;
            graphicsDeviceVersionTxt.text = systemStats.graphicsDeviceVersion;
            gpuMemoryTxt.text = RDT_TextFormator.GetMemoryString(systemStats.gpuMemory) + " VRAM";
            systemRAMTxt.text = RDT_TextFormator.GetMemoryString(systemStats.systemRAM) + " RAM";
            displayResolutionTxt.text = systemStats.displayResolutionWidth + "x" + systemStats.displayResolutionHeight;
            refreshRateTxt.text = systemStats.refreshRate.ToString("N1") + " Hz";
        }
    }
}