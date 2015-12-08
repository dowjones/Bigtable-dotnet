using System;
using System.Threading.Tasks;
using BigtableNet.Common;
using BigtableNet.Common.Extensions;
using BigtableNet.Models.Extensions;
using Google.Apis.Auth.OAuth2;
using Grpc.Core;
using Grpc.Core.Logging;

namespace BigtableNet.Models.Abstraction
{
    public abstract class BigClient : IDisposable
    {
        protected readonly BigtableConfig Config;
        protected readonly Channel Channel;
        protected Func<Channel> ChannelCreator;

        internal string ClusterId { get; private set; }


        public string Project { get { return Config.Project; } }

        public string Zone { get { return Config.Zone; } }

        public string Cluster { get { return Config.Cluster; } }

        protected BigClient(BigtableConfig config, Func<Channel> channelCreator)
        {
            Config = config;
            Channel = channelCreator();
            ChannelCreator = channelCreator;
            ClusterId = config.ToClusterId();
        }


        public async Task Connect()
        {
            await Channel.ConnectAsync();
        }

        public async Task Disconnect()
        {
            await Channel.ShutdownAsync();
        }

        public void Dispose()
        {
            Task.Run(async () => await Channel.ShutdownAsync());
        }
    }
}
