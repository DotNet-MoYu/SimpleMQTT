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
        public static void AddMqttClientManager(this IServiceCollection services, IConfiguration configuration, string section = "MqttSetting", bool start = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            MqttClientConfig config = new MqttClientConfig();
            configuration.GetSection(section).Bind(config);//获取配置
            services.AddSingleton<IMqttClientManager, MqttClientManager>(x => new MqttClientManager(config, start));
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
