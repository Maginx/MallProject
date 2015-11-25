using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Windows.Forms;

namespace ProxyClient
{
    /// <summary>
    /// WCF客户端拦截器
    /// </summary>
    internal sealed class WCFClientInspector : IClientMessageInspector
    {
        /// <summary>
        /// 消息拦截
        /// </summary>
        /// <param name="reply">要转换为类型并交回给客户端应用程序的消息。</param>
        /// <param name="correlationState">关联状态数据。</param>
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (reply.Headers.GetHeader<string>("result", string.Empty).ToString() == "false")
            {
                MessageBox.Show("请求失败，您是非法用户！");
            }
        }

        /// <summary>
        /// 在将请求消息发送到服务之前，启用消息的检查或修改。
        /// </summary>
        /// <param name="request">要发送给服务的消息。</param>
        /// <param name="channel">客户端对象通道。</param>
        /// <returns>
        /// 作为 <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)" /> 方法的 <paramref name="correlationState " />参数返回的对象。如果不使用相关状态，则为 null。最佳做法是将它设置为 <see cref="T:System.Guid" />，以确保没有两个相同的 <paramref name="correlationState" /> 对象。
        /// </returns>
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            MessageHeader headerName = MessageHeader.CreateHeader("header", "name", GSetting.UserName);
            MessageHeader headerPass = MessageHeader.CreateHeader("header", "pass", GSetting.PassWord);
            request.Headers.Add(headerName);
            request.Headers.Add(headerPass);
            return null;
        }
    }

    /// <summary>  
    /// 插入到终结点的Behavior  
    /// </summary> 
    internal class MyEndPointBehavior : IEndpointBehavior
    {
        /// <summary>
        /// 实现此方法可以在运行时将数据传递给绑定，从而支持自定义行为。
        /// </summary>
        /// <param name="endpoint">要修改的终结点。</param>
        /// <param name="bindingParameters">绑定元素支持该行为所需的对象。</param>
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        /// <summary>
        /// 在终结点范围内实现客户端的修改或扩展。
        /// </summary>
        /// <param name="endpoint">要自定义的终结点。</param>
        /// <param name="clientRuntime">要自定义的客户端运行时。</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // 植入客户端拦截
            // clientRuntime.MessageInspectors.Add(new WCFClientInspector());
        }

        /// <summary>
        /// 在终结点范围内实现客户端的修改或扩展。
        /// </summary>
        /// <param name="endpoint">公开协定的终结点。</param>
        /// <param name="endpointDispatcher">要修改或扩展的终结点调度程序。</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // 植入服务端拦截
            // endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MyMessageInspector());
        }

        /// <summary>
        /// 实现此方法可以确认终结点是否满足某些设定条件。
        /// </summary>
        /// <param name="endpoint">要验证的终结点。</param>
        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }
    }
}
