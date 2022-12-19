using NewLife.MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMQTT
{
    /// <summary>
    /// MQTT客户端服务
    /// </summary>
    public interface IMqttClientManager
    {
        /// <summary>
        /// 添加mqtt客户端
        /// </summary>
        /// <param name="config">配置信息</param>
        void AddClient(MqttClientConfig config);

        /// <summary>
        /// 获取mqtt客户端
        /// </summary>
        /// <param name="name">客户端名称</param>
        /// <returns></returns>
        MqttClient GetClient(string name = "Mqtt");

        /// <summary>
        /// 获取所有客户端
        /// </summary>
        /// <returns></returns>
        List<MqttClient> GetClients();

        /// <summary>
        /// 启动Mqtt客户端
        /// </summary>
        /// <param name="name">客户端名称</param>
        /// <returns></returns>
        bool StartClient(string name = "Mqtt");
    }
}
