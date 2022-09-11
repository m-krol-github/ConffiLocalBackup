using Conffi.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Conffi.Java
{
    public class WebBridge : MonoBehaviour
    {
        [SerializeField] private SofaPresenter presenter;
        
        public void GetJSONFromPage(string text)
        {
            presenter.ActivatePreset(JsonConvert.DeserializeObject<Sofa>(text));
        }
        
        public void SendMessageToPage(string text)
        {
            WebGLPluginJS.SendJSONToPage(text);
        }
        
        public void SetColorRed()
        {
            //presenter.SetSegmentsColor(Color.red);
        }
        
        public void SetColorBlue()
        {
            //presenter.SetSegmentsColor(Color.blue);
        }

        public void SegmentLarge()
        {
            presenter.SetSideType(ElementSideType.Large);
        }
        
        public void SegmentThin()
        {
            presenter.SetSideType(ElementSideType.Thin);
        }
    }
}