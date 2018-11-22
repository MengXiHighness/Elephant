using System;
using System.Collections.Specialized;
namespace DS.AFP.Communication.Chain.Protocol
{
    public interface IHeaderNameValueCollection
    {
        void Add(string name, string value);
        string this[int index] { get; }
        string this[string name] { get; set; }

        string[] AllKeys { get; }

        void Add(IHeaderNameValueCollection c);

        void Add(NameValueCollection c);

        void Clear();

        void Remove(string name);

        int Count { get; }
    }
}
