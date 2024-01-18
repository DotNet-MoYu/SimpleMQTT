using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Serialization;
using SimpleMQTT;
using System;

namespace SimpleMQTT
{
    /// <summary>
    /// Mqtt客户端扩展类
    /// </summary>
    public static class SimpleMQTTSetup
    {
        /// <summary>
        /// 添加Mqtt客户端服务
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddMqttClientManager(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<IMqttClientManager, MqttClientManager>();
        }

        /// <summary>
        /// 添加Mqtt客户端服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="section">配置文件节点</param>
        /// <param name="start">立即启动</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddMqttClientManager(this IServiceCollection services, IConfiguration configuration, string section = "MqttSettings", bool start = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            MqttClientConfig config = configuration.GetSection(section).Get<MqttClientConfig>();//获取配置配置
            if (config != null && !string.IsNullOrEmpty(config.Host))
            {
                services.AddSingleton<IMqttClientManager, MqttClientManager>(x => new MqttClientManager(config, start));
            }
            else
            {
                List<MqttClientConfig> configs = configuration.GetSection(section).Get<List<MqttClientConfig>>();//获取配置配置
                if (configs == null) throw new ArgumentException("mqtt配置未找到，请配置mqtt链接信息", nameof(configuration));
                services.AddSingleton<IMqttClientManager, MqttClientManager>(x => new MqttClientManager(configs, start));
            }

        }

        /// <summary>
        /// 添加Mqtt客户端服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config">mqtt配置</param>
        /// <param name="start">立即启动</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddMqttClientManager(this IServiceCollection services, MqttClientConfig config, bool start = true)
        {

            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<IMqttClientManager, MqttClientManager>(x => new MqttClientManager(config, start));
        }

        /// <summary>
        /// 添加Mqtt客户端服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configs">mqtt配置</param>
        /// <param name="start">立即启动</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddMqttClientManager(this IServiceCollection services, List<MqttClientConfig> configs, bool start = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<IMqttClientManager, MqttClientManager>(x => new MqttClientManager(configs, start));
        }
    }
}
