namespace map_api{
    /// <summary>
    /// Cors policy
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Policy name
        /// </summary>
        public const string PolicyName = "DangerPolicy";   
        public const string Ok = "OK";   
        public const string NoResult = "ZERO_RESULTS";   
        public const string DailyLimit = "OVER_DAILY_LIMIT";   
        public const string QueryLimit = "OVER_QUERY_LIMIT";
        public const string RequestDenied = "REQUEST_DENIED";   
        public const string InvalidRequest = "INVALID_REQUEST";   
        public const string UnknownError = "UNKNOWN_ERROR";
        public const string MapResponseProcessingError = "MAP_RESPONSE_PROCESSING_ERROR";
        public const string AddressOutsideCountry = "ADDRESS_OUTSIDE_COUNTRY";
        public const string AllowCorsUrls = "AllowCorsUrls";
        public const string DataSourceProvider = "DataSourceProvider";
        public const string GeoApiKey = "GeoApiKey";
        public const string APIBase = "APIBase";
        public const string CountryName = "CountryName";

        public const string Error = "FAILURE";
        public const string Success = "SUCCESS";      
        
    }
}