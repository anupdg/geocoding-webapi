
namespace map_api.Dto{

    ///Base response model
    public class GenericResponse : ResponseBase
    {
        public dynamic Data { get; set; }
    }
}