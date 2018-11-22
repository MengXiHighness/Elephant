using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.Rest
{
    public enum ParameterType
    {
        Cookie = 0,
        GetOrPost = 1,
        UrlSegment = 2,
        HttpHeader = 3,
        RequestBody = 4
    }
}
