using System.Runtime.InteropServices;

public static class WebGLPluginJS
{
    [DllImport("__Internal")]
    public static extern void SendJSONToPage(string text);
    
    [DllImport("__Internal")]
    public static extern string GetJSONFromPage();
}
