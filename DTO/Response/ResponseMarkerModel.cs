namespace map_api.Dto{

    /// <summary>
    /// Response model for marker item
    /// </summary>
    public class ResponseMarkerModel 
    {
        /// <summary>
        /// Data id
        /// </summary>
        /// <value></value>
        public int? Id { get; set; }
        
        /// <summary>
        /// Place name
        /// </summary>
        /// <value></value>
        public string PlaceName { get; set; }

        /// <summary>
        /// Map tooltip
        /// </summary>
        /// <value></value>
        public string Tooltip {get; set;}
        
        /// <summary>
        /// Location latitute
        /// </summary>
        /// <value></value>
        public float Latitude { get; set; }
        
        /// <summary>
        /// Location longitude
        /// </summary>
        /// <value></value>
        public float Longitude { get; set; }
    }
}