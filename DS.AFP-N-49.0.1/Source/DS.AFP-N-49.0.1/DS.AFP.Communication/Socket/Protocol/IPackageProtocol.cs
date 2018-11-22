using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.SocketNameSpace
{
    public interface IPackageProtocol<T> where T : class ,new()
    {
        T ResolveProtocol(byte[] package);

        IList<T> ResolveProtocol(IList<byte> package);

        byte[] ResolveProtocol(T package);
    }

   

   
}
