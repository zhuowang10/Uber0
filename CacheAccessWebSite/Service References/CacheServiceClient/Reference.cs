﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CacheAccessWebSite.CacheServiceClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CacheServiceClient.ICacheService")]
    public interface ICacheService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheService/GetHelloWorldWithCache", ReplyAction="http://tempuri.org/ICacheService/GetHelloWorldWithCacheResponse")]
        string GetHelloWorldWithCache(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheService/ClearHelloWorldCache", ReplyAction="http://tempuri.org/ICacheService/ClearHelloWorldCacheResponse")]
        void ClearHelloWorldCache(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICacheServiceChannel : CacheAccessWebSite.CacheServiceClient.ICacheService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CacheServiceClient : System.ServiceModel.ClientBase<CacheAccessWebSite.CacheServiceClient.ICacheService>, CacheAccessWebSite.CacheServiceClient.ICacheService {
        
        public CacheServiceClient() {
        }
        
        public CacheServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CacheServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CacheServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CacheServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetHelloWorldWithCache(string name) {
            return base.Channel.GetHelloWorldWithCache(name);
        }
        
        public void ClearHelloWorldCache(string name) {
            base.Channel.ClearHelloWorldCache(name);
        }
    }
}
