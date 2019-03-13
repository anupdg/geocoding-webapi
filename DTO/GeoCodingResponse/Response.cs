using System.Collections.Generic;

namespace map_api.Dto{
    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public PlusCode2 plus_code { get; set; }
        public List<string> types { get; set; }
    }

    public class Response
    {
        public string error_message { get; set; } 
        public PlusCode plus_code { get; set; }
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}