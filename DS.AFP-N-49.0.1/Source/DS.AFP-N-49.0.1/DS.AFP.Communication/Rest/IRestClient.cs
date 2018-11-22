

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DS.AFP.Communication.Rest
{
    public interface IRestClient
    {
        CookieContainer CookieContainer { get; set; }
        IRestResponse Post(string resource, object postData);
        IRestResponse Put(string resource, object putData);
        IRestResponse Delete(string resource, object putData);
        IRestResponse Get(string resource);

        int setTimeOut
        {
            get;set;
        }
        
    }
}
