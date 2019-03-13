using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using map_api.Data;
using map_api.Data.Business;
using map_api.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace map_api.Controllers
{
    /// <summary>
    /// Map REST api
    /// </summary>
    [EnableCors(Constants.PolicyName)]
    [Route("api")]
    [ApiController]
    public class MapController : BaseController
    {

        private readonly IDataAccess _dataAccess;
        private readonly IMapResponseProcessor _mapResponseProcessor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor used for dependency injection
        /// </summary>
        /// <remarks>
        /// <param name="dataAccess"></param>
        public MapController(IDataAccess dataAccess, IMapper mapper, IMapResponseProcessor mapResponseProcessor)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _mapResponseProcessor = mapResponseProcessor;
        }
        
        /// <summary>
        /// Get all markers
        /// </summary>
        /// <remarks>Get all markers !</remarks>
        /// <response code="200">Marker list for success response</response>
        /// <response code="500">Oops! Server error</response>
        [Route("{lang}/map")]
        [HttpGet]
        [ProducesResponseType(typeof(Marker), 200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<ResponseMarkerModel>> Get([FromRoute]RequestBase request)
        {
            try{
                List<Marker> markers =_dataAccess.GetAllMarkers();

                var markersResponse = _mapper.Map<List<ResponseMarkerModel>>(markers);

                ResponseMarkersModel response = new ResponseMarkersModel();
                response.MarkersList = markersResponse;

                return Ok(response);
            }
            catch(Exception ex) //todo: Do proper logging of the exception
            {
                return ReturnStatus500("Opps! Server error, contact API owner");
            }
        }

        /// <summary>
        /// Save a marker 
        /// </summary>
        /// <remarks>Saves a new marker</remarks>
        /// <response code="200">Marker saved successfully</response>
        /// <response code="500">Oops! Server error</response>
        /// <response code="400">Input data invalid</response>
        [Route("map")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [HttpPost]
        public ActionResult<ResponseMarkerModel> Post( [FromBody] RequestMarker marker)
        {
            //marker.lang = lang;

            try{
                if (ModelState.IsValid)
                {
                    var markerData = _mapper.Map<RequestMarker, Marker>(marker);
                    
                    int? markerId = _dataAccess.InsertMarker(markerData);
                    return Ok(new GenericResponse(){Data = markerId, Message="Location saved successfully"});
                }
                else{
                    if(marker.Id != null)
                        return ReturnStatus400("Please check input"); 
                    else
                        return ReturnStatus400("Input data is not valid");  
                }
            }
            catch(Exception ex) //todo: Do proper logging of the exception
            {
                ResponseBase responseData =_mapResponseProcessor.ProcessMapErrors(ex);

                if(responseData!=null)
                    return StatusCode(StatusCodes.Status400BadRequest,responseData);
                
                return ReturnStatus500("Opps! Server error, contact API owner");
            }
        }

        /// <summary>
        /// Update a marker 
        /// </summary>
        /// <remarks>Update a marker</remarks>
        /// <response code="200">Marker updated successfully</response>
        /// <response code="500">Oops! Server error</response>
        /// <response code="400">Input data invalid</response>
        [Route("map/{id}")]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [HttpPut]
        public ActionResult<bool> Put([FromRoute]int id, [FromBody] RequestMarker marker)
        {
            try{
                if (ModelState.IsValid)
                {
                    if(marker.Id == null)
                        marker.Id = id;

                    var markerData = _mapper.Map<RequestMarker, Marker>(marker);
                    bool status =_dataAccess.UpdateMarker(markerData);
                    return Ok(new GenericResponse(){Data = marker.Id, Message="Location updated successfully"});
                }
                else{
                    return ReturnStatus400("Input data is not valid");
                }
            }
            catch(Exception ex) //todo: Do proper logging of the exception
            {
                ResponseBase responseData =_mapResponseProcessor.ProcessMapErrors(ex);

                if(responseData!=null)
                    return ReturnStatus400ByResponseBase(responseData);

                
                return ReturnStatus500("Opps! Server error, contact API owner");
            }
        }

        /// <summary>
        /// Delete a marker 
        /// </summary>
        /// <remarks>Delete a marker by id</remarks>
        /// <response code="200">Marker deleted successfully</response>
        /// <response code="500">Oops! Server error</response>
        /// <response code="400">Input data invalid</response>
        [Route("{lang}/map/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [HttpDelete]
        public ActionResult<bool> Delete([FromRoute]int id, [FromRoute] string lang)
        {
            try{
                if (ModelState.IsValid)
                {
                    bool status =_dataAccess.DeleteMarker(id);
                    if(status)
                        return Ok(new ResponseBase(){Message="Marker deleted successfully"});
                    else    
                        return  ReturnStatus500("Opps! Server error, contact API owner");
                }
                else{
                    return ReturnStatus400("Input data is not valid"); 
                    
                }
            }
            catch(Exception ex)//todo: Do proper logging of the exception
            {
                return ReturnStatus500("Opps! Server error, contact API owner");
            }
        }
    }
}
