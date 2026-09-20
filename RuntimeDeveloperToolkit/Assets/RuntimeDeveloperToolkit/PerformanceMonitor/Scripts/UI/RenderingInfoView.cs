using TMPro;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class RenderingInfoView : MonoBehaviour
    {
        [Header("UI")] 
        public TMP_Text drawCallTxt;
        public TMP_Text setPassCallTxt;
        public TMP_Text batchesTxt;
        public TMP_Text trianglesTxt;
        public TMP_Text verticesTxt;
        public TMP_Text renderersTxt;
        public TMP_Text camerasTxt;
        public TMP_Text lightsTxt;
        public TMP_Text shadowCasterTxt;
        public TMP_Text skinnedMeshesTxt;

        private readonly string PlaceHolderStr = "--";
        
        internal void SetUp()
        {
            drawCallTxt.text = PlaceHolderStr;
            setPassCallTxt.text = PlaceHolderStr;
            
            batchesTxt.text = PlaceHolderStr;
            trianglesTxt.text = PlaceHolderStr;
            verticesTxt.text = PlaceHolderStr;
            renderersTxt.text = PlaceHolderStr;
            camerasTxt.text = PlaceHolderStr;
            lightsTxt.text = PlaceHolderStr;
            shadowCasterTxt.text = PlaceHolderStr;
            skinnedMeshesTxt.text = PlaceHolderStr;
        }
         
        internal void UpdateDataInUi(RendererStats rendererStats)
        {
            drawCallTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.drawCallCount);
            setPassCallTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.setPassCallCount);
            batchesTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.batchesCount);
            trianglesTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.trianglesCount);
            verticesTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.verticesCount);
            renderersTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.renderersCount);
            camerasTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.camerasCount);
            lightsTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.lightsCount);
            shadowCasterTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.shadowCasterCount);
            skinnedMeshesTxt.text = RDT_TextFormator.GetFormatedString(rendererStats.skinnedMeshesCount); 
        }
    }
}