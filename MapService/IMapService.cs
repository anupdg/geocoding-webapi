using map_api.Dto;

namespace map_api.Data.Service{
    public interface IMapService
    {
         Response ReverseGeocode(double latitude, double longitude);
    } 
}