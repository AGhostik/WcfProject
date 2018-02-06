using System.ServiceModel;
using System.ServiceModel.Channels;
using Client.MessageServiceReference;

namespace Client.Model
{
    public class DuplexContractClient : DuplexClientBase<IMessageService>
    {
        public DuplexContractClient(object callbackInstance, Binding binding, EndpointAddress remoteAddress)
            : base(callbackInstance, binding, remoteAddress) { }
    }
}