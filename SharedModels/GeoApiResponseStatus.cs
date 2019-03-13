using System.Collections.Generic;

namespace map_api.Data
{
    public static class GeoApiResponseStatus
    {
        static GeoApiResponseStatus()
        {
            ResponseStatus = new Dictionary<string, string>()
                {
                { Constants.RequestDenied,  "Api request was denied"},
                { Constants.NoResult, "Address does not exists" },
                { Constants.DailyLimit, "The API key is missing or invalid or Billing has not been enabled on your account or A self-imposed usage cap has been exceeded or The provided method of payment is no longer valid" },
                { Constants.QueryLimit, "Quota is over" },
                { Constants.InvalidRequest, "Query is missing address, components or latlng" },
                { Constants.UnknownError, "Server error" },
                { Constants.AddressOutsideCountry, "Address is outside range" }
            };
        }
        public static Dictionary<string, string> ResponseStatus { get; set; }        
    }    
}
