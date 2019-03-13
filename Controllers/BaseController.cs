using map_api.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace map_api.Controllers
{
    public class BaseController : ControllerBase{
        protected ObjectResult ReturnStatus500(string message){
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseBase(){ 
                    Status=Constants.Error,
                    Message = message 
                });
        } 

        protected ObjectResult ReturnStatus400(string message){
            return StatusCode(StatusCodes.Status400BadRequest,
                new ResponseBase(){ 
                    Status=Constants.Error,
                    Message = message 
                });
        } 

        protected ObjectResult ReturnStatus400ByResponseBase(ResponseBase responseData){
            return StatusCode(StatusCodes.Status400BadRequest,responseData);
        } 

        
    }
}