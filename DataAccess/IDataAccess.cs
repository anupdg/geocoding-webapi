using System.Collections.Generic;
using map_api.Dto;

namespace map_api.Data{

    /// <summary>
    /// Data access interface for map
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Get all markers
        /// </summary>
        /// <returns>List of markers</returns>
        List<Marker> GetAllMarkers();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="markerId">marker id to delete</param>
        /// <returns>status</returns>
        bool DeleteMarker(int markerId);

        /// <summary>
        /// Save marker, create new if does not exists
        /// </summary>
        /// <param name="marker">Maker object</param>
        /// <returns>status</returns>
        int? InsertMarker(Marker marker);

        /// <summary>
        /// Update marker data
        /// </summary>
        /// <param name="marker">Marker data to update</param>
        /// <returns>status</returns>
        bool UpdateMarker(Marker marker);
    }
}