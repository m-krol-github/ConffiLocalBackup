using Newtonsoft.Json;
using UnityEngine;

namespace Conffi.Data
{
    public sealed class JsonPresetLoader : MonoBehaviour
    {
        [SerializeField] private string jsonContent;

        public Sofa Preset => LoadPresetFromJson();
        
        private Sofa LoadPresetFromJson()
        {
            var preset = JsonConvert.DeserializeObject<Sofa>(jsonContent);
            return preset;
        }
    }
}