using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.Rest
{
    public class Parameter
    {
        public Parameter() { }

        public string Name { get; set; }
        public object Value { get; set; }
        public ParameterType Type { get; set; }

        
    }
}
