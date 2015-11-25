﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Security;
using System.Xml;
using System.IO;

namespace MallHost
{

    /// <summary>
    ///  消息拦截器
    /// </summary>
    public class MyMessageInspector : IClientMessageInspector, IDispatchMessageInspector
    {
        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("客户端接收到的回复：\n{0}", reply.ToString());
        }

        object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Console.WriteLine("客户端发送请求前的SOAP消息：\n{0}", request.ToString());
            return null;
        }

        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            string username = request.Headers.GetHeader<string>("header", "name");
            string password = request.Headers.GetHeader<string>("header", "pass");
            if (DataBusiProcess.ValidID(DES.DecryptDES(username, DES.ConstKey), DES.DecryptDES(password, DES.ConstKey)))
            {
                return "验证成功";
            }
            else
            {
                throw new SecurityException("验证失败");
            }
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
            if (correlationState != null)
            {
                MessageHeader header = MessageHeader.CreateHeader("result", "", "true");
                reply.Headers.Add(header);
            }
            else
            {
                MessageHeader header = MessageHeader.CreateHeader("result", "", "false");
                reply.Headers.Add(header);
            }
        }
    }

    /// <summary>
    /// 插入到终结点的Behavior
    /// </summary>
    public class MyEndPointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // 不需要
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // 植入“偷听器”客户端
            //clientRuntime.ClientMessageInspectors.Add(new MyMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // 植入“偷听器” 服务器端
            //endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MyMessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // 不需要
            return;
        }
    }

}
