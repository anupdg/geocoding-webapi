using Newtonsoft.Json;

namespace map_api.Data{

    /// <summary>
    /// Data model for marker, used for both data and business logic
    /// </summary>
    public class Marker
    {
        /// <summary>
        /// Id for the marker, this is a reference key id like primary key or unique key, generated from data access logic
        /// </summary>
        /// <value></value>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// Place name
        /// </summary>
        /// <value></value>
        [JsonProperty(PropertyName = "name")]
        public string PlaceName { get; set; }

        /// <summary>
        /// Map tooltip
        /// </summary>
        /// <value></value>
        [JsonProperty(PropertyName = "title")]
        public string Tooltip {get; set;}
        
        /// <summary>
        /// Location latitute
        /// </summary>
        /// <value></value>
        [JsonProperty(PropertyName = "lat")]
        public float Latitude { get; set; }
        
        /// <summary>
        /// Location longitude
        /// </summary>
        /// <value></value>
        [JsonProperty(PropertyName = "lng")]
        public float Longitude { get; set; }
    }
}