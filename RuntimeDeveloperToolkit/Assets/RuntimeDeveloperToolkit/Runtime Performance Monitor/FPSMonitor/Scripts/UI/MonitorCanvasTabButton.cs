using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimePerformanceMonitor
{
    public class MonitorCanvasTabButton : MonoBehaviour
    {
        public MonitorTabs buttonTab;
        
        [Header("UI Elements")] 
        public Image buttonBgImg;
        public TMP_Text buttonText;

        private Button button;
        
        private static readonly Color ButtonBgActiveColor = new Color(103f/255f, 168f/255f, 56f/255f);
        private static readonly Color ButtonBgInActiveColor =  new Color(31f/255f, 108f/255f, 85f/255f);
        private static readonly Color ButtonTextActiveColor = new Color(210f/255f, 255f/255f, 179f/255f);
        private static readonly Color ButtonTextInActiveColor = new Color(177f/255f, 255f/255f, 232f/255f);

        public void OnButtonClick()
        {
            PerformanceMonitorCanvas.OnClickTab?.Invoke(buttonTab);
        }
        
        internal void SetButtonState(bool isActive)
        {
            buttonBgImg.color = isActive ? ButtonBgActiveColor : ButtonBgInActiveColor;
            buttonText.color = isActive ? ButtonTextActiveColor : ButtonTextInActiveColor;
        }
    }
}