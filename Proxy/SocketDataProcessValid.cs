namespace ProxyClient
{
    /// <summary>
    /// Socket数据处理
    /// </summary>
    internal sealed class SocketDataProcessValid
    {
        /// <summary>
        /// 数据验证
        /// </summary>
        /// <param name="data">The data.</param>
        public static void DataProcess(MonitorData data)
        {
            if (GSetting.PortMapDict.ContainsKey(data.PortNO))
            {
                // 进一步数据处理
                GSetting.PortMapDict[data.PortNO].DatagramProcess(data);
            }
        }

        /// <summary>
        /// Socket连接验证
        /// </summary>
        /// <param name="data">连接数据</param>
        /// <returns>
        /// 验证结果
        /// </returns>
        public static bool SocketConnection(string data)
        {
            if (data.Length < 16)
            {
                return false;
            }

            if (!data.StartsWith("ECEC"))
            {
                return false;
            }

            if (!data.EndsWith("EDED"))
            {
                return false;
            }

            return true;
        }
    }
}
