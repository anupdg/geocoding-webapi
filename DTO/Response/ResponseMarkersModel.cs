using System.Collections.Generic;

namespace map_api.Dto{

    ///Base response model
    public class ResponseMarkersModel : ResponseBase
    {
        /// <summary>
        /// List of markers
        /// </summary>
        /// <value></value>
        public List<ResponseMarkerModel> MarkersList { get; set; }
    }
}