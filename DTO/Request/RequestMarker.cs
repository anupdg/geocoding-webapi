using System.ComponentModel.DataAnnotations;

namespace map_api.Dto{

    /// <summary>
    /// Base response model
    /// </summary>
    public class RequestMarker: RequestBase
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
        [Required]
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