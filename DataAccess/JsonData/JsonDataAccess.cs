using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using map_api.Data;
using map_api.Dto;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Linq;
using map_api.Data.Service;
using map_api.Data.Business;
using Microsoft.Extensions.Configuration;

namespace map_api.Data.Json{

    /// <summary>
    /// Data access layer for json data source
    /// </summary>
    public class JsonDataAccess : IDataAccess
    {
        private readonly string JsonPath;
        private readonly IMapper _mapper;
        private readonly IMapService _mapService;
        private readonly IMapResponseProcessor _mapResponseProcessor;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor for Json data access. This takes hosting environment context 
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="mapper">Object mapper</param>
        /// <param name="mapService">Map api</param>
        /// <param name="mapResponseProcessor">Map response processor</param>
        /// <param name="configuration">Configuration provider</param>
        public JsonDataAccess(IHostingEnvironment hostingEnvironment, 
                                IMapper mapper, 
                                IMapService mapService, 
                                IMapResponseProcessor mapResponseProcessor,
                                IConfiguration configuration)
        {
            _mapService = mapService;

            string contentRootPath = hostingEnvironment.ContentRootPath;
            JsonPath = $"{contentRootPath}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}markers.json";
            _mapper = mapper;

            _mapResponseProcessor = mapResponseProcessor;

            _configuration = configuration;
        }
        /// <summary>
        /// Delete marker data by id
        /// </summary>
        /// <param name="markerId">marker id to delete</param>
        /// <returns>status</returns>
        public bool DeleteMarker(int markerId)
        {
            var markers = GetAllMarkers();
            
            var query = markers.Where((d)=> d.Id == markerId);

            if(query.Count()>0)
            {
                var data = markers.Where((d)=> d.Id != markerId).ToList();
                
                WriteDataToFile(data);
                return true;
            }
            else
            {
                throw new Exception("Marker does not exists");
            }
        }

        /// <summary>
        /// Get all markers
        /// </summary>
        /// <returns>List of markers</returns>
        public List<Marker> GetAllMarkers()
        {
            var markerString = File.ReadAllText(JsonPath);
            
            return JsonConvert.DeserializeObject<List<Marker>>(markerString);
        }

        /// <summary>
        /// Insert a marker.
        /// </summary>
        /// <param name="marker">Marker object</param>
        /// <returns>Inserted marker id</returns>
        public int? InsertMarker(Marker marker)
        {
            if(marker.Id == null || marker.Id <=0)
            {
                Random random = new Random();  
                marker.Id = random.Next(1, Int32.MaxValue); // Incase of database this can be, say, auto generated
            }

            Response mapResponse = _mapService.ReverseGeocode(marker.Latitude, marker.Longitude);

            bool found =_mapResponseProcessor.CheckAddressInCountry(_configuration[Constants.CountryName], mapResponse);

            if(found)
            {
                var markers = GetAllMarkers();
                
                if(markers == null)
                    markers = new List<Marker>();

                var query = markers.Where((d)=> d.Id == marker.Id);

                if(query.Count()>0)
                {
                    throw new Exception("Data already exists");
                }

                markers.Add(marker);

                WriteDataToFile(markers);
                return marker.Id;
            }
            else
                throw new Exception(Constants.AddressOutsideCountry);
        }

        /// <summary>
        /// Update marker data
        /// </summary>
        /// <param name="marker">Marker data to update</param>
        /// <returns>status</returns>
        public bool UpdateMarker(Marker marker){
            var markers = GetAllMarkers();
            
            var query = markers.Where((d)=> d.Id == marker.Id);

            if(query.Count()>0)
            {
                Response mapResponse = _mapService.ReverseGeocode(marker.Latitude, marker.Longitude);

                bool found =_mapResponseProcessor.CheckAddressInCountry(_configuration[Constants.CountryName], mapResponse);

                if(found)
                {
                    for(int i=0; i< markers.Count; i++)
                    {
                        if(markers[i].Id == marker.Id)
                        {
                            markers[i] = marker;
                        }
                    }
                    WriteDataToFile(markers);
                    return true;
                }
                else
                 throw new Exception(Constants.AddressOutsideCountry);
            }
            else
            {
                throw new Exception("Marker does not exists");
            }
            
        }

        private void WriteDataToFile(List<Marker> markers){

            File.WriteAllText(JsonPath, JsonConvert.SerializeObject(markers));
        }

    }
}