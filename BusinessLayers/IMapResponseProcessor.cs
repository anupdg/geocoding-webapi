using System;
using map_api.Dto;

namespace map_api.Data.Business{
    public interface IMapResponseProcessor
    {
        ResponseBase ProcessMapErrors(Exception ex);

        bool CheckAddressInCountry(string countryName, Response response);
    }
}