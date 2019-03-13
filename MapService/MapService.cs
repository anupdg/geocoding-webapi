using System;
using map_api.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace map_api.Data.Service{
    public class MapService: IMapService
    {
        private readonly IConfiguration _configuration;
        private readonly string _key;
        private readonly string _apiBase;
        
        public MapService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = _configuration[Constants.GeoApiKey];
            _apiBase = _configuration[Constants.APIBase];
        }
         public Response ReverseGeocode(double latitude, double longitude){
            string AddressURL = $"{_apiBase}?latlng={latitude},{longitude}&key={_key}";
            var result = new System.Net.WebClient().DownloadString(AddressURL);
            Response data =  JsonConvert.DeserializeObject<Response>(result);
            if(data.status==Constants.Ok)
                return data;
            else
            {
                //todo: log error from map for auditing

                throw new Exception(data.status);
            }
                
         }
    } 
}