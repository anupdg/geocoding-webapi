using System.Collections.Generic;
using AutoMapper;
using map_api.Data;
using map_api.Dto;

namespace map_api
{

    /// <summary>
    /// Object mappers
    /// </summary>
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {
            CreateMap<ResponseMarkerModel, Marker>(); 
            CreateMap<List<ResponseMarkerModel>, List<Marker>>(); 
            CreateMap<Marker, RequestMarker>();
        }
    }
}