using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.Rest
{
    public class RestResponse : RestSharp.RestResponse, DS.AFP.Communication.Rest.IRestResponse
    {
       

        ResponseStatus IRestResponse.ResponseStatus
        {
            get
            {
                return (ResponseStatus)Enum.ToObject(typeof(ResponseStatus), base.ResponseStatus);
            }
           
        }
    }
}
