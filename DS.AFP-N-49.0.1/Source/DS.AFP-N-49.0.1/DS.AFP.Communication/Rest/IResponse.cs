using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DS.AFP.Communication.Rest
{
    public interface IRestResponse
    {
        
        string ContentType { get; set; }
        long ContentLength { get; set; }
        string ContentEncoding { get; set; }
        string Content { get; set; }
        HttpStatusCode StatusCode { get; set; }
        string StatusDescription { get; set; }
        byte[] RawBytes { get; set; }
        Uri ResponseUri { get; set; }
        string Server { get; set; }
        //IList<RestResponseCookie> Cookies { get; }
        //IList<Parameter> Headers { get; }
        ResponseStatus ResponseStatus { get; }
        string ErrorMessage { get; set; }
        Exception ErrorException { get; set; }
    }
}
