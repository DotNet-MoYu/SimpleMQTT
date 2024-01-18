using Microsoft.Extensions.Configuration;
using NewLife.MQTT;

namespace SimpleMQTT
{
    /// <summary>
    /// <inheritdoc cref="IMqttClientManager"/>
    /// </summary>
    public class MqttClientManager : IMqttClientManager
    {
        /// <summary>
        /// mqtt对象列表
        /// </summary>
        private List<MqttClient> MqttClients = new List<MqttClient>();

        public MqttClientManager(IConfiguration configuration)
        {
            MqttClientConfig config = configuration.GetSection("MqttSettings").Get<MqttClientConfig>();//获取配置配置
            if (config != null && !string.IsNullOrEmpty(config.Host))
            {
                //如果配置不是空则创建连接
                AddClient(config);
                StartClient(config.Name);
            }
            else
            {
                //配置是空，试试是不是多个客户端配置
                List<MqttClientConfig> configs = configuration.GetSection("MqttSettings").Get<List<MqttClientConfig>>();//获取配置配置
                if (configs != null)
                {
                    //如果不为空，创建连接
                    configs.ForEach(it =>
                    {
                        AddClient(it);
                        StartClient(it.Name);
                    });
                }
                else
                {
                    throw new ArgumentException("mqtt配置未找到，请配置mqtt链接信息", nameof(config));
                }
            }
        }

        public MqttClientManager(MqttClientConfig config, bool start = false)
        {
            //如果配置不是空则创建连接
            AddClient(config);
            if (start)
                StartClient(config.Name);
        }

        public MqttClientManager(List<MqttClientConfig> configs, bool start = false)
        {
            //如果不为空，创建连接
            configs.ForEach(it =>
            {
                AddClient(it);
                if (start)
                    StartClient(it.Name);
            });
        }


        /// <inheritdoc/>
        public void AddClient(MqttClientConfig config)
        {
            if (!string.IsNullOrEmpty(config.Host))
            {
                if (MqttClients.Any(it => it.Name == config.Name))
                    throw new ArgumentException("mqtt配置错误，Name属性唯一", nameof(config));
                var client = new MqttClient
                {
                    Server = $"tcp://{config.Host}:{config.Port}",
                    Name = config.Name,
                    UserName = config.UserName,
                    Password = config.SecretKey,
                    ClientId = config.ClientId,
                    KeepAlive = config.KeepAlive,
                    Reconnect = config.Reconnect,
                    CleanSession = config.CleanSession,
                    Timeout = config.Timeout,
                    UseSSL = config.UseSSL,

                };
                client.Connected += MqttClient_Connected;
                client.Disconnected += MqttClient_Disconnected;
                MqttClients.Add(client);
            }
            else
            {
                throw new ArgumentException("mqtt配置错误，请配置mqtt链接信息", nameof(config));
            }


        }


        /// <inheritdoc/>
        public bool StartClient(string name = "Mqtt")
        {
            //获取mqtt连接
            var client = MqttClients.Where(it => it.Name == name).FirstOrDefault();
            if (client != null)//获取不为空
            {
                try
                {
                    client.ConnectAsync().Wait();//连接服务器
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return client.IsConnected;//返回连接状态
            }
            else { return false; }
        }


        /// <inheritdoc/>
        public MqttClient GetClient(string name = "Mqtt")
        {
            var client = MqttClients.Where(it => it.Name == name).FirstOrDefault();
            if (client != null) return client;
            else return null;
        }

        /// <inheritdoc/>
        public List<MqttClient> GetClients()
        {
            return MqttClients;
        }

        /// <summary>
        /// 断开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_Disconnected(object sender, EventArgs e)
        {
            var client = ((MqttClient)sender);
            Console.WriteLine($"已断开{client.Name}服务端!:" + DateTime.Now);
        }

        /// <summary>
        /// 连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_Connected(object sender, EventArgs e)
        {
            var client = ((MqttClient)sender);
            Console.WriteLine($"已连接{client.Name}的服务端!:" + DateTime.Now);
        }
    }
}
