using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CacheServiceLib
{
    /*
     * this is the interface for cache wcf service
     */
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICacheService
    {
        [OperationContract]
        string GetHelloWorldWithCache(string name);

        [OperationContract]
        void ClearHelloWorldCache(string name);
    }
}
