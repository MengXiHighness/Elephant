

using System;
using DS.AFP.Common.Core.ExceptionNameSpace;

namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// ���ڲ���׳����쳣 
    /// </summary>
    public class ModularityException : AddinException
    {

        public ModularityException()
            : this(null)
        {
        }

        public ModularityException(string message)
            : this(null, message)
        {
        }

        public ModularityException(string message, Exception innerException)
            : this(null, message, innerException)
        {
        }

        public ModularityException(string moduleName, string message)
            : this(moduleName, message, null)
        {
        }

        public ModularityException(string moduleName, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ModuleName = moduleName;
        }

        public string ModuleName { get; set; }
    }
}
