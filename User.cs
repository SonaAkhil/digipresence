using Newtonsoft.Json;

namespace DigiPresence
{
    public class PostDetails
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "icon")]
        public string icon { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }
        [JsonProperty(PropertyName = "color1")]
        public string color1 { get; set; }
        
    }
}