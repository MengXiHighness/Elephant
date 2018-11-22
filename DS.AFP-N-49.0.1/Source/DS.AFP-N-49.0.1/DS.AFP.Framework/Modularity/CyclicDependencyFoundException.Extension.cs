
using System;
using System.Runtime.Serialization;

namespace DS.AFP.Framework.Modularity
{
    [Serializable]
    public partial class CyclicDependencyFoundException
    {
        protected CyclicDependencyFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
