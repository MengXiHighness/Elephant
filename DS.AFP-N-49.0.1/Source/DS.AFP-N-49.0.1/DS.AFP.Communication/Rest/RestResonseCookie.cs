﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.Rest
{
    public class RestResponseCookie
    {
        public RestResponseCookie() { }

        public string Comment { get; set; }
        public Uri CommentUri { get; set; }
        public bool Discard { get; set; }
        public string Domain { get; set; }
        public bool Expired { get; set; }
        public DateTime Expires { get; set; }
        public bool HttpOnly { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Port { get; set; }
        public bool Secure { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Value { get; set; }
        public int Version { get; set; }
    }
}
