using System;
using System.Reflection;
using System.ServiceModel;

namespace ProxyClient
{
    /// <summary>
    /// 手动配置WCF元数据地址
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    internal sealed class ServiceDynamicReflect<T> where T : class
    {
        /// <summary>
        /// Create a service instance
        /// </summary>
        /// <param name="serviceIP">The service ip.</param>
        /// <returns>泛型兑现</returns>
        public static T GetInstance(string serviceIP)
        {
            T instance = null;
            Uri serviceUri = new Uri(serviceIP);

            if (string.IsNullOrEmpty(serviceUri.ToString()))
            {
                return null;
            }

            // 参数1
            WSHttpBinding binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;

            // 参数2
            EndpointAddress address = new EndpointAddress(serviceUri);

            object[] paras = new object[2];
            paras[0] = binding;
            paras[1] = address;
            ConstructorInfo constructor = null;

            try
            {
                Type[] types = new Type[2];
                types[0] = typeof(System.ServiceModel.Channels.Binding);
                types[1] = typeof(System.ServiceModel.EndpointAddress);
                constructor = typeof(T).GetConstructor(types);
            }
            catch (Exception)
            {
                return null;
            }

            if (constructor != null)
            {
                instance = (T)constructor.Invoke(paras);
            }

            return instance;
        }
    }
}
