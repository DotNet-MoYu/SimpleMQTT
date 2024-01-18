using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMQTT
{
    /// <summary>
    /// mqtt客户端配置
    /// </summary>
    public class MqttClientConfig
    {
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string Name { get; set; } = "Mqtt";

        /// <summary>
        /// 主机地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 心跳
        /// </summary>
        public int KeepAlive { get; set; } = 60;

        /// <summary>
        /// 是否自动重连,默认True
        /// </summary>
        public bool Reconnect { get; set; } = true;

        /// <summary>
        /// 是否清除会话,默认True
        /// </summary>
        public bool CleanSession { get; set; } = true;

        /// <summary>
        /// 超时。默认15000ms
        /// </summary>
        public int Timeout { get; set; } = 15000;

    }
}
