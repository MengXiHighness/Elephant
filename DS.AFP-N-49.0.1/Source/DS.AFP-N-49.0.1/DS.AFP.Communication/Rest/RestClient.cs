using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.Rest
{
    public class RestClient : RestSharp.RestClient, DS.AFP.Communication.Rest.IRestClient
    {
        public int setTimeOut
        {
            set
            {
                base.Timeout = value;
            }
            get
            {
                return base.Timeout;
            }
        }
        public RestClient(string baseUrl):base(baseUrl)
        {
            
            base.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
        }
        public IRestResponse Post(string resource, object postData)
        {
            RestSharp.RestRequest restReq = new RestSharp.RestRequest(resource, Method.POST);
            restReq.RequestFormat = DataFormat.Json;
            restReq.AddBody(postData);
            return Convert(Execute(restReq));

        }

        public IRestResponse Put(string resource, object putData)
        {
            RestRequest restReq = new RestRequest(resource, Method.PUT);
            restReq.RequestFormat = DataFormat.Json;
            restReq.AddBody(putData);
            return Convert(Execute(restReq));
        }

        public IRestResponse Delete(string resource, object putData)
        {
            RestRequest restReq = new RestRequest(resource, Method.DELETE);
            restReq.RequestFormat = DataFormat.Json;
            restReq.AddBody(putData);
            return Convert(Execute(restReq));
        }

        public IRestResponse Get(string resource)
        {
            RestRequest restReq = new RestRequest(resource, Method.GET);

            return Convert(Execute(restReq));
        }

        private IRestResponse Convert(RestSharp.IRestResponse resp)
        {
            RestResponse r = new RestResponse();
            r.Content = resp.Content;
            r.ContentEncoding = resp.ContentEncoding;
            r.ContentLength = resp.ContentLength;
            r.ContentType = resp.ContentType;
            r.ErrorException = resp.ErrorException;
            r.ErrorMessage = resp.ErrorMessage;
            r.ResponseUri = resp.ResponseUri;
            r.Server = resp.Server;
            r.StatusCode = resp.StatusCode;
            return r;
        }
    }
}
