
namespace map_api.Dto{

    ///Base response model
    public class ResponseBase
    {
        public ResponseBase()
        {
          Status = Constants.Success;  
        }
        public string Status { get; set; }
        public string Message {get; set;}
        
    }
}