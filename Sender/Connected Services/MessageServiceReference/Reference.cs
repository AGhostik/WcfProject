﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sender.MessageServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MessageServiceReference.IMessageService")]
    public interface IMessageService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/GetMessage", ReplyAction="http://tempuri.org/IMessageService/GetMessageResponse")]
        string GetMessage();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/GetMessage", ReplyAction="http://tempuri.org/IMessageService/GetMessageResponse")]
        System.Threading.Tasks.Task<string> GetMessageAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/SetMessage", ReplyAction="http://tempuri.org/IMessageService/SetMessageResponse")]
        void SetMessage(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/SetMessage", ReplyAction="http://tempuri.org/IMessageService/SetMessageResponse")]
        System.Threading.Tasks.Task SetMessageAsync(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessageServiceChannel : Sender.MessageServiceReference.IMessageService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessageServiceClient : System.ServiceModel.ClientBase<Sender.MessageServiceReference.IMessageService>, Sender.MessageServiceReference.IMessageService {
        
        public MessageServiceClient() {
        }
        
        public MessageServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MessageServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessageServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessageServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetMessage() {
            return base.Channel.GetMessage();
        }
        
        public System.Threading.Tasks.Task<string> GetMessageAsync() {
            return base.Channel.GetMessageAsync();
        }
        
        public void SetMessage(string message) {
            base.Channel.SetMessage(message);
        }
        
        public System.Threading.Tasks.Task SetMessageAsync(string message) {
            return base.Channel.SetMessageAsync(message);
        }
    }
}