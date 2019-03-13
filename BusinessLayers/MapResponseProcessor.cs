using System;
using map_api.Dto;

namespace map_api.Data.Business{
    public class MapResponseProcessor: IMapResponseProcessor
    {
        public ResponseBase ProcessMapErrors(Exception ex)
        {
            string logMessage = string.Empty;
                
            if(ex.Message==Constants.NoResult || ex.Message==Constants.InvalidRequest)
            {
                logMessage = GeoApiResponseStatus.ResponseStatus[ex.Message];
                return new ResponseBase(){ 
                                Status=Constants.Error,
                                Message=logMessage 
                            } ;
                
            }
            else if(Constants.RequestDenied == ex.Message||
                    Constants.DailyLimit == ex.Message||
                    Constants.QueryLimit == ex.Message||
                    Constants.UnknownError == ex.Message)
            {
                logMessage = GeoApiResponseStatus.ResponseStatus[ex.Message];
                return new ResponseBase(){ 
                    Status=Constants.Error,
                    Message="Unable to connect to map api, contact administrator" 
                } ;
            }
            else if(Constants.AddressOutsideCountry == ex.Message)
            {
                logMessage = GeoApiResponseStatus.ResponseStatus[ex.Message];
                return new ResponseBase(){ 
                    Status=Constants.Error,
                    Message=logMessage 
                } ;
            }
        
            //todo: Log logMessage for internal debugging purpose

            return null;
        }

        public bool CheckAddressInCountry(string countryName, Response response){
            bool found = false;

            if(response == null || response.results == null)
                throw new Exception(Constants.MapResponseProcessingError);
            else
            {
                for(int i= response.results.Count-1; i>=0; i--)
                {
                    if(response.results[i].types == null)
                    {
                        throw new Exception(Constants.MapResponseProcessingError);
                    }
                    else
                    {
                        if(response.results[i].types.Contains("country") 
                            && response.results[i].formatted_address != null
                            && response.results[i].formatted_address.Equals(countryName))
                        {
                            found = true;
                            break;
                        }
                        else
                            throw new Exception(Constants.AddressOutsideCountry);
                    }
                }
            }
            return found;
        }
    }
}